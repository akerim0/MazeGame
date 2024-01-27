using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int Maxhealth = 100;
    int Currenthealth;
    int health;
    bool canheal;
    private void Start()
    {
        Currenthealth = Maxhealth;
    }
    
    public void takeDamage(int damageAmount)
    {
        Currenthealth -= damageAmount; 
        if(Currenthealth <= 0)
        {
            Currenthealth = 0;
        }
    }

    public void heal(int healAmount)
    {
        if (canheal)
        {
            Currenthealth += healAmount;

            if (Currenthealth >= 100)
            {
                Currenthealth = 100;
                canheal = false;
                Debug.Log("Healing finsished !");
            }
            
        }
        if (Currenthealth <= 99)
        {
            canheal = true;
        }

    }

   
    public int getHealth()
    {
        return Currenthealth;
    }
    
}
