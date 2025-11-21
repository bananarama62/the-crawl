using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class ChrisTest
{
    public class TestEnemy : Enemy
    {
        public override void die()
        {
            
        }
    }
    public class TestOrc : Orc
    {
        public override void die()
        {
            
        }
        public override void TakeDamage(float damage)
        {
        // Overrides to use boss health bar instead of default enemy health bar.
        modifyHealth((int)(-1*damage));
        }
    }
    public class TestSkele : SkeletonArcher
    {
        public override void die()
        {
            
        }
        public override void TakeDamage(float damage)
        {
        // Overrides to use boss health bar instead of default enemy health bar.
        modifyHealth((int)(-1*damage));
        }
    }
    public class TestBoss : BossOverlord
    {
        public override void die()
        {
            
        }
        public override void TakeDamage(float damage)
        {
        // Overrides to use boss health bar instead of default enemy health bar.
        modifyHealth((int)(-1*damage));
        }
    }
    [OneTimeSetUp]
    public void Setup()
    {
    
    }
    [Test]
    public void TestFireBallDamage()
    {
        
        GameObject FireballObj = new GameObject();
        FireballBehaviour FireLogic = FireballObj.AddComponent<FireballBehaviour>();
        FireLogic.setDamage(5);

        GameObject EnemyObj = new GameObject();
        TestEnemy enemy = EnemyObj.AddComponent<TestEnemy>();
        GameObject SliderObj = new GameObject();
        Slider slider = SliderObj.AddComponent<Slider>();
        enemy.healthBar = slider;
        enemy.initHealthAndSpeed(10);
        
        FireLogic.TestDamage(enemy);

        Assert.AreEqual(5,enemy.getHealth());

    }
    [Test]
    public void TestFireBallDamageOrc()
    {
        
        GameObject FireballObj = new GameObject();
        FireballBehaviour FireLogic = FireballObj.AddComponent<FireballBehaviour>();
        FireLogic.setDamage(5);

        GameObject EnemyObj = new GameObject();
        TestOrc enemy = EnemyObj.AddComponent<TestOrc>();
        
        enemy.initHealthAndSpeed(10);
        
        FireLogic.TestDamage(enemy);

        Assert.AreEqual(5,enemy.getHealth());

    }
    [Test]
    public void TestFireBallDamageSkele()
    {
        
        GameObject FireballObj = new GameObject();
        FireballBehaviour FireLogic = FireballObj.AddComponent<FireballBehaviour>();
        FireLogic.setDamage(5);

        GameObject EnemyObj = new GameObject();
        TestSkele enemy = EnemyObj.AddComponent<TestSkele>();
        
        enemy.initHealthAndSpeed(10);
        
        FireLogic.TestDamage(enemy);

        Assert.AreEqual(5,enemy.getHealth());

    }
    [Test]
    public void TestFireBallDamageBoss()
    {
        
        GameObject FireballObj = new GameObject();
        FireballBehaviour FireLogic = FireballObj.AddComponent<FireballBehaviour>();
        FireLogic.setDamage(5);

        GameObject BossObj = new GameObject();
        BossOverlord boss = BossObj.AddComponent<TestBoss>();
        boss.initHealthAndSpeed(10);

        FireLogic.TestDamage(boss);
        Assert.AreEqual(5,boss.getHealth());

    }
    [Test]
    public void TestTrapDamage()
    {
        
        GameObject TrapObj = new GameObject();
        TrapBehavior TrapLogic = TrapObj.AddComponent<TrapBehavior>();
        TrapLogic.setDamage(5);

        GameObject EnemyObj = new GameObject();
        TestEnemy enemy = EnemyObj.AddComponent<TestEnemy>();
        GameObject SliderObj = new GameObject();
        Slider slider = SliderObj.AddComponent<Slider>();
        enemy.healthBar = slider;
        enemy.initHealthAndSpeed(10);
        
        TrapLogic.TestDamage(enemy);

        Assert.AreEqual(5,enemy.getHealth());

    }
    [Test]
    public void TestTrapDamageOrc()
    {
        
        GameObject TrapObj = new GameObject();
        TrapBehavior TrapLogic = TrapObj.AddComponent<TrapBehavior>();
        TrapLogic.setDamage(5);

        GameObject EnemyObj = new GameObject();
        TestOrc enemy = EnemyObj.AddComponent<TestOrc>();
        enemy.initHealthAndSpeed(10);
        
        TrapLogic.TestDamage(enemy);

        Assert.AreEqual(5,enemy.getHealth());

    }
    [Test]
    public void TestTrapDamageSkele()
    {
        
        GameObject TrapObj = new GameObject();
        TrapBehavior TrapLogic = TrapObj.AddComponent<TrapBehavior>();
        TrapLogic.setDamage(5);

        GameObject EnemyObj = new GameObject();
        TestSkele enemy = EnemyObj.AddComponent<TestSkele>();
        
        enemy.initHealthAndSpeed(10);
        
        TrapLogic.TestDamage(enemy);

        Assert.AreEqual(5,enemy.getHealth());

    }
    [Test]
    public void TestTrapDamageBoss()
    {
        GameObject TrapObj = new GameObject();
        TrapBehavior TrapLogic = TrapObj.AddComponent<TrapBehavior>();
        TrapLogic.setDamage(5);

        GameObject BossObj = new GameObject();
        BossOverlord boss = BossObj.AddComponent<TestBoss>();
        boss.initHealthAndSpeed(10);

        TrapLogic.TestDamage(boss);
        Assert.AreEqual(5,boss.getHealth());

    }
}
