using UnityEngine;

public class Swing : Effect {
   // Models an object swinging in an arc.
   public float SwingSpeed;
   private Quaternion TargetRotation;
   [SerializeField] private GameObject AimDirection;
   [SerializeField] private bool SwingFromLeft;
   [SerializeField] private int StartSwingDegreesFromCenter;
   [SerializeField] private int DegreesOfArc;
   private BoxCollider2D Collision;
   private SpriteRenderer Sprite;


   void enable(bool value){
      Sprite.enabled = value;
      Collision.enabled = value;
   }

   public override int individualEffect(){
      Quaternion StartingRotationOffset;
      Quaternion EndingRotation;
      if(SwingFromLeft){
         StartingRotationOffset = Quaternion.Euler(0,0,StartSwingDegreesFromCenter);
         EndingRotation = Quaternion.Euler(0,0,-DegreesOfArc);
      } else {
         StartingRotationOffset = Quaternion.Euler(0,0,-StartSwingDegreesFromCenter);
         EndingRotation = Quaternion.Euler(0,0,DegreesOfArc);
      }
      transform.rotation = AimDirection.transform.rotation * StartingRotationOffset;
      TargetRotation = transform.rotation * EndingRotation;
      enable(true);
      return 1;
   }

   void Awake(){
      // Gets the collider and Sprite renderer objects and disables them
      Collision = GetComponent<BoxCollider2D>();
      Sprite = transform.Find("image").GetComponent<SpriteRenderer>();
      enable(false);
   }

   void Update(){
      if(Collision.enabled){
         transform.rotation = Quaternion.Slerp(transform.rotation,TargetRotation,Time.deltaTime * SwingSpeed);
         // Checks if near the end of rotation and ends early to prevent stalling.
         if(Mathf.Abs(transform.rotation.eulerAngles.z - TargetRotation.eulerAngles.z) <= 2f){
            enable(false);
         }
      }
   }
}
