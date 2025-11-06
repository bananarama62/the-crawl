using UnityEngine;
using UnityEngine.UI;

public interface IEnemyFactory
{
    Enemy CreateEnemy(Vector2 position);
}

public class MeleeEnemyFactory : IEnemyFactory
{
    public Enemy CreateEnemy(Vector2 position)
    {
        GameObject enemyObject = new GameObject("MeleeEnemy");
        enemyObject.AddComponent<Rigidbody2D>();
        enemyObject.AddComponent<Slider>();
        enemyObject.transform.position = position;

        MeleeEnemy meleeEnemy = enemyObject.AddComponent<MeleeEnemy>();
        meleeEnemy.initHealthAndSpeed(5, set_max_health: 10, set_current_health: 10, speed: 2f);
        meleeEnemy.initMovement();

        return meleeEnemy;
    }
}

public class RangedEnemyFactory : IEnemyFactory
{
    public Enemy CreateEnemy(Vector2 position)
    {
        GameObject enemyObject = new GameObject("RangedEnemy");
        enemyObject.AddComponent<Rigidbody2D>();
        enemyObject.AddComponent<Slider>();
        enemyObject.transform.position = position;

        RangedEnemy rangedEnemy = enemyObject.AddComponent<RangedEnemy>();
        rangedEnemy.initHealthAndSpeed(3, set_max_health: 6, set_current_health: 6, speed: 1.5f);
        rangedEnemy.initMovement();

        return rangedEnemy;
    }
}