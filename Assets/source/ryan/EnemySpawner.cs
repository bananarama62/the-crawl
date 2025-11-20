using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint; // Assign the spawn object in the Inspector

    private EnemyFactory meleeFactory;
    private EnemyFactory rangedFactory;

    void Start()
    {
        // Initialize the factories
        meleeFactory = new OrcFactory();
        rangedFactory = new ArcherFactory();

        // Spawn one random enemy
        SpawnRandomEnemy();
    }

    private void SpawnRandomEnemy()
    {
        EnemyFactory selectedFactory = Random.Range(0, 2) == 0 ? meleeFactory : rangedFactory; // Randomly select a factory
        Vector2 position = GetSpawnPosition();
        selectedFactory.CreateEnemy(position); // Use the factory to create and spawn the enemy
    }

    private Vector2 GetSpawnPosition()
    {
        return spawnPoint.transform.position; // Use the spawn object's position
    }
}