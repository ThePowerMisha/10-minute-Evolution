using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Turrets : EnemyBase
    {
        public int minShellCount = 0;
        public int maxShellCount = 10;
        public override IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(enemyData.enemyCoolDown);
                if (!(Vector2.Distance(transform.position, _enemTarget.transform.position) < 15)) continue;
                for (int i = 0; i < Random.Range(minShellCount, maxShellCount); i++)
                {
                    var projectile = enemyData.enemyProjectile;
                    projectile.targetTag = "Player";
                    GameObject shoot = Instantiate(projectile.gameObject, this.transform.position, Quaternion.identity);
                    Vector2 direction = (new Vector2(Random.Range(-360, 360), Random.Range(-360, 360))).normalized;
                    shoot.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                    shoot.GetComponent<Rigidbody2D>().velocity = direction * projectile.speed;
                }
            }
        }
        
    }
}
