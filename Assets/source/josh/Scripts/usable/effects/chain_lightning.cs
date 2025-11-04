using UnityEngine;
using System.Collections.Generic;

public class ChainLightning : Effect {
  private List<GameObject> pylons;
  [SerializeField] private GameObject LightningBoltPreFab;
  private GameObject origin;

  public override int individualEffect(){
    Debug.Log("Casting Chain Lightning...");
    GameObject current_bolt;
    GameObject next_pylon;
    GameObject current_pylon;
    for(int i = -1; i < pylons.Count; ++i){
      if(i < 0){
        current_pylon = origin;
        next_pylon = pylons[0];
      } else {
        current_pylon = pylons[i];
        if(i < pylons.Count - 1){
          next_pylon = pylons[i+1];
        } else {
          next_pylon = pylons[0];
        }
      }
        current_bolt = Instantiate(LightningBoltPreFab, current_pylon.transform.position, Quaternion.Euler(Vector3.zero));
        Vector3 direction = next_pylon.transform.position - current_pylon.transform.position;
        direction.z = 0;
        float size = current_bolt.GetComponent<BoxCollider2D>().size.x;
        float ratio = direction.magnitude / size;
        current_bolt.transform.localScale = new Vector3(ratio,1f,1f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Converts Vector3 to an angle
        current_bolt.transform.rotation = Quaternion.Euler(0f,0f,angle);
    }
    return 1;
  }

  public void setPylons(List<GameObject> pylons_list){
    pylons = pylons_list;
  }

  public void setOrigin(GameObject o){
    origin = o;
  }

  void Awake(){
    base.init();
  }
}
