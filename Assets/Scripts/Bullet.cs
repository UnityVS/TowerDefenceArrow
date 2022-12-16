using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _bulletSpeed = 1f;
    public void FlyToEnemy()
    {
    }
    public IEnumerator WaitToCollapse(Enemy enemy, float attackDamage)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.007f);
            if (enemy != null)
            {
                transform.Translate((enemy.transform.position - transform.position).normalized * _bulletSpeed);
                if (Vector3.Distance(transform.position, enemy.transform.position) < 0.5f && enemy != null)
                {
                    enemy.GetHealthInfo().TakeDamage(attackDamage);
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
