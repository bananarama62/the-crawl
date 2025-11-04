using UnityEngine;

public class camera : MonoBehaviour
{
   [SerializeField] private GameObject target;
   private Vector3 relCameraPos;
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {

   }

   void Awake()
   {
      relCameraPos = target.transform.position - transform.position;
   }

   // Update is called once per frame
   void Update()
   {
      transform.position = target.transform.position - relCameraPos; 
   }
}
