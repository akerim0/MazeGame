using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingScript : MonoBehaviour
{
    public GameObject gameObj;
    PlayerManagerScript playercontrol;
    // Start is called before the first frame update
    void Start()
    {
        playercontrol = gameObj.GetComponent<PlayerManagerScript>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Healing Started .. ");
            StartCoroutine("Coroutine");
        }
            
        
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        StartCoroutine("Coroutine");
    //    }
        
    //}
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playercontrol.enableVisiblity();
        }
           
    }

    IEnumerator Coroutine()
    {
        if (playercontrol.isvisible)
        {
            playercontrol.disableVisiblity();
        }
        for (int i = 0; i <= 30; i++)
        {
            playercontrol.heal();
            yield return new WaitForSeconds(0.5f);
        }

        playercontrol.enableVisiblity();
    }
}
