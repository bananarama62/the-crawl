using UnityEngine;

public class Mage : Archetype
{
    private Fireball fire1;
    public override void Initialize(PlayerController player)
    {
        base.Initialize(player);
        fire1 = player.transform.Find("Ability").GetComponent<Fireball>();
        setBaseStats();
    }
    public override void setBaseStats()
    {
        player.initHealthAndSpeed(set_base_health: 30, speed: 5);
    }
    public override string getSprite()
    {
        //sprite info goes here
        return "name";
    }
    public override void setItems()
    {
        //items go here
    }
    public override void castSkill()
    {
        fire1.use();
    }
}
