using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class SwordPlayerController : MonoBehaviour
{
    Animator animatorController;
    CharacterController characterController;
    AudioSource runAudio;  
    GameManagerS gameManager;   
    bool isIdling;
    bool isRunning, isbackRunning;
    bool isJumping;
    bool timePassed;
    
    Vector3 gravityVector;
    private int clickCount;
    float sensitivity = 90.0f;
    int fightIdle;
    [SerializeField] static float runvelocity = 0.0f, backrunvelocity = 0.0f;  
    


    // Start is called before the first frame update
    void Start()
    {
        
        isIdling = true;
        characterController = GetComponent<CharacterController>();
        animatorController = GetComponent<Animator>();
        runAudio = GetComponent<AudioSource>();
        fightIdle = Animator.StringToHash("fightIdle");
        //gameManager = objManag.GetComponent<GameManagerS>();
        // animator.SetBool("fightIdle",true);
    }

    // Update is called once per frame
    void Update()
    {
        //
        //Idle();

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
            animatorController.SetBool("fightIdle",false);
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
            if(animatorController.GetCurrentAnimatorStateInfo(0).IsName("Run Blend"))
            {
                runvelocity = 0.0f;
                backrunvelocity = 0.0f;
                animatorController.SetBool("runForw", false);
                animatorController.SetBool("fightIdle", true);
                isIdling = true;
                isRunning = false;
            }
            
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
            if (animatorController.GetCurrentAnimatorStateInfo(0).IsName("backwardRunBlend"))
            {
                backrunvelocity = 0.0f;
                animatorController.SetBool("runBack", false);
                animatorController.SetBool("fightIdle", true);
                isIdling = true;
                isbackRunning = false;
            }
            


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
            if(animatorController.GetCurrentAnimatorStateInfo(0).IsName("Right Strafe"))
            {
                animatorController.SetBool("rightStrafe", false);
                animatorController.SetBool("fightIdle", true);
                isIdling = true;
                isRunning = false;
            }
            

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
            if(animatorController.GetCurrentAnimatorStateInfo(0).IsName("Left Strafe"))
            {
                animatorController.SetBool("leftStrafe", false);
                animatorController.SetBool("fightIdle", true);
                isIdling = true;
                isRunning = false;
            }
            

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
            animatorController.SetTrigger("block");
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            animatorController.SetBool("fightIdle", false);
            animatorController.SetBool("blockIdle", true);
            if (Input.GetKey(KeyCode.S))
            {
                animatorController.SetBool("blockBackWalk",true);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                animatorController.SetBool("blockWalk",true);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                animatorController.SetBool("blockBackWalk", false);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                animatorController.SetBool("blockWalk", false);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animatorController.SetBool("fightIdle", true);
            animatorController.SetBool("blockIdle", false);
            animatorController.SetBool("blockBackWalk", false);
            animatorController.SetBool("blockWalk", false);
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
        if (isbackRunning && !isJumping && !isRunning)
        {
            isIdling = true;
            animatorController.SetBool("fightIdle", true);
        }

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
        isJumping = false;
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