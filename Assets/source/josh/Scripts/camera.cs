using UnityEngine;

public class camera : MonoBehaviour
{
   [SerializeField] private GameObject Target;
   private Vector3 RelCameraPos;

   void Awake()
   {
      RelCameraPos = Target.transform.position - transform.position;
   }

   // Update is called once per frame
   void Update()
   {
      transform.position = Target.transform.position - RelCameraPos; 
   }
}
