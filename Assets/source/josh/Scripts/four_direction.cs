using UnityEngine;

public class FourDirectionSprite : MonoBehaviour
{
   private Animator AnimatorControl;
   [SerializeField] private float DefaultY = -1;
   [SerializeField] private float DefaultX = 0;

   void Start()
   {
      AnimatorControl = GetComponent<Animator>();
      AnimatorControl.SetFloat("x",DefaultX);
      AnimatorControl.SetFloat("y",DefaultY);
   }

   public void UpdateDirection(Vector2 Direction){
      Direction = Direction.normalized;
      if(Direction != Vector2.zero){
         AnimatorControl.Play("Base Layer.Walk");
         AnimatorControl.SetFloat("x",Direction.x);
         AnimatorControl.SetFloat("y",Direction.y);
      } else {
         AnimatorControl.Play("Base Layer.Idle");
      }
   }
}
