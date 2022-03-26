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

    public GameObject _enemTarget;


    public EnemyBase()
    {
    }

    private Coroutine shootCoroutine;

    private void Start()
    {
        _enemTarget = GameObject.Find("Character");
        rb = this.GetComponent<Rigidbody2D>();
        enemyData.enemyProjectile.target = _enemTarget;
        enemyData.enemyProjectile.targetTag = _enemTarget.tag;
        shootCoroutine = StartCoroutine(Shoot());
    }

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
                enemyData.enemyProjectile.CreateProjectile(transform.position);
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
            enemyData.enemyProjectile.target = _enemTarget;
            Move();
        }
        else
        {
            StopCoroutine(shootCoroutine);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (shootCoroutine != null) StopCoroutine(shootCoroutine);
        var coins = GameObject.FindWithTag("Coins");
        var score = GameObject.FindWithTag("Score");
        if (coins != null || score != null)
        {
            coins.GetComponent<Text>().text =
                (enemyData.enemyCoins + int.Parse(coins.GetComponent<Text>().text)).ToString();
            score.GetComponent<Text>().text =
                (enemyData.enemyScore + int.Parse(score.GetComponent<Text>().text)).ToString();
        }
    }
}

    
    

