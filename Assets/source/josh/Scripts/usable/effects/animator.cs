public class AnimatorBase {
   public virtual int getAnimation(int num){
      return 1;
   }
}


public class AnimatorType : AnimatorBase {
   public override int getAnimation(int num){
      return num;
   }
}
