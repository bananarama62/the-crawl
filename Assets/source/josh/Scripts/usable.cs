using UnityEngine;

public abstract class Usable {
  private string name;
  public abstract int use();

  public void setName(string new_name){
    name = new_name;
  }

  public string getName(){
    return name;
  }
}
