using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalsScript : MonoBehaviour
{
    Transform[] pts_List;
    public List<GameObject> zombies;
    public Transform EnemiesParent;
    bool CanInstantiateNext = true;
    private int portalCounter = 1;
    int round;
    float roundTime;

    GameRoundScript gameRoundScript;

    
    //[SerializeField] GameObject zombie1, zombie2, zombie3;
   
    // Start is called before the first frame update
    void Start()
    {
        pts_List = gameObject.GetComponentsInChildren<Transform>();
        gameRoundScript = FindObjectOfType<GameRoundScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        round = gameRoundScript.getRound();
        roundTime = gameRoundScript.roundList[round].duration;
        
        if (CanInstantiateNext)
        {
            int rand = Random.Range(0, 3);
            if(zombies != null)
            {
                Instantiate(zombies[rand], pts_List[portalCounter ].position, Quaternion.identity,EnemiesParent);
            }

            StartCoroutine("waitToInstantiate");
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (Transform child in this.transform)
        {
            Gizmos.DrawWireSphere(child.transform.position, 2.0f);
        }
    }
    IEnumerator waitToInstantiate()
    {      
        CanInstantiateNext = false;
        if (portalCounter >= 4)
        {
            yield return new WaitForSeconds(10.0f);
            portalCounter = 1;

        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            portalCounter++;
        }
            
        CanInstantiateNext = true;
        
        
    }
}
