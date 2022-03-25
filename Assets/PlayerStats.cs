
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
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

    public void Heal(float heal)
    {
        Health += heal;
        CheckOverHeal();
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
