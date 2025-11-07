using UnityEngine;

public abstract class Boss : Enemy {
   protected BossEncounter WithinSightRange;
   
   public override void TakeDamage(float damage){
      modifyHealth((int)(-1*damage));
      UIHandler.instance.SetBossHealth(getCurrentHealthPercentage());
   }
}

