using UnityEngine;

public class Archer : Archetype
{
    private ArcherTrap trap1;
    private weapon weap1;
    public override void Initialize(PlayerController player)
    {
        base.Initialize(player);
        trap1 = player.transform.Find("Ability").GetComponent<ArcherTrap>();
        setBaseStats();
        setItems();
    }
    public override void setBaseStats()
    {
        player.initHealthAndSpeed(set_base_health: 30, speed: 10);
    }
    public override string getSprite()
    {
        //sprite info goes here
        return "name";
    }
    public override void setItems()
    {
        weap1 = player.transform.Find("Spear").GetComponent<weapon>();
    }
    public override weapon getItems()
    {
        return weap1;
    }
    public override void castSkill()
    {
        trap1.use();
    }
}
