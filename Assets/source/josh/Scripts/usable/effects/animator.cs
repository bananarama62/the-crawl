/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the AnimatorBase and AnimatorType classes, which exist
 * solely for the purpose of having dynamic binding.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
public class AnimatorBase {
   // Used by the lightning prefab to determine which animation to use.
   public virtual int getAnimation(int num){
      // Always returns 1
      return 1;
   }
}


public class AnimatorType : AnimatorBase {
   // Used by the lightning prefab to determine which animation to use.
   public override int getAnimation(int num){
      // Returns whatever number is passed into it.
      return num;
   }
}
