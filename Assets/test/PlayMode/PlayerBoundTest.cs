using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class PlayerBoundTest
{
    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        SceneManager.LoadScene("Demo");
        yield return null;
    }

    private static PlayerController FindPlayer()
    {
        var player = GameObject.FindFirstObjectByType<PlayerController>();
        Assert.NotNull(player, "PlayerController not found in scene 'Demo'");
        return player;
    }

    private static GameObject FindTilemap()
    {
        //var tilemap = GameObject.FindFirstObjectByType<Tilemap>();

        var tilemap = GameObject.Find("Walls");
        Assert.NotNull(tilemap, "Tilemap not found in scene 'Demo'");
        return tilemap;
    }
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerCollision()
    {
        
        var player = FindPlayer();
        var tilemap = FindTilemap();
        var playerPosition = player.transform.position;
        var tilemapPosition = tilemap.transform.position;

        Assert.AreEqual(playerPosition, tilemapPosition, "Player is outside tilemap position!");
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
