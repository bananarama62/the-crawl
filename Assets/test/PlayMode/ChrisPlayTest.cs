using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Assertions.Must;
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
        var player = GameObject.FindObjectOfType<PlayerController>();
        Assert.NotNull(player, "PlayerController not found in scene!");
        var MainMenu = GameObject.FindObjectOfType<MainMenuScript>();
        MainMenu.setScene(false);
        player.setClass(new Mage());
        yield return null;

        player.PlayerClass.castSkill();

        yield return new WaitForSeconds(0.1f);

        var spawned = GameObject.FindObjectOfType<Fireball>();
        Assert.NotNull(spawned, "Projectile should have been spawned by Fireball.");
    }

    [UnityTest]
    public IEnumerator Fireball_CannotCast_WhenOnCooldown()
    {
        var player = GameObject.FindObjectOfType<PlayerController>();
        Assert.NotNull(player, "PlayerController not found in scene!");
        var MainMenu = GameObject.FindObjectOfType<MainMenuScript>();
        MainMenu.setScene(false);
        player.setClass(new Mage());
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

    [UnityTest]
    public IEnumerator Trap_SpawnsProjectile_WhenCastSkillIsCalled()
    {
        var player = GameObject.FindObjectOfType<PlayerController>();
        Assert.NotNull(player, "PlayerController not found in scene!");
        var MainMenu = GameObject.FindObjectOfType<MainMenuScript>();
        MainMenu.setScene(false);
        player.setClass(new Archer());
        yield return null;

        player.PlayerClass.castSkill();

        yield return new WaitForSeconds(0.1f);

        var spawned = GameObject.FindObjectOfType<ArcherTrap>();
        Assert.NotNull(spawned, "Projectile should have been spawned by Fireball.");
    }
    [UnityTest]
    public IEnumerator Trap_CannotCast_WhenOnCooldown()
    {
        var player = GameObject.FindObjectOfType<PlayerController>();
        Assert.NotNull(player, "PlayerController not found in scene!");
        var MainMenu = GameObject.FindObjectOfType<MainMenuScript>();
        MainMenu.setScene(false);
        player.setClass(new Archer());
        // Cast once (starts cooldown)
        player.PlayerClass.castSkill();
        yield return null;

        // Try to cast again immediately
        var beforeCount = GameObject.FindObjectsOfType<ArcherTrap>().Length;
        player.PlayerClass.castSkill();
        yield return new WaitForSeconds(0.1f);
        var afterCount = GameObject.FindObjectsOfType<ArcherTrap>().Length;

        Assert.AreEqual(beforeCount, afterCount, "No new fireball should spawn while on cooldown.");
    }
}
