using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillActions : MonoBehaviour
{
    public GameObject projectile;
    public int minDamage;
    public int maxDamage;

    public void SpawnArrows()
    {
        var ArrowCount = Random.Range(10, 15);
        for (int i = 0; i <= ArrowCount; i++)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject shoot = Instantiate(projectile, mousePos, Quaternion.identity);
            Vector2 myPos = transform.position;
            Vector2 direction =(  new Vector2( Random.Range(-360, 360), Random.Range(-360,360))).normalized;
            
            shoot.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            
            shoot.GetComponent<Rigidbody2D>().velocity = direction * Random.Range(1, 7);
            shoot.GetComponent<Projectile>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
        }
    }
}
