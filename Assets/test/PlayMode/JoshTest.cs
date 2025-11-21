using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
public class JoshPlayModeTestsHealth
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
        Assert.NotNull(player, "PlayerController not found in scene 'Demo'.");
        return player;
    }

    [UnityTest] // Can set health
    public IEnumerator SetHealthWorks()
    {
        var player = FindPlayer();
        player.initHealthAndSpeed(50);
        yield return null;
        Assert.AreEqual(player.getHealth(),50,"initHealthAndSpeed() is messed up.");
        player.setHealth(75);
        yield return null;
        Assert.AreEqual(player.getHealth(),75,"setHealth() is messed up.");
        player.setBaseHealth(100);
        yield return null;
        Assert.AreEqual(player.getBaseHealth(),100,"setBaseHealth() is messed up.");
        player.setMaxHealth(125);
        yield return null;
        Assert.AreEqual(player.getMaxHealth(),125,"setMaxHealth() is messed up.");
    }

    [UnityTest] // Cannot set health values to a negative number
    public IEnumerator SetHealthNegativeValue()
    {
        var player = FindPlayer();
        player.initHealthAndSpeed(50);
        yield return null;
        Assert.AreEqual(player.getHealth(),50,"initHealthAndSpeed() is messed up.");
        Assert.AreEqual(player.setBaseHealth(-1),true,"setBaseHealth() is messed up. Should not be able to set to negative.");
        Assert.AreEqual(player.setMaxHealth(-1),true,"setMaxHealth() is messed up. Should not be able to set to negative.");
        Assert.AreEqual(player.setHealth(-1),true,"setHealth() is messed up. Should not be able to set to negative.");
        Assert.AreEqual(player.setHealth(0),false,"setHealth() is messed up. Should be able to set to zero.");
    }

    [UnityTest] // Various modifyHealth functions work
    public IEnumerator ModifyHealth()
    {
        var player = FindPlayer();
        player.initHealthAndSpeed(50);
        yield return null;
        Assert.AreEqual(player.modifyHealth(-10),40,"modifyHealth() with a negative number is messed up.");
        Assert.AreEqual(player.modifyHealth(10),50,"modifyHealth() with a positive number is messed up.");
        Assert.AreEqual(player.modifyHealth(10),50,"modifyHealth() with a positive number to be greater than maxHealth is messed up.");
        Assert.AreEqual(player.modifyHealth(-60),0,"modifyHealth() down to or below zero is messed up.");
    }
}
