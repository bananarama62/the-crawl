/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the Lightning class, which is spawned by the chain
 * lightning spell. They have a finite lifespan
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;
using NUnit.Framework;
using System.Collections;
public class Lightning : MonoBehaviour {
   private Animator AnimatorControl = new Animator();
   [SerializeField] private float timeToLive; // seconds to live for
   [SerializeField] private int damage;

   private AnimatorBase AnimationControl = new AnimatorType(); // Used for dynamic binding

   void Start(){
      Destroy(gameObject,timeToLive); // Sets timer for when to destroy object
   }

   public void setAnimation(int x){
      // Determines which animation in the chain to use
      float ChangeTo = new();
      ChangeTo = -1.0f;
      int num = AnimationControl.getAnimation(x);
      if(num == 1){
         ChangeTo = 0f;
      } else if(num == 2){
         ChangeTo = 0.2f;
      } else if(num == 3){
         ChangeTo = 0.4f;
      } else if(num == 4){
         ChangeTo = 0.6f;
      } else if(num == 5){
         ChangeTo = 0.8f;
      } else if(num == 6){
         ChangeTo = 1.0f;
      }

      if(ChangeTo >= 0f){ // If the number was within the bounds of what can set the animation to.
         AnimatorControl.SetFloat("Blend",ChangeTo);
      }
   }
   void Awake()
   {
      AnimatorControl = GetComponent<Animator>();
      Assert.NotNull(AnimatorControl);
      setAnimation(1);
   }

   void OnTriggerEnter2D(Collider2D collision)
   {
      // Only deals damage to the player.
      PlayerController player = collision.GetComponent<PlayerController>();
      if (collision.CompareTag("Player"))
      {
         player.takeDamage(damage);
      }
   }
}
