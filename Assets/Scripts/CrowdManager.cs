using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdManager : MonoBehaviour
{
    public static CrowdManager Instance;

    [Header("UI")]
    public TMP_Text crowdText;

    [Header("Goblin Setup")]
    public GameObject[] goblinPrefabs;
    public GameObject giantGoblinPrefab;
    public int mergeThreshold = 10;
    public float giantScaleMultiplier = 2.0f;

    [Header("Path Following")]
    public float followSpeed = 15f;
    public float recordDistance = 0.1f;

    [Header("Cluster & Boundaries")]
    public float clusterWidth = 1.5f;
    public float maxForwardOffset = 0.5f;
    public int maxGoblinsInFront = 3;
    public float limitX = 4f;

    public List<GameObject> activeGoblins = new List<GameObject>();
    public List<GameObject> activeGiantGoblins = new List<GameObject>();

    private List<Vector3> smallOffsets = new List<Vector3>();
    private List<Vector3> giantOffsets = new List<Vector3>();

    private List<Vector3> pathHistory = new List<Vector3>();
    private Vector3 lastRecordedPos;


    // Boss update stuff
    public Example TemproraryBoss;
    public int HowManyPointsToWin = 75;
    private int StopSendingMessages = 0;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (!activeGoblins.Contains(gameObject))
        {
            activeGoblins.Add(gameObject);
            smallOffsets.Add(Vector3.zero);
        }

        lastRecordedPos = transform.position;
        pathHistory.Add(transform.position);
    }

    void Update()
    {
        if (crowdText != null)
        {
            int totalValue = activeGoblins.Count + (activeGiantGoblins.Count * mergeThreshold);
            crowdText.text = totalValue.ToString();
            // päivitä
            if (totalValue >= HowManyPointsToWin && StopSendingMessages == 0)
            {
                TemproraryBoss.BOss();
                StopSendingMessages++;
            }
        }
    }

    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, lastRecordedPos) >= recordDistance)
        {
            pathHistory.Insert(0, transform.position);
            lastRecordedPos = transform.position;

            if (pathHistory.Count > 500)
            {
                pathHistory.RemoveAt(pathHistory.Count - 1);
            }
        }

        for (int i = 1; i < activeGoblins.Count; i++)
        {
            if (activeGoblins[i] == null) continue;
            MoveGoblinAlongPath(activeGoblins[i], smallOffsets[i]);
        }

        for (int i = 0; i < activeGiantGoblins.Count; i++)
        {
            if (activeGiantGoblins[i] == null) continue;
            MoveGoblinAlongPath(activeGiantGoblins[i], giantOffsets[i]);
        }
    }

    void MoveGoblinAlongPath(GameObject goblin, Vector3 offset)
    {
        Vector3 basePos;

        if (offset.z >= 0 || pathHistory.Count == 0)
        {
            basePos = transform.position + new Vector3(0, 0, offset.z);
        }
        else
        {
            int historyStepsBack = Mathf.RoundToInt(Mathf.Abs(offset.z) / recordDistance);
            int index = Mathf.Clamp(historyStepsBack, 0, pathHistory.Count - 1);
            basePos = pathHistory[index];
        }

        Vector3 targetWorldPos = basePos + new Vector3(offset.x, 0, 0);

        Vector3 newPos = Vector3.Lerp(goblin.transform.position, targetWorldPos, Time.deltaTime * followSpeed);
        newPos.x = Mathf.Clamp(newPos.x, -limitX, limitX);

        goblin.transform.position = newPos;
    }

    public void ApplyGateMath(GateType type, int value)
    {
        int currentSmallCount = activeGoblins.Count - 1;

        switch (type)
        {
            case GateType.Add:
                AddSmallGoblins(value);
                break;
            case GateType.Multiply:
                int amountToAdd = (currentSmallCount * value) - currentSmallCount;
                AddSmallGoblins(amountToAdd);
                break;
            case GateType.Subtract:
                RemoveSmallGoblins(value);
                break;
            case GateType.Divide:
                if (value > 0)
                {
                    int newCount = currentSmallCount / value;
                    RemoveSmallGoblins(currentSmallCount - newCount);
                }
                break;
        }

        CheckForMerge();
    }

    void AddSmallGoblins(int count)
    {
        int currentInFront = 0;
        for (int j = 1; j < smallOffsets.Count; j++)
        {
            if (smallOffsets[j].z > 0.1f)
            {
                currentInFront++;
            }
        }

        for (int i = 0; i < count; i++)
        {
            float xOffset = Random.Range(-clusterWidth, clusterWidth);
            float zOffset;

            if (currentInFront < maxGoblinsInFront)
            {
                zOffset = Random.Range(0.1f, maxForwardOffset);
                currentInFront++;
            }
            else
            {
                float maxBackwardOffset = 0.5f + ((activeGoblins.Count + activeGiantGoblins.Count) * 0.08f);
                zOffset = Random.Range(-maxBackwardOffset, -0.1f);
            }

            Vector3 spawnOffset = new Vector3(xOffset, 0, zOffset);
            GameObject prefabToUse = GetRandomPrefab();

            if (prefabToUse != null)
            {
                GameObject newGoblin = Instantiate(prefabToUse, transform.position + spawnOffset, Quaternion.identity);
                activeGoblins.Add(newGoblin);
                smallOffsets.Add(spawnOffset);
            }
        }
    }

    void RemoveSmallGoblins(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (activeGoblins.Count > 1)
            {
                int lastIndex = activeGoblins.Count - 1;
                GameObject goblinToRemove = activeGoblins[lastIndex];

                activeGoblins.RemoveAt(lastIndex);
                smallOffsets.RemoveAt(lastIndex);
                Destroy(goblinToRemove);
            }
            else if (activeGiantGoblins.Count > 0)
            {
                int lastGiantIndex = activeGiantGoblins.Count - 1;
                GameObject giantToRemove = activeGiantGoblins[lastGiantIndex];

                activeGiantGoblins.RemoveAt(lastGiantIndex);
                giantOffsets.RemoveAt(lastGiantIndex);
                Destroy(giantToRemove);

                AddSmallGoblins(mergeThreshold - 1);
            }
        }
    }

    void CheckForMerge()
    {
        while (activeGoblins.Count - 1 >= mergeThreshold)
        {
            for (int i = 0; i < mergeThreshold; i++)
            {
                int lastIndex = activeGoblins.Count - 1;
                GameObject goblinToRemove = activeGoblins[lastIndex];

                activeGoblins.RemoveAt(lastIndex);
                smallOffsets.RemoveAt(lastIndex);
                Destroy(goblinToRemove);
            }

            SpawnGiantGoblin();
        }
    }

    void SpawnGiantGoblin()
    {
        float xOffset = Random.Range(-clusterWidth, clusterWidth);
        float maxBackwardOffset = 0.5f + ((activeGoblins.Count + activeGiantGoblins.Count) * 0.08f);
        float zOffset = Random.Range(-maxBackwardOffset, -0.1f);

        Vector3 spawnOffset = new Vector3(xOffset, 0, zOffset);
        GameObject prefabToUse = giantGoblinPrefab != null ? giantGoblinPrefab : GetRandomPrefab();

        if (prefabToUse != null)
        {
            GameObject giant = Instantiate(prefabToUse, transform.position, Quaternion.identity);

            if (giantGoblinPrefab == null)
            {
                giant.transform.localScale *= giantScaleMultiplier;
            }

            activeGiantGoblins.Add(giant);
            giantOffsets.Add(spawnOffset);
        }
    }

    GameObject GetRandomPrefab()
    {
        if (goblinPrefabs == null || goblinPrefabs.Length == 0) return null;
        int randomIndex = Random.Range(0, goblinPrefabs.Length);
        return goblinPrefabs[randomIndex];
    }
}