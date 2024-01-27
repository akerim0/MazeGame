using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    int counter = 0;
    public Text cointxt;
    // Start is called before the first frame update
    public void IncrementCoin()
    {
        counter++;
        string setText = counter.ToString();
        cointxt.text = setText;

    }
}
