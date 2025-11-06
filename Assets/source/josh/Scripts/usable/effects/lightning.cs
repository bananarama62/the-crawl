using UnityEngine;
using NUnit.Framework;
using UnityEditor.Animations;
using System.Collections;
public class Lightning : MonoBehaviour {
   [SerializeField] private AnimatorController animation_loop_1;
   [SerializeField] private AnimatorController animation_loop_2;
   [SerializeField] private AnimatorController animation_loop_3;
   [SerializeField] private AnimatorController animation_loop_4;
   [SerializeField] private AnimatorController animation_loop_5;
   [SerializeField] private AnimatorController animation_loop_6;
   [SerializeField] private float timeToLive; // seconds to live for
   [SerializeField] private int damage;
   private float elapsed_time;
   private AnimatorBase AnimationControl = new AnimatorType();

   void Start(){
      Destroy(gameObject,timeToLive);
   }

   public void setAnimation(int x){
      AnimatorController ChangeTo = null;
      int num = AnimationControl.getAnimation(x);
      if(num == 1){
         ChangeTo = animation_loop_1;
      } else if(num == 2){
         ChangeTo = animation_loop_2;
      } else if(num == 3){
         ChangeTo = animation_loop_3;
      } else if(num == 4){
         ChangeTo = animation_loop_4;
      } else if(num == 5){
         ChangeTo = animation_loop_5;
      } else if(num == 6){
         ChangeTo = animation_loop_6;
      }

      if(ChangeTo != null){
         gameObject.GetComponent<Animator>().runtimeAnimatorController = ChangeTo;
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
}
