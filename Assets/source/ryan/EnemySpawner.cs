using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private IEnemyFactory meleeFactory;
    private IEnemyFactory rangedFactory;

    public EnemySpawner(IEnemyFactory meleeFactory, IEnemyFactory rangedFactory)
    {
        this.meleeFactory = meleeFactory;
        this.rangedFactory = rangedFactory;
    }

    public List<Enemy> SpawnEnemies(int meleeCount, int rangedCount)
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();

        for (int i = 0; i < meleeCount; i++)
        {
            Vector2 position = GetRandomPosition();
            spawnedEnemies.Add(meleeFactory.CreateEnemy(position));
        }

        for (int i = 0; i < rangedCount; i++)
        {
            Vector2 position = GetRandomPosition();
            spawnedEnemies.Add(rangedFactory.CreateEnemy(position));
        }

        return spawnedEnemies;
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        return new Vector2(x, y);
    }
}