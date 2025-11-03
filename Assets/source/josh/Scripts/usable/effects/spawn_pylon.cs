using UnityEngine;
using NUnit.Framework;

public class SpawnPylon : Effect {
  public GameObject PylonPreFab; // Prefab for enemy to spawn
  [SerializeField] GameObject PlayerLocation;
  public GameObject SpawnedPylon;
  public override int individualEffect(){
    Debug.Log("Casting Spawn Pylon...");
    GameObject temp = Instantiate(PylonPreFab, PlayerLocation.transform.position, Quaternion.identity);
    if(temp){
      SpawnedPylon = temp;
      return 0;
    }
    return 2;
  }

  void Awake(){
    base.init();
    PlayerLocation = GameObject.Find("Player");
    Assert.NotNull(PlayerLocation);
  }

}
