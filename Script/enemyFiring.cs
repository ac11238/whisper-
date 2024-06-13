using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFiring : MonoBehaviour
{
    public bool attacking = false;
    public GameObject ammoPrefab;
    public Transform player; 
    public float launchForce = 10f; 
    public float spawnInterval = 5f;
    private float timer = 0f;
    public float HP = 100f;
    public Transform teleportTarget;
    public Vector3 teleportDestination;
    private controller Player;

    void start()
    {
        Player = player.GetComponent<controller>();
    }

    void Update()
    {
        if(attacking)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                attackFire();
                timer = 0f;
            }
        }
    }

    void attackFire()
    {
        if (ammoPrefab != null && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            GameObject spawnedAmmo = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = spawnedAmmo.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
            player.position = teleportDestination;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
