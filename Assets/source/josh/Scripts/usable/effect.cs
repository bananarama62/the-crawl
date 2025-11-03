using UnityEngine;
using NUnit.Framework;

public abstract class Effect : MonoBehaviour {
  private Cooldown cooldown;

  public abstract int individualEffect();

  public int use(){
    if(cooldown.start()){ // Ability is still on cooldown
      return 1;
    }
    return individualEffect();
  }

  public void init(){
    Transform t = transform.Find("Cooldown");
    Assert.NotNull(t);
    cooldown = t.GetComponent<Cooldown>();
    Debug.Log(cooldown.getCooldownLength());
  }

  void Start() {
    init();
  }
}
