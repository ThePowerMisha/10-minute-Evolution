using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //public Transform player;
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;

    public float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootPlayer());
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldown);
        if ( GameObject.Find("Character") != null )
        {
            Vector2 myPos = transform.position;
            var charact = GameObject.Find("Character");
            Vector2 targetPos = charact.GetComponent<Transform>().position;
            if (Vector2.Distance(myPos,targetPos) < 10)
            {
                Vector2 direction = (targetPos - myPos).normalized;

                GameObject shoot = Instantiate(projectile, transform.position, Quaternion.identity);
                shoot.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                shoot.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                shoot.GetComponent<EnemyProjectile>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
                
            }
            StartCoroutine(ShootPlayer());
        }
    }
}
