using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour
{
    Animator animatorController;
    CharacterController characterController;
    AudioSource runAudio;
    public GameObject obj;
    GameManagerS gameManager;
    public Slider slider;
    HealthSystem healthSystem;
    bool isIdling;
    bool isRunning, isbackRunning;
    bool isJumping;
    bool timePassed;
    public bool isvisible;
    Vector3 gravityVector;
    private int clickCount;
    float sensitivity = 90.0f;
    [SerializeField] static float runvelocity = 0.0f, backrunvelocity = 0.0f;
    [SerializeField] private float health ;
    [SerializeField] private int damageAmount;
    [SerializeField] private int healingAmount = 2;


    // Start is called before the first frame update
    void Start()
    {
        isvisible = true;
        isIdling = true;
        characterController = GetComponent<CharacterController>();
        animatorController = GetComponent<Animator>();
        runAudio = GetComponent<AudioSource>();
        healthSystem = obj.GetComponent<HealthSystem>();
        //gameManager = objManag.GetComponent<GameManagerS>();
        // animator.SetBool("fightIdle",true);
    }

    // Update is called once per frame
    void Update()
    {
        //
        Idle();

        //JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isIdling)
        {
            
            animatorController.SetTrigger("idleJump");
           
        }

        if (Input.GetKeyDown(KeyCode.Space) && isRunning)
        {
            isJumping = true;
            animatorController.SetTrigger("runJump");
            //isRunning = false;          
        }

        if (Input.GetKeyDown(KeyCode.Space) && isbackRunning)
        {
            //isbackRunning = false;
            isJumping = true;
            animatorController.SetTrigger("idleJump");
            
        }

        //MOVEMENT
        if (Input.GetKey(KeyCode.W))
        {
            isIdling = false;
            isRunning = true;
            animatorController.SetBool("fightIdle", false);
            animatorController.SetBool("runForw", true);
            runvelocity += 0.7f * Time.deltaTime;
            if (runvelocity >= 1.0f)
            {
                runvelocity = 1.0f;
            }
            animatorController.SetFloat("Blend", runvelocity);
            //rb.velocity = Vector3.forward * runvelocity * Time.deltaTime; ;
        }
        else
        {

                runvelocity = 0.0f;
                backrunvelocity = 0.0f;
                animatorController.SetBool("runForw", false);
                animatorController.SetBool("fightIdle", true);
                isIdling = true;
                isRunning = false;
            

        }

        if (Input.GetKey(KeyCode.S))
        {
            isIdling = false;
            isbackRunning = true;
           
            animatorController.SetBool("fightIdle", false);
            animatorController.SetBool("runBack", true);
            backrunvelocity += 0.4f * Time.deltaTime;
            if (backrunvelocity >= 1.0f)
            {
                backrunvelocity = 1.0f;
            }
            animatorController.SetFloat("backBlend", backrunvelocity);
            //rb.velocity = Vector3.forward * runvelocity * Time.deltaTime; ;
        }
        else
        {

             backrunvelocity = 0.0f;
             animatorController.SetBool("runBack", false);
             animatorController.SetBool("fightIdle", true);
             isIdling = true;
             isbackRunning = false;


        }
        if (Input.GetKey(KeyCode.D))
        {

            isIdling = false;
            isRunning = true;
            animatorController.SetBool("fightIdle", false);
            animatorController.SetBool("rightStrafe", true);

        }
        else
        {

            animatorController.SetBool("rightStrafe", false);
            animatorController.SetBool("fightIdle", true);
            isIdling = true;
            isRunning = false;

        }

        if (Input.GetKey(KeyCode.A))
        {

            isIdling = false;
            isRunning = true;
            animatorController.SetBool("fightIdle", false);
            animatorController.SetBool("leftStrafe", true);

        }
        else
        {

            animatorController.SetBool("leftStrafe", false);
            animatorController.SetBool("fightIdle", true);
            isIdling = true;
            isRunning = false;

        }

        //Fight
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (clickCount == 1)
            {
                clickCount = 0;
                animatorController.SetTrigger("slash");
            }
            else if (clickCount == 2 && isIdling)
            {
                clickCount = 0;
                animatorController.SetTrigger("attack");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animatorController.SetTrigger("slash2");
        }
    }

    private void FixedUpdate()
    {
        gravityVector = Vector3.zero;
        if (!characterController.isGrounded && isJumping == false)
        {
            gravityVector += Physics.gravity;
        }
        characterController.Move(gravityVector * Time.deltaTime);
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isMouse)
        {
            clickCount = e.clickCount;
            Debug.Log("Mouse clicks: " + e.clickCount);
        }
    }


    private void Idle()
    {
        if (!isbackRunning && !isJumping && !isRunning )
        {
            isIdling = true;
            animatorController.SetBool("fightIdle", true);
        }

    }
    private void RotateWithCam()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Horizontal"));
    }


    public void TakeDamage(string enemy)
    {
        if(enemy == "zombie")
        {
            damageAmount = 1;
        }
        else if (enemy == "mutant")
        {
            damageAmount = 10;
            animatorController.SetTrigger("impact");
        }

        healthSystem.takeDamage(damageAmount);
        health = healthSystem.getHealth();
        slider.value = health / 100.0f;        
        Debug.Log("Remaining Life : " + health.ToString());
        if (health <= 0)
            Die();
    }
    public void heal()
    {
        healthSystem.heal(healingAmount);
        Debug.Log("Healing...");
        health = healthSystem.getHealth();
        slider.value = health/100.0f;
    }

    public void Die()
    {
        animatorController.SetTrigger("death");

        //gameManager.LooseScene();
    }
    public void disableVisiblity()
    {
        isvisible = false;
    }
    public void enableVisiblity()
    {
        isvisible = true;
    }
    void playRunAudio()
    {

    }

    public void NoGravity()
    {
        isJumping = true;
        isIdling = false;
        isRunning = false;
        Debug.Log("No gravity");
    }
    public void AddGravity()
    {
        isJumping = false;
        isIdling = true;
        Debug.Log("gravity Added");
    }
    public void AddGravityForRun()
    {
        isJumping = false ;
        isRunning = true;
    }
    IEnumerator audioCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        runAudio.Play();

    }
    void checkAnimationState(string name)
    {
        if (animatorController.GetCurrentAnimatorStateInfo(0).IsName(name) && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            isJumping = false;
            isIdling = true;
        }
    }
}