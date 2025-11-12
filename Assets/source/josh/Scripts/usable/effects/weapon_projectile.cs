/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the WeaponProjectile class, which is an effect that 
 * "throws" a weapon. The weapon is spawned by this. Must be coupled with the
 * SpinningWeapon class.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;

public class WeaponProjectile : Effect {
   [SerializeField] private GameObject PreFab; // Item that will be spawned
   private Quaternion AimDirection;
   private Vector3 SpawnLocation;
   [SerializeField] private string CasterTag; // Who threw the weapon (prevents it from damaging caster)
   [SerializeField] private string TargetTag; // Tag of enemey (or player). Whoever can take damage from it



   public void aim(Vector3 Location, Quaternion Direction){
      // Sets the location and direction to spawn the object in
      AimDirection = Direction;
      SpawnLocation = Location;
   }

   public override int individualEffect(){
      // Spawns the object. aim() must be called first.
      Debug.Log("Casting Weapon Projectile "+AimDirection);
      GameObject Projectile = Instantiate(PreFab,SpawnLocation,AimDirection);
      Projectile.GetComponent<SpinningWeapon>().setTargetTag(TargetTag);
      Projectile.GetComponent<weapon>().Caster = CasterTag;
      return 1;
   }
}
