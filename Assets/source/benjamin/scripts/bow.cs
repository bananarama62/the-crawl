using UnityEngine;

public class Bow : Effect
{
    [SerializeField] private GameObject ArrowPrefab;
    [SerializeField] private Transform StartPos;
    [SerializeField] private GameObject AimDirection;   
    [SerializeField] private float ArrowSpeed;
    [SerializeField] private string Caster = "Player";
    private SpriteRenderer Sprite;
    private void Update()
    {
        transform.rotation = AimDirection.transform.rotation;
    }
    public override int individualEffect()
    {
        Vector3 SpawnPos;
        SpawnPos = StartPos.position;
        Quaternion Rotation;
        Rotation = AimDirection.transform.rotation;

        GameObject arrow = Instantiate(ArrowPrefab, SpawnPos, Rotation);
        Arrow  aw= arrow.GetComponent<Arrow>();
        aw.Owner = Caster;
        aw.Speed = ArrowSpeed;
        aw.Fire(Rotation);
        return 1;
    }
}