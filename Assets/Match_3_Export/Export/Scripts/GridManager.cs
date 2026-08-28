using UnityEngine;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    [Header("Ruudukon asetukset")]
    public int columns = 5;
    public int rows = 3; 
    public float spacing = 1.1f;

    [Header("Monistettavat 2D-objektit")]
    public List<GameObject> objectPrefabs = new List<GameObject>();

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        if (objectPrefabs == null || objectPrefabs.Count == 0)
        {

            return;
        }
        float totalWidth = (columns - 1) * spacing;
        float totalHeight = (rows - 1) * spacing;
        Vector3 centerOffset = new Vector3(totalWidth / 2f, totalHeight / 2f, 0);

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 localPosition = new Vector3(x * spacing, y * spacing, 0) - centerOffset;

                Vector3 spawnPosition = transform.position + localPosition;

                int randomIndex = Random.Range(0, objectPrefabs.Count);
                GameObject selectedPrefab = objectPrefabs[randomIndex];

                GameObject newObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
                newObject.transform.parent = this.transform;
            }
        }
    }
}