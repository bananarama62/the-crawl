using UnityEngine;

public class SpawnMinion : Effect {
  public override int individualEffect(){
    Debug.Log("Casting Spawn Minion...");
    return 1;
  }

  public void init() {
    base.init(10);
  }
}
