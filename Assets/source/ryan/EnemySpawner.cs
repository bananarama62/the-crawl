using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint; // Assign the spawn object in the Inspector
    [SerializeField] private int maxMeleeEnemies = 5; // Maximum number of melee enemies to spawn
    [SerializeField] private int maxRangedEnemies = 5; // Maximum number of ranged enemies to spawn

    private IEnemyFactory meleeFactory;
    private IEnemyFactory rangedFactory;

    void Start()
    {
        // Initialize the factories
        meleeFactory = new OrcFactory();
        rangedFactory = new ArcherFactory();

        // Spawn enemies once
        SpawnRandomEnemies();
    }

    private void SpawnRandomEnemies()
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();

        // Randomly decide the number of melee and ranged enemies to spawn
        int meleeCount = Random.Range(0, maxMeleeEnemies + 1); // Random count between 0 and maxMeleeEnemies
        int rangedCount = Random.Range(0, maxRangedEnemies + 1); // Random count between 0 and maxRangedEnemies

        for (int i = 0; i < meleeCount; i++)
        {
            Vector2 position = GetSpawnPosition();
            spawnedEnemies.Add(meleeFactory.CreateEnemy(position));
        }

        for (int i = 0; i < rangedCount; i++)
        {
            Vector2 position = GetSpawnPosition();
            spawnedEnemies.Add(rangedFactory.CreateEnemy(position));
        }
    }

    private Vector2 GetSpawnPosition()
    {
        return spawnPoint.transform.position; // Use the spawn object's position
    }
}