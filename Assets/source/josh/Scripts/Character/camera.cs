/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the camera class, which allows the camera to follow a 
 * target without being child of the target.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;

public class camera : MonoBehaviour
   // Allows the camera to follow the player. Prevents it from
   // being affected by rotation of the player.
{
   [SerializeField] private GameObject Target; // The target that the camera will follow (player)
   private Vector3 RelCameraPos; // Distance between the camera and player at the start of the scene.

   void Awake()
   {
      RelCameraPos = Target.transform.position - transform.position;
   }

   // Update is called once per frame
   void Update()
   {
      transform.position = Target.transform.position - RelCameraPos; 
   }
}
