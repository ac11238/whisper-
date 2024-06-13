using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderingEnemy : MonoBehaviour
{
    public bool attacking = false;
    public GameObject ammoPrefab;
    public Transform player;
    public float attackDistance = 5f; 
    public float attackRange = 3f; 
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
        else
        {
            Debug.LogError("Could not find controller object with tag 'Player'.");
        }
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
            if (distanceToPlayer <= attackDistance)
            {

                ChasePlayer();
            }
            else
            {

                Wander();
            }
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
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Controller.IncrementKillCount();
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
        Vector2 targetPosition = player.position + (transform.position - player.position).normalized * attackRange;
        
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;
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
