using UnityEngine;

public class Fireball : Effect
{
    private float damage = 5;
    Enemy enemy1;
    [SerializeField] public GameObject ball;
    [SerializeField] public GameObject player;
    [SerializeField] public Transform spawnPoint;
    public override int individualEffect()
    {
        Debug.Log("Casting Fireball...");
        Instantiate(ball, spawnPoint.position, player.transform.rotation);
        return 1;
    }
    void Awake()
    {
        base.init();
    } 
}
