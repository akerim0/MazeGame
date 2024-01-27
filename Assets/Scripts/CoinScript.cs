using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject UIObject;
    UIScript getUIScript;
    private void Start()
    {
        getUIScript = UIObject.GetComponent<UIScript>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            getUIScript.IncrementCoin();
            waitforAndDestroy(0.5f);
        }
    }
    IEnumerator waitforAndDestroy(float secs)
    {
        yield return new WaitForSeconds(secs);
        Destroy(this.gameObject);
    }
}
