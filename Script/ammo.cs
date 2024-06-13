using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{
    public float radishDamage = 10f;
    public float speed = 10f;
    private Rigidbody2D rb;
    public controller playerHealth;
    public float mentDamage = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Random.Range(0f, 1f) < 0.75f) 
            {
                controller playerHealth = other.GetComponent<controller>();
                playerHealth.TakeDamage(radishDamage);
            }
            else 
            {
                controller playerHealth = other.GetComponent<controller>();
                playerHealth.mentalDamage(mentDamage);
            }
        }

        Destroy(gameObject);
    }

}
