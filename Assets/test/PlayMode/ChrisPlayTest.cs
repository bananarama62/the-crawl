using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ChrisPlayTest
{
    [UnitySetUp]
    public IEnumerator LoadScene()
    {
        
        SceneManager.LoadScene("Demo");
        yield return null;
    }

    [UnityTest]
    public IEnumerator Fireball_SpawnsProjectile_WhenCastSkillIsCalled()
    {
        // Find the Player object in your loaded scene
        var player = GameObject.FindObjectOfType<PlayerController>();
        Assert.NotNull(player, "PlayerController not found in scene!");

        // Wait for any initialization logic
        yield return null;

        // Act — simulate casting skill
        player.PlayerClass.castSkill();

        // Wait for projectile spawn
        yield return new WaitForSeconds(0.1f);

        // Assert — check that a projectile was created
        var spawned = GameObject.FindObjectOfType<Fireball>();
        Assert.NotNull(spawned, "Projectile should have been spawned by Fireball.");
    }

    [UnityTest]
    public IEnumerator Fireball_CannotCast_WhenOnCooldown()
    {
        var player = GameObject.FindObjectOfType<PlayerController>();
        Assert.NotNull(player, "PlayerController not found in scene!");

        // Cast once (starts cooldown)
        player.PlayerClass.castSkill();
        yield return null;

        // Try to cast again immediately
        var beforeCount = GameObject.FindObjectsOfType<Fireball>().Length;
        player.PlayerClass.castSkill();
        yield return new WaitForSeconds(0.1f);
        var afterCount = GameObject.FindObjectsOfType<Fireball>().Length;

        Assert.AreEqual(beforeCount, afterCount, "No new fireball should spawn while on cooldown.");
    }
}
