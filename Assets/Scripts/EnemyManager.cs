using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform playerTarget;

    public GameObject[] enemies;
    private List<GameObject> enemiesList = new List<GameObject>();
    [SerializeField] private int enemiesCap = 50;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnRadius = 20f;
    [SerializeField] private float spawnRate = 0.7f;
    [SerializeField] private float spawnDelay = 5f;

    void Start()
    {
        if (enemiesList.Count < enemiesCap)
        {
            InvokeRepeating("SpawnEnemies", spawnRate, spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemiesList.RemoveAll(e => e == null); // Removes enemies on the list that were destroyed
    }

    void SpawnEnemies()
    {
        var angle = Random.Range(0f, 360f);
        var distance = Random.Range(spawnRadius * 0.5f, spawnRadius);

        Vector3 convertPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * distance, 0f, Mathf.Sin(angle * Mathf.Deg2Rad) * distance);
        Vector3 spawnPosition = playerTarget.position + convertPosition;

        GameObject newEnemy = Instantiate(enemies[GetRandom()], spawnPosition, Quaternion.identity);
        enemiesList.Add(newEnemy);
    }

    int GetRandom()
    {
        return Random.Range(0, enemies.Length);
    }
}
