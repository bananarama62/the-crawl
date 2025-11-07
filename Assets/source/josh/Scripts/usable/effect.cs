using UnityEngine;
using NUnit.Framework;

public abstract class Effect : MonoBehaviour {
   protected Cooldown cooldown;

   public abstract int individualEffect();

   public int use(){
      if(cooldown.start()){ // Ability is still on cooldown
         return 1;
      }
      return individualEffect();
   }

   public bool onCooldown(){
      return cooldown.onCooldown();
   }

   public void init(){
      Transform t = transform.Find("Cooldown");
      Assert.NotNull(t);
      cooldown = t.GetComponent<Cooldown>();
   }

   void Start() {
      init();
   }
}
