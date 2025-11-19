using UnityEngine;

public class Mage : Archetype
{
    private Fireball fire1;
    private weapon weap1;
    public override void Initialize(PlayerController player)
    {
        base.Initialize(player);
        fire1 = player.transform.Find("Ability").GetComponent<Fireball>();
        setBaseStats();
        setItems();
    }
    public override void setBaseStats()
    {
        player.initHealthAndSpeed(set_base_health: 15, speed: 5);
    }
    public override string getSprite()
    {
        //sprite info goes here
        return "name";
    }
    public override void setItems()
    {
        Transform Wep;

        Wep = player.transform.Find("bow");
        if (Wep != null) Wep.gameObject.SetActive(false);

        Wep = player.transform.Find("sword");
        if (Wep != null) Wep.gameObject.SetActive(false);

        Wep = player.transform.Find("spear");
        if (Wep != null) Wep.gameObject.SetActive(false);

        Transform bowTransform = player.transform.Find("dagger");
        if (bowTransform != null)
        {
            bowTransform.gameObject.SetActive(true);
            weap1 = bowTransform.GetComponent<weapon>();
            if (weap1 != null)
            {
                // Make the player use this weapon immediately
                player.EquipWeapon(weap1);

                // Update UI icon if UIHandler is available
                if (UIHandler.instance != null)
                {
                    UIHandler.instance.setIcon(1, weap1.icon);
                }
            }
        }
    }
    public override weapon getItems()
    {
        return weap1;
    }
    public override void castSkill()
    {
        fire1.use();
    }
}
