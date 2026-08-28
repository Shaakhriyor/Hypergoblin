using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [Header("Prefabit")]
    [SerializeField] private List<GameObject> prefabsToSpawn = new List<GameObject>(); // Lista prefabeille

    [Header("Ajoitus")]
    [SerializeField] private float spawnInterval = 2.0f; // Aikav‰li sekunteina

    [Header("Et‰isyys Z-akselilla")]
    [SerializeField] private float minZGap = 5f;  // Minimiv‰limatka edelliseen objektiin
    [SerializeField] private float maxZGap = 15f; // Maksimiv‰limatka edelliseen objektiin

    [Header("Kiinte‰t koordinaatit")]
    [SerializeField] private float spawnX = 0f; // Kiinte‰ X-koordinaatti (esim. tien keskell‰)
    [SerializeField] private float spawnY = 0f; // Kiinte‰ korkeus

    private float timer;
    private float currentZ; // Pit‰‰ kirjaa siit‰, miss‰ viimeisin spawn tapahtui

    void Start()
    {
        timer = spawnInterval;

        // Aloitetaan spawn-pisteen laskeminen skriptin oman objektin Z-sijainnista
        currentZ = transform.position.z;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnNextPrefab();
            timer = spawnInterval;
        }
    }

    void SpawnNextPrefab()
    {
        if (prefabsToSpawn.Count == 0)
        {
            Debug.LogWarning("Prefab-lista on tyhj‰!");
            return;
        }

        // Valitaan satunnainen prefab listasta
        int randomIndex = Random.Range(0, prefabsToSpawn.Count);
        GameObject selectedPrefab = prefabsToSpawn[randomIndex];

        // Lis‰t‰‰n nykyiseen Z-sijaintiin satunnainen positiivinen v‰limatka (aina eteenp‰in)
        float randomGap = Random.Range(minZGap, maxZGap);
        currentZ += randomGap;

        // Luodaan uusi sijainti, jossa X ja Y pysyv‰t samoina
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, currentZ);

        // Luodaan objekti
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}