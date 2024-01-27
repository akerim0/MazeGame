using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantHealthSystem : MonoBehaviour
{
    private int Maxhealth = 100;
    int Currenthealth;
    int health;
    private void Start()
    {
        Currenthealth = Maxhealth;
    }

    public void takeDamage(int damageAmount)
    {
        Currenthealth -= damageAmount;
        if (Currenthealth <= 0)
        {
            Currenthealth = 0;
        }
    }
    public int getHealth()
    {
        return Currenthealth;
    }
}
