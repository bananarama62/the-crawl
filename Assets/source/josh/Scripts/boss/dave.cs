using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class BossDave : Boss {

  private ChainLightning ability_chain_lightning;
  private SpawnPylon ability_spawn_pylon;
  [SerializeField] int max_num_pylons;
  private List<GameObject> pylons = new();

  void FixedUpdate() {
    if (playerInSight){
      if(pylons.Count < max_num_pylons){
        int status;
        if((status = ability_spawn_pylon.use()) == 0){
          pylons.Add(ability_spawn_pylon.SpawnedPylon);
        } else if(status == 2){
          Debug.Log("Spawn pylon returned NULL.");
        }
      } else {
        ability_chain_lightning.use();
      }
    }
  }

  void Awake()
  {
    initHealthAndSpeed(5,speed:3); // Sets current and max health to 5 and speed to 3
    initMovement();
    Transform t = transform.Find("ChainLightning");
    Assert.NotNull(t);
    ability_chain_lightning = t.GetComponent<ChainLightning>();
    t = transform.Find("SpawnPylon");
    Assert.NotNull(t);
    ability_spawn_pylon = t.GetComponent<SpawnPylon>();
  }
  
}
