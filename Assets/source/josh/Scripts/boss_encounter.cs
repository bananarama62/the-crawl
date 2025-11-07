using UnityEngine;

public class BossEncounter : MonoBehaviour {
   private bool PlayerInSight = false;
   [SerializeField] private string BossName;

   public void SetName(string n){
      BossName = n;
   }

   public bool inSight(){
      return PlayerInSight;
   }

   void OnTriggerEnter2D(Collider2D collision){
      if (collision.CompareTag("Player")){
         PlayerInSight = true;
         UIHandler.instance.EnterBossEncounter(BossName);
      }
   }

   void OnTriggerStay2D(Collider2D collision){
      if (collision.CompareTag("Player")){
         if(!PlayerInSight){
            PlayerInSight = true;
         }
      }
   }

   void OnTriggerExit2D(Collider2D collision){
      if (collision.CompareTag("Player")){
         PlayerInSight = false;
         UIHandler.instance.ExitBossEncounter();
      }
   }
}
