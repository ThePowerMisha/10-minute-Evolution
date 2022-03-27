using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
  Противник любой
  у каждого свой тип выстрела / атаки
  Еще в зависимости от сложности надо босса делать
 */


[System.Serializable]
public class EnemyBase : MonoBehaviour
{
    private Rigidbody2D rb;

    /// <summary>
    /// Характеристики противника
    /// </summary>
    public EnemyData enemyData;

    /// <summary>
    /// События вызываемы При очистке карты
    /// </summary>
    public MassDeathUnityEvent massDeath;

    /// <summary>
    ///  Прекращение спавна противников
    /// </summary>
    public StopSpawnUnityEvent stopSpawn;

    protected HeathSystem _heathSystem;
    protected GameObject _enemTarget;


    public EnemyBase()
    {
    }

    private Coroutine shootCoroutine;



    public virtual void StopSpawnEnemy()
    {
        stopSpawn?.Invoke(enemyData.enemyProjectile);
    }

    public virtual IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyData.enemyCoolDown);
            if (Vector2.Distance(transform.position, _enemTarget.transform.position) < 10)
            {
                var projectile = enemyData.enemyProjectile;
                projectile.targetTag = "Player";
                var position = this.transform.position;
                Vector2 direction = (_enemTarget.transform.position - position).normalized;
                GameObject shoot = Instantiate(projectile.gameObject, position, Quaternion.identity);
                shoot.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                shoot.GetComponent<Rigidbody2D>().velocity = direction * projectile.speed;
            }
        }
    }

    public virtual void Move()
    {
        
            rb.MovePosition(transform.position + (_enemTarget.transform.position - transform.position).normalized *
                (enemyData.enemySpeed * Time.deltaTime));
    }

    public void Update()
    {
        if (_enemTarget != null)
        {
            Move();
        }
        else
        {
            StopCoroutine(shootCoroutine);
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        _heathSystem = gameObject.GetComponent<HeathSystem>();
        _heathSystem.maxHealth = enemyData.enemyHealth;
        _heathSystem.health = enemyData.enemyHealth;
        _enemTarget = GameObject.Find("Character");
        rb = this.GetComponent<Rigidbody2D>();
        shootCoroutine = StartCoroutine(Shoot());
    }

    private void OnDestroy()
    {
        if (shootCoroutine != null) StopCoroutine(shootCoroutine);
        var score = GameObject.FindWithTag("Score");
        if (score != null)
        {
            //coins.GetComponent<Text>().text =
            //    (enemyData.enemyCoins + int.Parse(coins.GetComponent<Text>().text)).ToString();
            score.GetComponent<Text>().text =
                (enemyData.enemyScore + int.Parse(score.GetComponent<Text>().text)).ToString();
        }
    }
}

    
    

