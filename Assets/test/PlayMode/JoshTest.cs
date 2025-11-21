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
        Assert.AreEqual(50,player.getHealth(),"initHealthAndSpeed() is messed up.");
        player.setHealth(75);
        yield return null;
        Assert.AreEqual(75,player.getHealth(),"setHealth() is messed up.");
        player.setBaseHealth(100);
        yield return null;
        Assert.AreEqual(100,player.getBaseHealth(),"setBaseHealth() is messed up.");
        player.setMaxHealth(125);
        yield return null;
        Assert.AreEqual(125,player.getMaxHealth(),"setMaxHealth() is messed up.");
    }

    [UnityTest] // Cannot set health values to a negative number
    public IEnumerator SetHealthNegativeValue()
    {
        var player = FindPlayer();
        player.initHealthAndSpeed(50);
        yield return null;
        Assert.AreEqual(50,player.getHealth(),"initHealthAndSpeed() is messed up.");
        Assert.AreEqual(true,player.setBaseHealth(-1),"setBaseHealth() is messed up. Should not be able to set to negative.");
        Assert.AreEqual(true,player.setMaxHealth(-1),"setMaxHealth() is messed up. Should not be able to set to negative.");
        Assert.AreEqual(true,player.setHealth(-1),"setHealth() is messed up. Should not be able to set to negative.");
        Assert.AreEqual(false,player.setHealth(0),"setHealth() is messed up. Should be able to set to zero.");
    }

    [UnityTest] // Various modifyHealth functions work
    public IEnumerator ModifyHealth()
    {
        var player = FindPlayer();
        player.initHealthAndSpeed(50);
        yield return null;
        Assert.AreEqual(40,player.modifyHealth(-10),"modifyHealth() with a negative number is messed up.");
        Assert.AreEqual(50,player.modifyHealth(10),"modifyHealth() with a positive number is messed up.");
        Assert.AreEqual(50,player.modifyHealth(10),"modifyHealth() with a positive number to be greater than maxHealth is messed up.");
        Assert.AreEqual(0,player.modifyHealth(-60),"modifyHealth() down to or below zero is messed up.");
    }

    [UnityTest] // Various modifyHealthByPercentage functions work
    public IEnumerator ModifyHealthByPercentage()
    {
        var player = FindPlayer();
        player.initHealthAndSpeed(100);
        yield return null;
        Assert.AreEqual(40,player.modifyHealthByPercentage(-60),"modifyHealthByPercentage() with a negative number is messed up.");
        Assert.AreEqual(50,player.modifyHealthByPercentage(10),"modifyHealthByPercentage() with a positive number is messed up.");
        Assert.AreEqual(60,player.modifyHealthByPercentage(20,false),"modifyHealthByPercentage() with a positive number and ofMaxHealth=false is messed up.");
        Assert.AreEqual(54,player.modifyHealthByPercentage(-10,false),"modifyHealthByPercentage() with a negative number and ofMaxHealth=false is messed up.");
        Assert.AreEqual(100,player.modifyHealthByPercentage(100),"modifyHealthByPercentage() with a positive number to be greater than maxHealth is messed up.");
        player.modifyHealthByPercentage(-20);
        Assert.AreEqual(100,player.modifyHealthByPercentage(200),"modifyHealthByPercentage() with a percentage greater than 100% is messed up.");
        player.modifyHealthByPercentage(-20);
        Assert.AreEqual(100,player.modifyHealthByPercentage(30,false),"modifyHealthByPercentage() with a positive number and ofMaxHealth=false to be greater than maxHealth is messed up.");
        Assert.AreEqual(0,player.modifyHealthByPercentage(-120),"modifyHealthByPercentage() down to or below zero is messed up.");
    }
}
