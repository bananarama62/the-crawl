using UnityEngine;

public class ArcherTrap : Effect
{
    [SerializeField] public GameObject trap;
    [SerializeField] public GameObject player;
    [SerializeField] public Transform spawnPoint;
    public override int individualEffect()
    {
        Debug.Log("Deploying Trap ...");
        Instantiate(trap, spawnPoint.position, player.transform.rotation);
        return 1;
    }
    void Awake()
    {
        base.init();
    } 
}
