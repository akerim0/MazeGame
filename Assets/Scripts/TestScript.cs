using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    bool elapsedTime = true;
    void Update()
    {

        if(elapsedTime)
             StartCoroutine(Coroutine());
        Debug.Log("This is update after Corountine ..");
    }

    IEnumerator Coroutine()
    {
        elapsedTime = false;
        Debug.Log("This is Corountine before return ...");
        yield return new WaitForSeconds(3.0f);
        elapsedTime = true;
        Debug.Log("This is Coroutine after yield >>>>>");
    }
}
