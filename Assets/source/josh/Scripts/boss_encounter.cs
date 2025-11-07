using UnityEngine;

public class BossEncounter : MonoBehaviour {
   private bool PlayerInSight = false;

   public bool inSight(){
      return PlayerInSight;
   }

   void OnTriggerEnter2D(Collider2D collision){
      if (collision.CompareTag("Player")){
         PlayerInSight = true;
      }
   }

   void OnTriggerStay2D(Collider2D collision){
      if(!PlayerInSight){
         PlayerInSight = true;
      }
   }

   void OnTriggerExit2D(Collider2D collision){
      PlayerInSight = false;
   }
}
