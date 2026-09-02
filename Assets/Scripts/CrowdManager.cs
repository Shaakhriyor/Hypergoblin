using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    public static CrowdManager Instance;

    [Header("Goblin Setup")]
    public GameObject[] goblinPrefabs; // Assign multiple Goblin Prefabs here!
    public List<GameObject> activeGoblins = new List<GameObject>();

    [Header("Cluster & Boundaries")]
    public float clusterRadius = 0.8f;
    public float limitX = 4f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (!activeGoblins.Contains(gameObject))
        {
            activeGoblins.Add(gameObject);
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < activeGoblins.Count; i++)
        {
            if (activeGoblins[i] == null) continue;

            Vector3 pos = activeGoblins[i].transform.position;
            pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
            activeGoblins[i].transform.position = pos;
        }
    }

    public void ApplyGateMath(GateType type, int value)
    {
        int currentCount = activeGoblins.Count;
        int newTotal = currentCount;

        switch (type)
        {
            case GateType.Add:
                newTotal += value;
                break;
            case GateType.Multiply:
                newTotal *= value;
                break;
            case GateType.Subtract:
                newTotal -= value;
                break;
            case GateType.Divide:
                if (value > 0) newTotal /= value;
                break;
        }

        newTotal = Mathf.Max(1, newTotal);
        int difference = newTotal - currentCount;

        if (difference > 0)
        {
            SpawnGoblins(difference);
        }
        else if (difference < 0)
        {
            RemoveGoblins(Mathf.Abs(difference));
        }
    }

    void SpawnGoblins(int count)
    {
        if (goblinPrefabs == null || goblinPrefabs.Length == 0) return;

        for (int i = 0; i < count; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * (clusterRadius + (activeGoblins.Count * 0.03f));
            Vector3 spawnOffset = new Vector3(randomCircle.x, 0, randomCircle.y);

            // Pick a random goblin variant from the array
            int randomIndex = Random.Range(0, goblinPrefabs.Length);
            GameObject chosenPrefab = goblinPrefabs[randomIndex];

            GameObject newGoblin = Instantiate(chosenPrefab, transform.position + spawnOffset, Quaternion.identity);
            newGoblin.transform.SetParent(transform);

            activeGoblins.Add(newGoblin);
        }
    }

    void RemoveGoblins(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (activeGoblins.Count > 1)
            {
                int lastIndex = activeGoblins.Count - 1;
                GameObject goblinToRemove = activeGoblins[lastIndex];

                activeGoblins.RemoveAt(lastIndex);
                Destroy(goblinToRemove);
            }
        }
    }
}