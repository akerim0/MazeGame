using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MutantScript : EnemyScript
{
    Animator animController;
    CharacterController characterController;
    AudioSource audioSource;
    public AudioClip slash, roar;
    public Transform target;
    public GameObject gameObj;
    private MutantHealthSystem healthSystem;
    PatrolScript patrolScript;
    private Vector3 gravityVector;
    public Slider slider;
    bool isPlayerVisible;
    public bool isDead;
    bool isAttacking;
    bool canAttack = true;
    int rand;
    [SerializeField] float attackRange = 3.5f;
    [SerializeField] float idleRange = 4.5f;
    [SerializeField] float chaseRange = 15.0f;
    [SerializeField] private float health;
    [SerializeField] private static float patrolSpeed;
    int damageAmount = 25;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        isDead = false;
        animController = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        healthSystem = gameObj.GetComponent<MutantHealthSystem>();
        patrolScript = GetComponent<PatrolScript>();
        audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerVisible)
        {
            patrolScript.patrol();
        }
        else if (isPlayerVisible)
        {
            animController.SetBool("patrol", false);
        }

        if (!this.isDead && (Vector3.Distance(transform.position, target.position) <= chaseRange) && (Vector3.Distance(transform.position, target.position) > idleRange))
        {
            isPlayerVisible = true;
            isAttacking = false;
            chase(target);
        }
        else if (!isDead && Vector3.Distance(transform.position, target.position) <= idleRange && Vector3.Distance(transform.position, target.position) > attackRange)
        {
            isPlayerVisible = true;
            isAttacking = false;
            idleFight();
        }
        else if (!isDead && (Vector3.Distance(transform.position, target.position) <= attackRange) && !isAttacking)
        {
            //StartCoroutine(generateRandom());
            isPlayerVisible = true;
            attack(target);
        }
        else if (Vector3.Distance(transform.position, target.position) > chaseRange)
        { isPlayerVisible = false; }
    }

    private void FixedUpdate()
    {
        gravityVector = Vector3.zero;
        if (!characterController.isGrounded)
        {
            gravityVector += Physics.gravity;
        }
        characterController.Move(gravityVector * Time.deltaTime);
    }
    private void idleFight()
    {
        transform.LookAt(target);
        animController.SetBool("patrolChase", false);
        animController.SetFloat("idleFightBlend", 0.7f);
        animController.SetBool("idle", true);

    }

    public override void attack(Transform target)
    {

        transform.LookAt(target);
        animController.SetBool("idle", true);
        isAttacking = true;
        if (canAttack)
        {
            int rand = generateRandom();
            switch (rand)
            {
                case 0:
                    Debug.Log("rand = : 0");
                    animController.SetTrigger("slash");
                    //checkAnimation("Slash");
                    break;
                case 1:
                    Debug.Log("rand = : 1");
                    animController.SetTrigger("slash2");
                    playComboAttack("slash2");
                    //checkAnimation("Slash");

                    break;

            }
            canAttack = false;
            StartCoroutine(WaitFor(3.0f));
        }
               
        

    }

    IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canAttack = true;
        isAttacking = false;
    }
    int generateRandom()
    {
        //yield return new WaitForSeconds(1.8f);
        return rand = UnityEngine.Random.Range(0, 2);
    }
    IEnumerator playComboAttack(String stateName)
    {
        yield return new WaitForSeconds(2.60f);
        animController.SetTrigger(stateName);
    }

    public override void Damage()
    {
        animController.SetTrigger("hit");
        audioSource.PlayOneShot(roar, 0.8f);
        healthSystem.takeDamage(damageAmount);
        health = healthSystem.getHealth();
        Debug.Log("health " + health.ToString());
        slider.value = health / 100.0f;

        if (health <= 0)
            Die();
    }

    public override void Die()
    {
        animController.SetBool("idle", false);
        animController.SetTrigger("death");
        isDead = true;
    }

    public override void chase(Transform target)
    {
        transform.LookAt(target);
        animController.SetBool("idle", false);
        animController.SetBool("patrolChase", true);
        patrolSpeed += 0.25f * Time.deltaTime;

        // Debug.Log("patrol Speed : " + patrolSpeed.ToString());
        if (patrolSpeed >= 0.8f)
            patrolSpeed = 0.8f;
        animController.SetFloat("patrolSpeed", patrolSpeed);

        //characterController.Move(Vector3.forward * 0.5f);


    }
    void Slash()
    {
        audioSource.PlayOneShot(slash, 0.45f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,chaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,idleRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
