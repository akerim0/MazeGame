using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamageScript : MonoBehaviour
{
    
    PlayerManagerScript playerScript;
    private void Start()
    {
        playerScript = FindObjectOfType<PlayerManagerScript>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Zombie Attack");
            playerScript.TakeDamage("zombie");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            playerScript.TakeDamage("zombie");
        }
    }
}
