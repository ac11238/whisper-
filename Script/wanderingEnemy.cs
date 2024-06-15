using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderingEnemy : MonoBehaviour
{
    public bool attacking = false;
    public GameObject ammoPrefab;
    public Transform player;
    public float launchForce = 10f;
    public float spawnInterval = 5f;
    private float timer = 0f;
    public float HP = 100f;
    private controller Controller;

    public float speed = 2f;
    public float detectionRadius = 5f;
    public float changeDirectionTime = 2f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float changeDirectionTimer;
    public Transform teleportLocation1;
    public Transform teleportLocation2;
    public float minimumDistanceFromPlayer = 3f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        changeDirectionTimer = changeDirectionTime;
        ChangeDirection();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            Controller = Player.GetComponent<controller>();
        }

        attacking = true;
    }

    void Update()
    {
        changeDirectionTimer -= Time.deltaTime;

        if (changeDirectionTimer <= 0)
        {
            ChangeDirection();
            changeDirectionTimer = changeDirectionTime;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer > minimumDistanceFromPlayer)
            {
                ChasePlayer();
            }
            else
            {
                rb.velocity = Vector2.zero; 
            }
        }
        else
        {
            Wander();
        }

        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                AttackFire();
                timer = 0f;
            }
        }
    }

    void AttackFire()
    {
        Debug.Log("AttackFire called"); 
        if (ammoPrefab != null && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 spawnPosition = (Vector2)transform.position + direction * 1.5f; 

            GameObject spawnedAmmo = Instantiate(ammoPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = spawnedAmmo.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
                Debug.Log("Ammo fired"); 
            }
        }
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        if (Controller != null)
        {
            Controller.IncrementKillCount();
        }
        else
        {
            Debug.LogError("Controller is null");
        }
    }

    void ChangeDirection()
    {
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Wander()
    {
        rb.velocity = movement * speed;
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)player.position - direction * minimumDistanceFromPlayer;
        Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = moveDirection * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            ChangeDirection();
        }
    }

    public void TeleportPlayer()
    {
        float randomValue = Random.value;

        Transform destination = randomValue < 0.5f ? teleportLocation1 : teleportLocation2;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = destination.position;
    }
}
