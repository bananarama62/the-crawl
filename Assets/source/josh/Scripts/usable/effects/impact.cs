/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the Impact class which add impact damage to an object.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;
using NUnit.Framework;

public class Impact : MonoBehaviour {
   // Adds impact damage to an object
   // The object this script is attached to needs to have a collider set to be 
   // a trigger. It should also contain some sort of visual element as to 
   // the size of the collider.

   private GameObject PlayerLocation;
   [SerializeField] private float timeToLive; // seconds to live for
   [SerializeField] private int damage;

   void Start(){
      Debug.Log("Impact...");
      Destroy(gameObject,timeToLive); // Object will be destroyed after a set amount of time
   }

   void OnTriggerEnter2D(Collider2D collision)
      // Will only deal damage to the player
   {
      PlayerController player = collision.GetComponent<PlayerController>();
      if (collision.CompareTag("Player"))
      {
         player.takeDamage(damage);
      }
   }

   void Awake(){
      PlayerLocation = GameObject.Find("Player");
      Assert.NotNull(PlayerLocation);
   }
}
