using UnityEngine;
using UnityEngine.Events;

public class HeathSystem : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public UnityEvent onCharacterDead = new UnityEvent();

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckOverHeal();
        CheckDeath();
    }

    private void CheckOverHeal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;

            onCharacterDead?.Invoke();

            Destroy(this.gameObject);
        }
    }
}