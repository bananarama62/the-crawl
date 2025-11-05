using UnityEngine;
using NUnit.Framework;

public class Impact : MonoBehaviour {

   private GameObject PlayerLocation;
   [SerializeField] private float timeToLive; // seconds to live for
   [SerializeField] private int damage;
   private float elapsed_time;

   void Start(){
      Debug.Log("Impact...");
      elapsed_time = 0;
   }

   void Update(){
      elapsed_time += Time.deltaTime;
      if(elapsed_time > timeToLive){
         Destroy(gameObject);
      }
   }

   void OnTriggerEnter2D(Collider2D collision)
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
