using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoorScript : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            door.transform.Translate(Vector3.up * -5);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            door.transform.Translate(Vector3.up * 5);
        }
    }
}
