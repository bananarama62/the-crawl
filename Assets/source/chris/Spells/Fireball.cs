using UnityEngine;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores Fireball ability used by player when using the mage class, it inherets from effect class from josh folder
/// </summary>
public class Fireball : Effect
{
    [SerializeField] public GameObject ball;
    [SerializeField] public GameObject player;
    [SerializeField] private GameObject AimDirection;
    [SerializeField] private float distance; // Distance from player to place ability
    /// <summary>
    /// deploys fireball from player aimdirection away from player from a specified distance
    /// </summary>
    /// <returns> int </returns>
    public override int individualEffect()
    {
        Debug.Log("Casting Fireball...");
        Vector3 location = player.transform.position + AimDirection.transform.rotation*new Vector3(distance,0f,distance);
        Instantiate(ball, location, AimDirection.transform.rotation);
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
