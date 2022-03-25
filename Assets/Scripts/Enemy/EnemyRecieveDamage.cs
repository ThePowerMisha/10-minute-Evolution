using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    
    void Start()
    {
        Health = MaxHealth;
    }
    
    public void DealDamage(float damage)
    {
        Health -= damage;
        CheckDeath();
    }

    private void CheckOverHeal()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    private void CheckDeath()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
