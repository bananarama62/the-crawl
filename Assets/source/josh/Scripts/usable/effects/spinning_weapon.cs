using UnityEngine;

public class SpinningWeapon : MonoBehaviour {
   private Vector3 MovementDirection;
   [SerializeField] private float MovementSpeed;
   [SerializeField] private float RotationSpeed;
   [SerializeField] private LayerMask Walls;
   private string TargetTag;
   private bool Moving;

   void Start() {
      MovementDirection = transform.right * MovementSpeed;
      Debug.Log(MovementDirection);
      Moving = true;
   }

   public void setTargetTag(string tag){
      TargetTag = tag;
   }


   void FixedUpdate(){
      //transform.position += new Vector3(MovementDirection.x * MovementSpeed,MovementDirection.y * MovementSpeed,0);
      if(Moving){
         //Debug.Log(MovementDirection);
         transform.position += (MovementDirection * Time.deltaTime);
         transform.rotation *= Quaternion.Euler(0,0,RotationSpeed);
      }
   }

   void OnTriggerEnter2D(Collider2D collision)
   {
      Debug.Log(collision.tag);
      if(collision.CompareTag(TargetTag) ){
         Destroy(gameObject);
      }
      else if (Walls.value == collision.gameObject.layer || collision.CompareTag("Walls"))
        {
            Moving = false;
        }
   }

}
