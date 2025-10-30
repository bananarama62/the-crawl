using UnityEngine;

public class Archer : Archetype
{
    private ArcherTrap trap1;
    public override void Initialize(PlayerController player)
    {
        base.Initialize(player);
        trap1 = player.transform.Find("Ability").GetComponent<ArcherTrap>();
        setBaseStats();
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
        //items go here
    }
    public override void castSkill()
    {
        trap1.use();
    }
}
