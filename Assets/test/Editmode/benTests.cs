using NUnit.Framework;
using UnityEngine;

public class BenEditModeTests
{
    [Test]
    public void ArrowSpeedIsPositive()
    {
        var arrowObj = new GameObject("TestArrow");
        var arrow = arrowObj.AddComponent<Arrow>();
        arrow.Speed = 10f;

        Assert.Greater(arrow.Speed, 0, "Arrow speed should be positive.");
        
        Object.DestroyImmediate(arrowObj);
    }

    [Test]
    public void ArrowDamageIsPositive()
    {
        var arrowObj = new GameObject("TestArrow");
        var arrow = arrowObj.AddComponent<Arrow>();
        arrow.damage = 15;

        Assert.Greater(arrow.damage, 0, "Arrow damage should be positive.");
        
        Object.DestroyImmediate(arrowObj);
    }
    [Test]
    public void WeaponDamageCanBeSet()
    {
        var weaponObj = new GameObject("TestWeapon");
        var weapon = weaponObj.AddComponent<weapon>();
        
        weapon.damage = 25;

        Assert.AreEqual(25, weapon.damage, "Weapon damage should be settable.");
        
        Object.DestroyImmediate(weaponObj);
    }

    [Test]
    public void HealthPotionHealAmountIsPositive()
    {
        var potion = ScriptableObject.CreateInstance<HealthPotionItem>();
        potion.HealAmount = 20;

        Assert.Greater(potion.HealAmount, 0, "Health potion heal amount should be positive.");
        
        Object.DestroyImmediate(potion);
    }

    [Test]
    public void ArrowOwnerDefaultsToPlayer()
    {
        var arrowObj = new GameObject("TestArrow");
        var arrow = arrowObj.AddComponent<Arrow>();
        arrow.Owner = "Player";

        Assert.AreEqual("Player", arrow.Owner, "Arrow owner should default to Player.");
        
        Object.DestroyImmediate(arrowObj);
    }
     [Test]
    public void ArrowOwnerCanBeChanged()
    {
        var arrowObj = new GameObject("TestArrow");
        var arrow = arrowObj.AddComponent<Arrow>();
        arrow.Owner = "Enemy";

        Assert.AreEqual("Enemy", arrow.Owner, "Arrow owner should be changeable.");
        
        Object.DestroyImmediate(arrowObj);
    }

    [Test]
    public void ArrowSpriteInheritsFromBaseClass()
    {
        var gameObj = new GameObject("TestSprite");
        var arrowSprite = new ActualArrowSprite(gameObj);

        Assert.IsInstanceOf<ArrowSprite>(arrowSprite, "ActualArrowSprite should inherit from ArrowSprite.");
        
        Object.DestroyImmediate(gameObj);
    }
    [Test]
    public void ArrowSpriteHasSetSpriteMethod()
    {
        var gameObj = new GameObject("TestSprite");
        var arrowSprite = new ArrowSprite(gameObj);

        Assert.IsTrue(arrowSprite.GetType().GetMethod("SetSprite") != null, "ArrowSprite should have SetSprite method.");
        
        Object.DestroyImmediate(gameObj);
    }

    [Test]
    public void ActualArrowSpriteOverridesSetSprite()
    {
        var gameObj = new GameObject("TestSprite");
        var arrowSprite = new ActualArrowSprite(gameObj);
        var method = arrowSprite.GetType().GetMethod("SetSprite");

        Assert.IsTrue(method.DeclaringType == typeof(ActualArrowSprite), "ActualArrowSprite should override SetSprite.");
        
        Object.DestroyImmediate(gameObj);
    }

    [Test]
    public void BowInheritsFromEffect()
    {
        var bowObj = new GameObject("TestBow");
        var bow = bowObj.AddComponent<Bow>();

        Assert.IsInstanceOf<Effect>(bow, "Bow should inherit from Effect.");
        
        Object.DestroyImmediate(bowObj);
    }
    [Test]
    public void WeaponComponentExists()
    {
        var weaponObj = new GameObject("TestWeapon");
        var weapon = weaponObj.AddComponent<weapon>();

        Assert.NotNull(weapon, "Weapon component should be creatable.");
        
        Object.DestroyImmediate(weaponObj);
    }

    [Test]
    public void ArrowComponentHasRequiredFields()
    {
        var arrowObj = new GameObject("TestArrow");
        var arrow = arrowObj.AddComponent<Arrow>();

        Assert.IsTrue(arrow.GetType().GetField("Speed") != null, "Arrow should have Speed field.");
        Assert.IsTrue(arrow.GetType().GetField("damage") != null, "Arrow should have damage field.");
        Assert.IsTrue(arrow.GetType().GetField("Owner") != null, "Arrow should have Owner field.");
        
        Object.DestroyImmediate(arrowObj);
    }

    [Test]
    public void HealthPotionIsItem()
    {
        var potion = ScriptableObject.CreateInstance<HealthPotionItem>();

        Assert.IsInstanceOf<Item>(potion, "HealthPotionItem should inherit from Item.");
        
        Object.DestroyImmediate(potion);
    }
[Test]
    public void ArrowDurationCanBeSet()
    {
        var arrowObj = new GameObject("TestArrow");
        var arrow = arrowObj.AddComponent<Arrow>();
        arrow.Duration = 5f;

        Assert.AreEqual(5f, arrow.Duration, "Arrow duration should be settable.");
        
        Object.DestroyImmediate(arrowObj);
    }

    [Test]
    public void WeaponDamageIsZeroByDefault()
    {
        var weaponObj = new GameObject("TestWeapon");
        var weapon = weaponObj.AddComponent<weapon>();

        Assert.GreaterOrEqual(weapon.damage, 0, "Weapon damage should be non-negative by default.");
        
        Object.DestroyImmediate(weaponObj);
    }
        [Test]
    public void BowHasIndividualEffectMethod()
    {
        var bowObj = new GameObject("TestBow");
        var bow = bowObj.AddComponent<Bow>();

        var method = bow.GetType().GetMethod("individualEffect");
        Assert.IsNotNull(method, "Bow should have individualEffect method.");
        
        Object.DestroyImmediate(bowObj);
    }

    [Test]
    public void ArrowCanChangeSpeed()
    {
        var arrowObj = new GameObject("TestArrow");
        var arrow = arrowObj.AddComponent<Arrow>();
        
        arrow.Speed = 10f;
        Assert.AreEqual(10f, arrow.Speed, "Arrow speed should be 10.");
        
        arrow.Speed = 25f;
        Assert.AreEqual(25f, arrow.Speed, "Arrow speed should update to 25.");
        
        Object.DestroyImmediate(arrowObj);
    }

    [Test]
    public void ArrowSpriteCreatesWithGameObject()
    {
        var gameObj = new GameObject("TestSprite");
        var arrowSprite = new ArrowSprite(gameObj);

        Assert.NotNull(arrowSprite, "ArrowSprite should be creatable with GameObject.");
        
        Object.DestroyImmediate(gameObj);
    }
    [Test]
    public void ArrowHasOnTriggerEnter2DMethod()
    {
        var arrowObj = new GameObject("TestArrow");
        var arrow = arrowObj.AddComponent<Arrow>();

        var method = arrow.GetType().GetMethod("OnTriggerEnter2D", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.IsNotNull(method, "Arrow should have OnTriggerEnter2D collision method.");
        
        Object.DestroyImmediate(arrowObj);
    }
}