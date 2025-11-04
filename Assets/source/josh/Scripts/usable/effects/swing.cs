using UnityEngine;

public class Swing : Effect {
   public GameObject ItemPreFab; // Prefab of item to spawn
   public float SwingSpeed;
   private Quaternion targetRotation;
   private bool isActive;
   public GameObject AimDirection;
   private BoxCollider2D collision;
   private SpriteRenderer sprite;
   void enable(bool value){
      sprite.enabled = value;
      collision.enabled = value;
   }

   public override int individualEffect(){
      transform.rotation = AimDirection.transform.rotation * Quaternion.Euler(0,0,45);
      targetRotation = transform.rotation * Quaternion.Euler(0,0,-90);
      enable(true);
      return 1;
   }

   void Awake(){
      collision = GetComponent<BoxCollider2D>();
      sprite = GameObject.Find("image").GetComponent<SpriteRenderer>();
      enable(false);
   }

   void Update(){
      transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Time.deltaTime * SwingSpeed);
      if(transform.rotation == targetRotation){
         enable(false);
      }
   }
}
