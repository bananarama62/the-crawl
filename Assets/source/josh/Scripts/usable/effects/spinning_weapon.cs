/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the SpinningWeapon class, which is any weapon that spins
 * while traveling in a direction (thrown weapon).
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;

public class SpinningWeapon : MonoBehaviour {
   // Causes the weapon to move while spinning.
   private Vector3 MovementDirection;
   [SerializeField] private float MovementSpeed; // Linear movement speed of object
   [SerializeField] private float RotationSpeed; // Rotational movement speed of object
   [SerializeField] private LayerMask Walls;
   private string TargetTag;
   private bool Moving;

   void Start() {
      MovementDirection = transform.right * MovementSpeed;
      Debug.Log(MovementDirection);
      Moving = true;
   }

   public void setTargetTag(string tag){
      TargetTag = tag;
   }


   void FixedUpdate(){
      //transform.position += new Vector3(MovementDirection.x * MovementSpeed,MovementDirection.y * MovementSpeed,0);
      if(Moving){
         //Debug.Log(MovementDirection);
         transform.position += (MovementDirection * Time.deltaTime);
         transform.rotation *= Quaternion.Euler(0,0,RotationSpeed);
      }
   }

   void OnTriggerEnter2D(Collider2D collision)
   {
      if(collision.CompareTag(TargetTag) ){
         Destroy(gameObject);
      }
      else if (Walls.value == collision.gameObject.layer || collision.CompareTag("Walls"))
        {
            Moving = false;
        }
   }

}
