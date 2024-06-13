using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class controller : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 LastDirection;
    private Animator animator;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public float jumpForce = 10f;

    public Slider healthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider AwakenessSlider;
    public float maxAwakeness = 0f;
    public float currentAwakeness;
    public float normalSpeed = 2.5f;
    public float plusSpeed = 2.5f;

    public GameObject arrowPrefab;
    public float attackForce = 20f;
    public float spawnOffset = 1f;
    public float cooldownTime = 3f;
    private float lastShotTime;
    public int killcount = 0; 
    public TextMeshProUGUI winText;

    public Transform teleportLocation1;
    public Transform teleportLocation2;
    public Transform teleportDestination;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        currentAwakeness = 30;
        AwakenessSlider.maxValue = maxAwakeness;
        AwakenessSlider.value = currentAwakeness;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        bool TopDown = animator.GetBool("IsTopDown");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        moveDirection = new Vector2(moveX, moveY).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !TopDown)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = normalSpeed + plusSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        if (moveDirection != Vector2.zero)
        {
            LastDirection = moveDirection;
            animator.SetFloat("LastX", LastDirection.x);
            animator.SetFloat("LastY", LastDirection.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        animator.SetFloat("X", moveX);
        animator.SetFloat("Y", moveY);

        if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + cooldownTime)
        {
            PlayerAttack();
        }

        if(currentAwakeness >= 100)
        {
            winText.text = "You win!";
        }

        if (currentAwakeness >= 80f)
        {
            TeleportPlayer();
        }
    }

    public void SetBool(string parameterName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(parameterName, value);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
        }
    }

    public void heal(float healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        healthSlider.value  = currentHealth;
    }
    
    public void mentalDamage(float damage)
    {
        currentAwakeness -= damage;
        currentAwakeness = Mathf.Clamp(currentAwakeness, 0f, maxAwakeness);
        AwakenessSlider.value = currentAwakeness;

        if(currentAwakeness <= 0)
        {
            Debug.Log("You are no longer awake");
        }
    }

    public void mentalHealing(float healing)
    {
        currentAwakeness += healing;
        currentAwakeness = Mathf.Clamp(currentAwakeness, 0f, maxAwakeness);
        AwakenessSlider.maxValue = currentAwakeness;
    }

    void PlayerAttack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;

        Vector3 spawnPosition = transform.position + (Vector3)direction * spawnOffset;

        GameObject arrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = direction * attackForce;

        lastShotTime = Time.time;
    }

    public void IncrementKillCount()
    {
        killcount++;

        if (killcount >= 50)
        {
            Debug.Log("Teleporting player...");

            float randomValue = Random.value;

            Transform destination = randomValue < 0.5f ? teleportLocation1 : teleportLocation2;

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            player.transform.position = destination.position;

            killcount = 0;
        }
    }

    public void TeleportPlayer()
    {
        Debug.Log("Teleporting player...");

        transform.position = teleportDestination.position;
    }
}
