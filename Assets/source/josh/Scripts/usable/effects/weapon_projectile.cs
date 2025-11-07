using UnityEngine;

public class WeaponProjectile : Effect {
   [SerializeField] private GameObject PreFab; // Item that will be spawned
   private Quaternion AimDirection;
   private Vector3 SpawnLocation;
   [SerializeField] private string CasterTag;
   [SerializeField] private string TargetTag;



   public void aim(Vector3 Location, Quaternion Direction){
      AimDirection = Direction;
      SpawnLocation = Location;
   }

   public override int individualEffect(){
      Debug.Log("Casting Weapon Projectile "+AimDirection);
      GameObject Projectile = Instantiate(PreFab,SpawnLocation,AimDirection);
      Projectile.GetComponent<SpinningWeapon>().setTargetTag(TargetTag);
      Projectile.GetComponent<weapon>().Caster = CasterTag;
      return 1;
   }
}
