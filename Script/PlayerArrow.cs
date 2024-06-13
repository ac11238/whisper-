using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyFiring firingEnemy = collision.GetComponent<enemyFiring>();
            if (firingEnemy != null)
            {
                firingEnemy.TakeDamage(damage);
            }
            else
            {
                wanderingEnemy wanderingEnemy = collision.GetComponent<wanderingEnemy>();
                if (wanderingEnemy != null)
                {
                    wanderingEnemy.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}
