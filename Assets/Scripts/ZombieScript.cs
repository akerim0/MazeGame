using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : EnemyScript
{
    Animator animController;
    NavMeshAgent navMeshAgent;
    bool isDead;
    bool canAttack;
    Transform player;
    //public GameObject gameObj;
    PlayerManagerScript PlayerManSc;
    [SerializeField] private int health = 100;
    [SerializeField] float attackrange = 2.0f;
    [SerializeField] float chaserange = 15.0f;
    [SerializeField] private float speed = 0.0f;
    private AudioSource audioSource;
    [SerializeField] AudioClip noiseClip;
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip deathClip; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animController = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        PlayerManSc = FindObjectOfType<PlayerManagerScript>();
        player = GameObject.Find("player").GetComponent<Transform>();
        animController.SetFloat("Blend", 0.75f);
        canAttack = true;
       
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerManSc.isvisible && !this.isDead )
        {
            transform.LookAt(player);
            chase(player);
           
        }
        if(PlayerManSc.isvisible && !this.isDead && (Vector3.Distance(transform.position, player.position)) <= attackrange)
        {
            Idle();
            transform.LookAt(player);
            attack(player);
        }

        if (isDead)
        {
            StartCoroutine(waitForTillDeath(4.0f));
        }
            
    }

    public override void Damage()
    {
        health -= 100;
        animController.SetTrigger("hit");
        if(health <= 0)
        {
            Die();
        }
        Debug.Log("Zombie Damage !!");
    }

    public override void Die()
    {
        isDead = true;
        animController.SetTrigger("death");
        audioSource.PlayOneShot(deathClip);
        
    }

    public override void chase(Transform target)
    {
        animController.SetBool("idle", false);
        animController.SetBool("walk", true);
        Debug.Log("zombie Chase !");
        
    }
    public void Idle()
    {
        animController.SetBool("idle", true);
        animController.SetBool("walk",false);
    }
    public override void attack(Transform target)
    {
        if (canAttack)
        {
            animController.SetTrigger("attack");
           
        }
        waitFor(2.0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,attackrange);
    }
    IEnumerator waitFor(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }
    IEnumerator waitForTillDeath(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
    public void playAttackAudio()
    {
        audioSource.PlayOneShot(attackClip);
    }
   
}
