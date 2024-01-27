using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    public abstract void Damage();
    public abstract void Die();
    public abstract void chase(Transform target);
    public abstract void attack(Transform target);
}
