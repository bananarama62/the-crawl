using UnityEngine;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores ArcherTrap ability used by player when using the archer class, it inherets from effect class from josh folder
/// </summary>
public class ArcherTrap : Effect
{
    [SerializeField] public GameObject trap;
    [SerializeField] public GameObject player;
    [SerializeField] private GameObject AimDirection;
    [SerializeField] private float distance; // Distance from player to place trap
    /// <summary>
    /// deploys trap from player aimdirection away from player from a specified distance
    /// </summary>
    /// <returns> int </returns>
    public override int individualEffect()
    {
        Debug.Log("Deploying Trap ...");
        Vector3 location = player.transform.position + AimDirection.transform.rotation*new Vector3(distance,0f,distance);
        Instantiate(trap, location, player.transform.rotation);
        return 1;
    }
    /// <summary>
    /// runs base init function from effect parent
    /// </summary>
    void Awake()
    {
        base.init();
    } 
}
