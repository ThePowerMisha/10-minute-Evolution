using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public LocationManager locationManager;

    public int maxEnemy = 2;
    public int enemyCount = 0;
    public GameObject player;
    public float enemyRespawnTime = 2f;
    private Coroutine spawnCoroutine;
    private bool playerDead;


    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyRespawnTime);
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount < maxEnemy){
                var randPosition = new Vector2(player.transform.position.x + Random.Range(-10, 10),
                    player.transform.position.y + Random.Range(-10, 10));

                Instantiate(locationManager.GetRandomEnemy(), randPosition, Quaternion.identity);

                enemyCount++;
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnCoroutine);
    }
    private void OnDestroy()
    {
        StopCoroutine(spawnCoroutine);
    }
}
