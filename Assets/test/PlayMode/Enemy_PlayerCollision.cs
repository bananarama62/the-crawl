using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class RyanTest
{
    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        SceneManager.LoadScene("Demo");
        yield return null;  // Wait a frame for scene to load and objects to initialize

        // Setup components for enemy and player to enable physics collisions
        var enemy = FindEnemy();

        if (!enemy.TryGetComponent<Rigidbody2D>(out var enemyRb))
        {
            enemyRb = enemy.gameObject.AddComponent<Rigidbody2D>();
            enemyRb.bodyType = RigidbodyType2D.Kinematic;
            enemyRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        if (!enemy.TryGetComponent<Collider2D>(out var enemyCol))
        {
            enemyCol = enemy.gameObject.AddComponent<BoxCollider2D>();
        }
        enemyCol.isTrigger = false;

        var player = GameObject.FindGameObjectWithTag("Player");
        Assert.NotNull(player);

        if (!player.TryGetComponent<Rigidbody2D>(out var playerRb))
        {
            playerRb = player.AddComponent<Rigidbody2D>();
            playerRb.bodyType = RigidbodyType2D.Dynamic;
            playerRb.gravityScale = 0f;
        }

        if (!player.TryGetComponent<Collider2D>(out var playerCol))
        {
            playerCol = player.AddComponent<BoxCollider2D>();
        }
        playerCol.isTrigger = false;
    }

    private Enemy FindEnemy()
    {
        Enemy enemy = GameObject.FindObjectOfType<Enemy>();
        Assert.NotNull(enemy, "Enemy not found in scene");
        return enemy;
    }

    [UnityTest]
    public IEnumerator Enemy_CollidesWithPlayer()
    {
        var enemy = FindEnemy();
        var player = GameObject.FindGameObjectWithTag("Player");

        // Position enemy and player so collision can happen
        enemy.transform.position = Vector2.zero;
        player.transform.position = new Vector2(0, 6);

        bool collided = false;

        // Subscribe to detect collision without modifying Enemy
        var enemyCollider = enemy.GetComponent<Collider2D>();
        var enemyRb = enemy.GetComponent<Rigidbody2D>();

        var collisionDetector = enemy.gameObject.AddComponent<CollisionDetector>();
        collisionDetector.Setup(() =>
        {
            collided = true;
        });

        int maxSteps = 500;
        for (int i = 0; i < maxSteps; i++)
        {
            enemy.decideMove();
            enemy.move();

            yield return new WaitForFixedUpdate();

            if (collided)
                break;
        }

        Assert.IsTrue(collided, "Enemy did not collide with player during test.");
    }
}

// Helper component to detect collisions for the test
public class CollisionDetector : MonoBehaviour
{
    private System.Action onCollide;

    public void Setup(System.Action callback)
    {
        onCollide = callback;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            onCollide?.Invoke();
        }
    }
}
