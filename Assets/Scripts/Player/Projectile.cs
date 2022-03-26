using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [Header("Минимальный Урон снаряда")] public float minDamage;
    [Header("Максимальный Урон снаряда")] public float maxDamage;
    [Header("Скорость снаряда")] public float speed;
    [Header("Время жизни снаряда")] public float lifetime = 4f;

    [Header("Спрайт снаряда")] public GameObject sprite;

    //[Header("Направление снаряда")] public Vector3 direction;
    [Header("Цель снаряда")] public GameObject target;
    [Header("Тэг цели")] public string targetTag;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Проверка коллизии
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //other.CompareTag(target.tag) ||
        if (other.CompareTag(targetTag))
        {
            if (other.GetComponent<HeathSystem>() != null)
            {
                other.GetComponent<HeathSystem>().DealDamage(Random.Range(minDamage, maxDamage));
            }

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Создаем снаряд который летит в цель
    /// </summary>
    public virtual void CreateProjectile(Vector3 position)
    {
        Vector2 direction = (target.transform.position - position).normalized;
        GameObject shoot = Instantiate(gameObject, position, Quaternion.identity);
        shoot.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        shoot.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    /// <summary>
    /// Создаем снаряд в направлении мыши
    /// </summary>
    public virtual void CreateProjectileToMousePosition(Vector3 position)
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - position).normalized;
        GameObject shoot = Instantiate(gameObject, position, Quaternion.identity);
        shoot.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        shoot.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    /// <summary>
    /// Создаем снаряд в случайном направлении 
    /// </summary>
    public virtual void CreateProjectileToRandomDirection(Vector3 position = default)
    {
        GameObject shoot = Instantiate(gameObject, position, Quaternion.identity);
        Vector2 direction = (new Vector2(Random.Range(-360, 360), Random.Range(-360, 360))).normalized;
        shoot.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        shoot.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
