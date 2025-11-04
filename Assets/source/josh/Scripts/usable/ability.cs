using UnityEngine;

public abstract class Ability : Usable {

   private Effect effect;

   public override int use(){
      return effect.use();
   }
}
