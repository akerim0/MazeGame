using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public GameObject SwordObject;
    DamageDealerScript getSword;
    // Start is called before the first frame update
    void Start()
    {
        getSword = SwordObject.GetComponent<DamageDealerScript>();
    }

    // Update is called once per frame
    void StartDealDamage()
    {
        getSword.StartDealDamage();
    }
    void EndDealDamage()
    {
        getSword.EndDealDamage();
    }

    
    
}
