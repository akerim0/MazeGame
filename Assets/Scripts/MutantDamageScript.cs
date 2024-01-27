using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantDamageScript : MonoBehaviour
{
    public GameObject gameObj,mutgameObj;
    PlayerManagerScript playerScript;
    MutantScript mutantScript;
    private void Start()
    {
        playerScript = gameObj.GetComponent<PlayerManagerScript>();
        mutantScript = mutgameObj.GetComponent<MutantScript>();
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if(!mutantScript.isDead)
                playerScript.TakeDamage("mutant");
        }
    }
}
