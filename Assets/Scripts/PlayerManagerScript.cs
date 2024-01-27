using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagerScript : MonoBehaviour
{
    public GameObject healthObj;
    [SerializeField] GameObject handSword, waistSword,swordRaycast;
    public Slider slider;
    Animator animatorController;
    bool fightMode, normalMode;
    bool isDead = false;
    public bool isvisible;
    HealthSystem healthSystem;
    [SerializeField] private float turnVelocity = 30.0f;
    [SerializeField] private int damageAmount;
    [SerializeField] private float health;
    [SerializeField] private int healingAmount = 2;
    // Start is called before the first frame update
    void Start()
    {
        animatorController = GetComponent<Animator>();
        this.GetComponent<SwordPlayerController>().enabled = false;
        this.GetComponent<NoSwordPlayerController>().enabled = true;
        //healthSystem = healthObj.GetComponent<HealthSystem>();
        waistSword.SetActive(true);
        handSword.SetActive(false);
        swordRaycast.SetActive(false);
        normalMode = true;
        fightMode = false;
        isvisible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Switch Modes
        if (Input.GetKeyDown(KeyCode.Tab) && normalMode)
        {
            Debug.Log("Tab Pressed");
            animatorController.SetTrigger("drawSword");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && fightMode == true)
        {
            Debug.Log("Tab Pressed");
            animatorController.SetTrigger("sheathSword");
        }
        
        //Turn
        this.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * turnVelocity * Time.deltaTime );
        
    }
    void DrawSword()
    {
        waistSword.SetActive(false);
        handSword.SetActive(true);
        swordRaycast.SetActive(true);
       // GetComponent<PlayerControlScript>().enabled = true;
        this.GetComponent<SwordPlayerController>().enabled = true;
        this.GetComponent<NoSwordPlayerController>().enabled = false;
        fightMode = true;
        normalMode = false;
    }

    void SheathSword()
    {
        waistSword.SetActive(true);
        handSword.SetActive(false);
        swordRaycast.SetActive(false);
        //GetComponent<PlayerControlScript>().enabled = false;
        this.GetComponent<SwordPlayerController>().enabled = false;
        this.GetComponent<NoSwordPlayerController>().enabled = true;
        fightMode = false;
        normalMode = true;
    }
    public void TakeDamage(string enemy)
    {
        if (enemy == "zombie")
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
        slider.value = health / 100.0f;
    }
    public void Die()
    {
        animatorController.SetTrigger("death");
        isDead = true;

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
    
   
}
