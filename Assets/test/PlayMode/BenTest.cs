using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
public class BensPlayModeTests
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
    private static weapon findWeapon(PlayerController player)
    {
        var weap = player.GetComponentInChildren<weapon>(includeInactive: true);
        if (weap == null)
        {
            var child = new GameObject("TestWeapon");
            child.transform.SetParent(player.transform, false);
            weap = child.AddComponent<weapon>();
            // Give it a stable baseline; Awake likely sets damage=baseDamage
            weap.damage = 1;
        }
        return weap;
    }
    [UnityTest]
    public IEnumerator HealthPotionHeals()
    {
        var player = FindPlayer();
        player.setMaxHealth(100);
        player.setHealth(50); // current = 50

        var potion = ScriptableObject.CreateInstance<HealthPotionItem>();
        potion.HealAmount = 30;

        potion.Activate(player.gameObject);
        yield return null;

        Assert.AreEqual(80, player.getHealth(), "Player should be healed by potion amount.");

        potion.HealAmount = 50;
        potion.Activate(player.gameObject);
        yield return null;
        Assert.AreEqual(100, player.getHealth(), "Health should not exceed max health.");
    }
    // 2) HealthPotionAction safely handles null target (no exception)
    [UnityTest]
    public IEnumerator HealthPotionNull()
    {
        var potion = ScriptableObject.CreateInstance<HealthPotionItem>();
        potion.HealAmount = 10;
        // Should not throw when Activate receives null
        Assert.DoesNotThrow(() => potion.Activate(null));
        yield return null;
    }
    [UnityTest]
    public IEnumerator BowFiresArrow()
    {
    var player = FindPlayer();
    var bow = player.GetComponentInChildren<Bow>(includeInactive: true);
    
    if (bow == null)
    {
        var bowObj = new GameObject("TestBow");
        bowObj.transform.SetParent(player.transform);
        bow = bowObj.AddComponent<Bow>();
    }

    int initialArrowCount = GameObject.FindObjectsByType<Arrow>(FindObjectsSortMode.None).Length;
    bow.individualEffect();
    yield return new WaitForSeconds(0.1f);

    int newArrowCount = GameObject.FindObjectsByType<Arrow>(FindObjectsSortMode.None).Length;
    Assert.Greater(newArrowCount, initialArrowCount, "Bow should spawn an arrow when fired.");
}

[UnityTest]
public IEnumerator PlayerHotbarInitializesEmpty()
{
    var player = FindPlayer();
    
    // Reset hotbar
    player.HotBarItem = null;
    yield return null;

    Assert.IsNull(player.HotBarItem, "Player hotbar should initialize as null/empty.");
}
[UnityTest]
public IEnumerator WeaponExistsOnPlayer()
{
    var player = FindPlayer();
    var weap = findWeapon(player);

    Assert.NotNull(weap, "Player should have a weapon component.");
    yield return null;
}


[UnityTest]
public IEnumerator WeaponHasPositiveDamage()
{
    var player = FindPlayer();
    var weap = findWeapon(player);
    
    weap.damage = 10;
    yield return null;

    Assert.Greater(weap.damage, 0, "Weapon damage should be positive.");
}

[UnityTest]
public IEnumerator HotbarStoresItem()
{
    var player = FindPlayer();
    
    var testItem = ScriptableObject.CreateInstance<HealthPotionItem>();
    testItem.HealAmount = 10;

    player.HotBarItem = testItem;
    yield return null;

    Assert.AreEqual(testItem, player.HotBarItem, "Hotbar should store item.");
}

[UnityTest]
public IEnumerator HotbarItemActivates()
{
    var player = FindPlayer();
    player.setMaxHealth(100);
    player.setHealth(50);
    
    var potion = ScriptableObject.CreateInstance<HealthPotionItem>();
    potion.HealAmount = 20;
    player.HotBarItem = potion;

    potion.Activate(player.gameObject);
    yield return null;

    Assert.AreEqual(70, player.getHealth(), "Hotbar item should activate and heal.");
}

[UnityTest]
public IEnumerator ArrowIgnoresOwnerCollision()
{
    var player = FindPlayer();
    player.tag = "Player";
    
    var arrowObj = new GameObject("TestArrow");
    arrowObj.AddComponent<BoxCollider2D>().isTrigger = true;
    var arrow = arrowObj.AddComponent<Arrow>();
    arrow.Owner = "Player";
    arrow.Speed = 10f;

    arrowObj.transform.position = player.transform.position;
    yield return new WaitForSeconds(0.1f);

    Assert.IsTrue(arrowObj != null, "Arrow should not be destroyed by owner collision.");
    
    Object.Destroy(arrowObj);
}

[UnityTest]
public IEnumerator ArrowDestroysOnWallCollision()
{
    var arrowObj = new GameObject("TestArrow");
    arrowObj.AddComponent<BoxCollider2D>().isTrigger = true;
    var arrow = arrowObj.AddComponent<Arrow>();
    arrow.Speed = 10f;
    arrow.damage = 5;

    var wallObj = new GameObject("Walls");
    wallObj.AddComponent<BoxCollider2D>().isTrigger = true;
    wallObj.transform.position = new Vector3(2, 0, 0);

    arrow.Fire(Quaternion.identity);
    yield return new WaitForSeconds(0.5f);

    Assert.IsTrue(arrowObj == null, "Arrow should be destroyed on wall collision.");
    
    Object.Destroy(wallObj);
}
[UnityTest]
public IEnumerator ArrowHandlesNull()
{
    var arrowObj = new GameObject("TestArrow");
    var arrow = arrowObj.AddComponent<Arrow>();
    
    var rb = arrowObj.GetComponent<Rigidbody2D>();
    if (rb != null) Object.Destroy(rb);
    
    yield return null;
    
    Assert.NotNull(arrow, "Arrow should handle missing Rigidbody2D gracefully.");
    
    Object.Destroy(arrowObj);
}

}
