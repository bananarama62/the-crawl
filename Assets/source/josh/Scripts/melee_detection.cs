using UnityEngine;

public class DetectRange : MonoBehaviour {

   public bool playerInSight = false;

   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Player"))
      {
         playerInSight = true;
      }
   }

   void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.CompareTag("Player"))
      {
         playerInSight = false;
      }
   }

}
