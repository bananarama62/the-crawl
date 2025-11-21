using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class testHealth
{
  private Health health; 

    [OneTimeSetUp]
    public void Setup(){
      health = new Health();
    }
    // A Test behaves as an ordinary method
    [Test]
    public void testModifyHealth()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(1,health.modifyHealth(-4,5)); // inside
        Assert.AreEqual(0,health.modifyHealth(-5,5)); // on
        Assert.AreEqual(0,health.modifyHealth(-6,5)); // outside
    }

    [Test]
    public void testSetHealth()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(false,health.setHealth(5)); // inside
        Assert.AreEqual(false,health.setHealth(0)); // on
        Assert.AreEqual(true,health.setHealth(-5)); // outside    
    }

}
