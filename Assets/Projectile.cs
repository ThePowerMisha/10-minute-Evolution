using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    private float _lifetime = 4f;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "Character" && other.name != gameObject.name)
        {
            if (other.GetComponent<EnemyRecieveDamage>() != null)
            {
                other.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
            Destroy(gameObject);
        }

        // if (Time.time > _lifetime)
        // {
        //     Destroy(gameObject);
        // }
    }
}
