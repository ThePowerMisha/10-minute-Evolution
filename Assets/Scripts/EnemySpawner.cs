using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyBase> Enemy = new List<EnemyBase>();
    public int maxEnemy = 2;
    public int enemyCount = 0;
    public GameObject player;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        for (int i = enemyCount;
            i < maxEnemy;
            i++)
        {
            var randPosition = new Vector2(player.transform.position.x + Random.Range(-10, 10),
                player.transform.position.y + Random.Range(-10, 10));
            if(Enemy.Count != 0) Instantiate(Enemy[Random.Range(0,Enemy.Count)].gameObject, randPosition, Quaternion.identity);
            enemyCount++;
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount < maxEnemy)
            {
                SpawnEnemy();
            }
        }
    }
}
