using UnityEngine;
using System.Collections.Generic;

public class ChainLightning : Effect {
  private List<GameObject> pylons;

  public override int individualEffect(){
    Debug.Log("Casting Chain Lightning...");
    return 1;
  }

  public void setPylons(List<GameObject> pylons_list){
    pylons = pylons_list;
  }

  void Awake(){
    base.init();
  }
}
