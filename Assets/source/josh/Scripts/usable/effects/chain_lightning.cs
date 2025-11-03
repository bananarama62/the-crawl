using UnityEngine;

public class ChainLightning : Effect {
  public GameObject[] pylons;

  public override int individualEffect(){
    Debug.Log("Casting Chain Lightning...");
    return 1;
  }

  void Awake(){
    base.init();
  }
}
