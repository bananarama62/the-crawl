using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
public class powerupTests
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
    public IEnumerator healthModDoesntOverheal()
    {
        var player = FindPlayer();
        player.maxHealth = 100;
        player.health = 95;

        var heal = ScriptableObject.CreateInstance<HealthModifier>();
        heal.healthValue = 50;

        heal.Activate(player.gameObject);
        yield return null;

        Assert.AreEqual(100, player.health, "Healing should not overheal at maxHealth.");
        Object.DestroyImmediate(heal);
    }
    [UnityTest]
    public IEnumerator WeaponModifier_MultipliesDamage_OnChildWeapon()
    {
        var player = FindPlayer();
        var weap = findWeapon(player);
        weap.damage = 1;
        yield return null;

        var mod = ScriptableObject.CreateInstance<WeaponModifier>();
        mod.damageMultiplier = 2f;

        mod.Activate(player.gameObject);
        yield return null;

        Assert.AreEqual(3f, weap.damage, 1e-5, "Damage should be baseDamage + multiplier.");
        Object.DestroyImmediate(mod);
    }
}