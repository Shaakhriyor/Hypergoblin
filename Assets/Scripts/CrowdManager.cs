using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    public static CrowdManager Instance;
    public GameObject goblinPrefab;
    public Transform playerTransform;

    private List<GameObject> activeGoblins = new List<GameObject>();

    void Awake() => Instance = this;

    public void ModifyCrowd(GateType type, int amount)
    {
        int targetCount = activeGoblins.Count;

        switch (type)
        {
            case GateType.Add:
                targetCount += amount;
                break;
            case GateType.Multiply:
                targetCount *= amount;
                break;
            case GateType.Subtract:
                targetCount = Mathf.Max(0, targetCount - amount);
                break;
        }

        UpdateGoblinCount(targetCount);
    }

    void UpdateGoblinCount(int targetCount)
    {
        // Spawn missing goblins around the leader position
        while (activeGoblins.Count < targetCount)
        {
            Vector3 randomOffset = Random.insideUnitSphere * 1.5f;
            randomOffset.y = 0; // Keep on track height

            GameObject newGoblin = Instantiate(goblinPrefab, playerTransform.position + randomOffset, Quaternion.identity, playerTransform);
            activeGoblins.Add(newGoblin);
        }
    }
}