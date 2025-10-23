using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class Enemy_WallCollision
{
    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        SceneManager.LoadScene("Demo");
        yield return null;
    }

    private static Enemy FindEnemy()
    {
        var enemy = GameObject.FindObjectOfType<Enemy>();
        Assert.NotNull(enemy, "Enemy not found in scene 'Demo'");
        return enemy;
    }

    private static GameObject FindTilemap()
    {
        var tilemap = GameObject.Find("Walls");
        Assert.NotNull(tilemap, "Tilemap 'Walls' not found in scene 'Demo'");
        return tilemap;
    }

    [UnityTest]
    public IEnumerator EnemyCollidesWithWall()
    {
        var enemy = FindEnemy();
        var tilemapObject = FindTilemap();
        Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
        Assert.NotNull(tilemap, "Tilemap component not found on 'Walls'");

        // Position enemy close to wall tilemap to force collision
        enemy.transform.position = tilemapObject.transform.position + new Vector3(1f, 5, 0);

        bool collisionDetected = false;

        // Attach helper to detect collision with wall tilemap
        var detector = enemy.gameObject.AddComponent<WallCollisionDetector>();
        detector.Setup(() =>
        {
            collisionDetected = true;
        });

        int maxSteps = 200;
        for (int i = 0; i < maxSteps; i++)
        {
            enemy.decideMove();
            enemy.move();

            yield return new WaitForFixedUpdate();

            if (collisionDetected)
                break;
        }

        Assert.IsTrue(collisionDetected, "Enemy did not collide with the wall Tilemap during test.");
    }
}

public class WallCollisionDetector : MonoBehaviour
{
    private System.Action onCollide;

    public void Setup(System.Action callback)
    {
        onCollide = callback;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Walls" || collision.collider.GetComponent<Tilemap>() != null)
        {
            onCollide?.Invoke();
        }
    }
}
