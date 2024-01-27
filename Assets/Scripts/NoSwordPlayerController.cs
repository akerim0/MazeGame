using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoSwordPlayerController : MonoBehaviour
{
    Animator animator;
    bool isIdling;
    bool isRunning,isbackRunning, isJumping;
    Vector3 gravityVector;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isIdling)
        {
            isJumping = true;
            isIdling = false;
            animator.SetTrigger("N.idleJump");
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && isRunning)
        {
            

            animator.SetTrigger("N.runJump");
                     
        }
      
        if (Input.GetKeyDown(KeyCode.Space) && isbackRunning)
        {
            //isbackRunning = false;
            isJumping = true;
            animator.SetTrigger("N.idleJump");

        }

        //MOVEMENT
        if (Input.GetKey(KeyCode.W))
        {
            isIdling = false;
            isbackRunning = false;
            isRunning = true;
            animator.SetBool("N.Idle", false);
            animator.SetBool("N.run", true);
           
        }
        else
        {
             animator.SetBool("N.run", false);
             animator.SetBool("N.Idle", true);
             isIdling = true;
             isRunning = false;

        }

        if (Input.GetKey(KeyCode.S))
        {

            isIdling = false;
            isbackRunning = true;
            animator.SetBool("N.Idle", false);
            animator.SetBool("N.backRun", true);

        }
        else
        {
 
            animator.SetBool("N.backRun", false);
            animator.SetBool("N.Idle", true);
            isIdling = true;
            isbackRunning = false;
            
        }

        if (Input.GetKey(KeyCode.D))
        {

            isIdling = false;
            isbackRunning = true;
            animator.SetBool("N.Idle", false);
            animator.SetBool("N.rightStrafe", true);

        }
        else
        {

            animator.SetBool("N.rightStrafe", false);
            animator.SetBool("N.Idle", true);
            isIdling = true;
            isbackRunning = false;

        }

        if (Input.GetKey(KeyCode.A))
        {

            isIdling = false;
            isbackRunning = true;
            animator.SetBool("N.Idle", false);
            animator.SetBool("N.leftStrafe", true);

        }
        else
        {

            animator.SetBool("N.leftStrafe", false);
            animator.SetBool("N.Idle", true);
            isIdling = true;
            isbackRunning = false;

        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("block0");
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("N.Idle", false);
            animator.SetBool("blockIdle0", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("N.Idle", true);
            animator.SetBool("blockIdle0", false);
        }

    }
    private void FixedUpdate()
    {
        gravityVector = Vector3.zero;
        if (!characterController.isGrounded && !isJumping)
        {
            gravityVector += Physics.gravity;
        }
        characterController.Move(gravityVector);
        
    }
    public void NoGravity()
    {
        isJumping = true;
        isIdling = false;
        Debug.Log("No gravity");
    }
    public void AddGravity()
    {
        isJumping = false;
        isIdling = true;
        Debug.Log("gravity Added");
    }
    
}
