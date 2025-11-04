using UnityEngine;
using NUnit.Framework;

public class FallingObject : MonoBehaviour {
   [SerializeField] float spawn_delay;
   private BoxCollider2D collision;
   private SpriteRenderer sprite;
   private Vector3 finalDestination;
   [SerializeField] GameObject locationIndicatorPreFab;
   private GameObject locationIndicator;
   private float indicator_offset_x;
   private float indicator_offset_y;
   private float elapsed_time;
   [SerializeField] float fall_from_height;
   [SerializeField] float fall_speed;
   private bool started_falling;
   private bool fall_complete;
   private int fall_length;
   private Vector3 indicator_location;

   [SerializeField] private GameObject impact;
   void Start(){
      Debug.Log("Casting Falling object");
      indicator_location = finalDestination + new Vector3(indicator_offset_x,indicator_offset_y,0);
      Debug.Log("Indicator: "+finalDestination+" Spawning: "+indicator_location);
      locationIndicator = Instantiate(locationIndicatorPreFab,indicator_location,Quaternion.identity);
   }

   void Update(){
      if(!started_falling || !fall_complete){
         elapsed_time += Time.deltaTime;
         if(!started_falling){
            if (elapsed_time > spawn_delay){
               started_falling = true;
               Destroy(locationIndicator);
               sprite.enabled = true;
               elapsed_time = 0;
               transform.position += new Vector3(0,fall_from_height,0);
            }
         } if(started_falling){
            if(transform.position != finalDestination){
               transform.position = Vector3.Lerp(transform.position,finalDestination,elapsed_time * fall_speed);
            } else {
               fall_complete = true;
               collision.enabled = true;
               Instantiate(impact,indicator_location,Quaternion.identity);
            }
         }
      }
   }

   public void setIndicatorOffset(float x, float y){
      indicator_offset_x = x;
      indicator_offset_y = y;
   }

   void Awake(){
      collision = GetComponent<BoxCollider2D>();
      sprite = GetComponent<SpriteRenderer>();
      collision.enabled = false;
      sprite.enabled = false;
      finalDestination = transform.position;
      elapsed_time = 0;
      started_falling = false;
      fall_complete = false;
   }

}
