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
        weap1 = player.transform.Find("sword").GetComponent<weapon>();
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
