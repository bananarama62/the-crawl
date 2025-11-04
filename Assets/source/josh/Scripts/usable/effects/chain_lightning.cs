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
        current_bolt.transform.localScale = new Vector3(3f,3f,1f);
        current_bolt.GetComponent<Lightning>().setAnimation(0);
        Vector2 size = current_bolt.GetComponent<BoxCollider2D>().size;

        int count = (int)(direction.magnitude / size.x);
        float ratio = direction.magnitude / (size.x*count);
        size = size * ratio;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Converts Vector3 to an angle
        current_bolt.transform.localScale = new Vector3(ratio,ratio,1f);
        direction = Vector3.Normalize(direction);
        Vector3 previous_position = current_bolt.transform.position;
        Vector3 rotated_length = Vector3.Scale(direction,new Vector3(size.x,size.y,0f));
        for(int j = 1; j < count; ++j){
          Debug.Log(direction);
          Vector3 position = rotated_length + previous_position;
          current_bolt = Instantiate(LightningBoltPreFab, position, Quaternion.Euler(0f,0f,angle));
          current_bolt.transform.localScale = new Vector3(3*ratio,3*ratio,1f);
          current_bolt.GetComponent<Lightning>().setAnimation((j+1)%6);
          previous_position = current_bolt.transform.position;
        }
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
