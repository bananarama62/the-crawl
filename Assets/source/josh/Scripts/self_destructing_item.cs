
/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the SelfDestruction class which causes an item to be
 * destroyed after a set amount of time.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;

public class SelfDestruction : MonoBehaviour 
{
   [SerializeField] private float TimeToLive;
   [SerializeField] private bool OnStartup; // true 
   void Start(){
      if(OnStartup){
         Activate();
      }
   }

   public void Activate(){
      Destroy(gameObject,TimeToLive);
   }
}
