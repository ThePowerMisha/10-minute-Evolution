using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage;
    private float _lifetime = 4f;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Character" && !other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
