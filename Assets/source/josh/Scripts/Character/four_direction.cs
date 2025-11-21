/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the FourDirectionSprite, which is used in conjunction with
 * an Animator to display sprites. It interfaces with an Animator to set the 
 * direction values based on a Vector2 object of the direction the character is
 * moving. The Animator needs to be set up with two blend trees, named both
 * 'Walk' and 'Idle'.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;

public class FourDirectionSprite : MonoBehaviour {
   // Controls a sprite that can face left,right,forward, or backwards.
   private Animator AnimatorControl;
   [SerializeField] private float DefaultY = -1;
   [SerializeField] private float DefaultX = 0;

   void Start()
   {
      AnimatorControl = GetComponent<Animator>();
      AnimatorControl.SetFloat("x",DefaultX);
      AnimatorControl.SetFloat("y",DefaultY);
   }

   public void UpdateDirection(Vector2 Direction){
      // Applies a direction to the Animator to adjust the sprite's direction
      Direction = Direction.normalized;
      if(Direction != Vector2.zero){ // The movement vector exists and is pointed in some direction
         AnimatorControl.Play("Base Layer.Walk");
         AnimatorControl.SetFloat("x",Direction.x);
         AnimatorControl.SetFloat("y",Direction.y);
      } else { // No movement, therefore, switch to idel.
         AnimatorControl.Play("Base Layer.Idle");
      }
   }
}
