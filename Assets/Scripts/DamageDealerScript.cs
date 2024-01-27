using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerScript : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;
    [SerializeField] public float weaponLength =0.85f;
    [SerializeField] float weaponDamage;
    AudioSource audioSource;
    public AudioClip slash;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
            int layerMask = 1 << 9;
            // Debug.DrawRay(transform.position, transform.forward * weaponLength, Color.red);
            if (Physics.Raycast(transform.position, -transform.forward, out hit, weaponLength, layerMask))
            {
                Debug.DrawRay(transform.position, -transform.forward * hit.distance, Color.red);
                if (!hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    if (hit.transform.TryGetComponent(out MutantScript mutantScript))
                    {
                        mutantScript.Damage();
                        //audioSource.PlayOneShot(slash,0.7f);
                        Debug.Log("Damage Mutant !!");
                    }

                    else if (hit.transform.TryGetComponent(out ZombieScript zombieScript))
                    {
                        zombieScript.Die();
                    }

                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    int layermask = 1 << 9;
    //    if(Physics.Raycast(transform.position,-transform.forward,out RaycastHit hitInfo, weaponLength, layermask))
    //    {
    //        if (!hasDealtDamage.Contains(hitInfo.transform.gameObject))
    //        {
    //            if (hitInfo.transform.TryGetComponent(out MutantScript mutantScript))
    //            {
    //                mutantScript.Damage();
    //                //audioSource.PlayOneShot(slash,0.7f);
    //                Debug.Log("Damage Mutant !!");
    //            }

    //            else if (hitInfo.transform.TryGetComponent(out ZombieScript zombieScript))
    //            {
    //                zombieScript.Die();
    //            }

    //            hasDealtDamage.Add(hitInfo.transform.gameObject);
    //        }
    //    }
    //}


    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position - transform.forward *(- weaponLength));
        //Gizmos.DrawWireCube(/*new Vector3(0.00685797f,-0.006698f,0.4069359f)*/transform.position,new Vector3(0.0774234f, 0.0443098f, 0.7415081f),);
    }
}
