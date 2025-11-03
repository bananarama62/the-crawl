using UnityEngine;

public class ArcherTrap : Effect
{
    [SerializeField] public GameObject trap;
    [SerializeField] public GameObject player;
    //[SerializeField] public Transform spawnPoint; // obsolete
    [SerializeField] private GameObject AimDirection;
    [SerializeField] private float distance; // Distance from player to place trap
    public override int individualEffect()
    {
        Debug.Log("Deploying Trap ...");
        //Instantiate(trap, spawnPoint.position, player.transform.rotation);
        Vector3 location = player.transform.position + AimDirection.transform.rotation*new Vector3(distance,0f,distance);
        Instantiate(trap, location, player.transform.rotation);
        return 1;
    }
    void Awake()
    {
        base.init();
    } 
}
