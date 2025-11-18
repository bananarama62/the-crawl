using UnityEngine;

public class Fireball : Effect
{
    [SerializeField] public GameObject ball;
    [SerializeField] public GameObject player;
    [SerializeField] private GameObject AimDirection;
    [SerializeField] private float distance; // Distance from player to place ability
    public override int individualEffect()
    {
        Debug.Log("Casting Fireball...");
        Vector3 location = player.transform.position + AimDirection.transform.rotation*new Vector3(distance,0f,distance);
        Instantiate(ball, location, AimDirection.transform.rotation);
        return 1;
    }
    void Awake()
    {
        base.init();
    } 
}
