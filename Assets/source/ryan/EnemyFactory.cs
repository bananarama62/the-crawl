using UnityEngine;
using UnityEngine.UI;

public interface EnemyFactory
{
    // Method to create an enemy at a specified position
    Enemy CreateEnemy(Vector2 position);
}

// Factory for creating Orc enemies
public class OrcFactory : EnemyFactory
{
    public Enemy CreateEnemy(Vector2 position)
    {
        GameObject enemyObject = new GameObject("Orc");
        enemyObject.AddComponent<Rigidbody2D>();
        enemyObject.AddComponent<Slider>();
        enemyObject.transform.position = position;

        // Initialize Orc-specific properties
        Orc meleeEnemy = enemyObject.AddComponent<Orc>();
        meleeEnemy.initHealthAndSpeed(5, set_max_health: 10, set_current_health: 10, speed: 2f);
        meleeEnemy.initMovement();

        return meleeEnemy;
    }
}

// Factory for creating Skeleton Archer enemies
public class ArcherFactory : EnemyFactory
{
    public Enemy CreateEnemy(Vector2 position)
    {
        GameObject enemyObject = new GameObject("SkeletonArcher");
        enemyObject.AddComponent<Rigidbody2D>();
        enemyObject.AddComponent<Slider>();
        enemyObject.transform.position = position;

        // Initialize Skeleton Archer-specific properties
        SkeletonArcher rangedEnemy = enemyObject.AddComponent<SkeletonArcher>();
        rangedEnemy.initHealthAndSpeed(3, set_max_health: 6, set_current_health: 6, speed: 1.5f);
        rangedEnemy.initMovement();

        return rangedEnemy;
    }
}