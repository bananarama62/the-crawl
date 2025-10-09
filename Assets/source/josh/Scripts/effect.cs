using UnityEngine;

public abstract class Effect : MonoBehaviour {
  private Cooldown cooldown;

  public abstract int individualEffect();

  public int use(){
    if(cooldown.start()){ // Ability is still on cooldown
      return 1;
    }
    return individualEffect();
  }
}
