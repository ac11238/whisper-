using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public bool isBouncing = true;
    public float speed = 5f;
    public float awakenDamage = 10;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D component found.");
            return;
        }

        rb.gravityScale = 0;
        rb.drag = 0; 
        rb.angularDrag = 0;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (isBouncing)
        {
            if (rb.velocity == Vector2.zero)
            {
                Vector2 initialDirection = Random.insideUnitCircle.normalized;
                rb.velocity = initialDirection * speed;
            }
            else
            {
                rb.velocity = rb.velocity.normalized * speed;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBouncing)
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 newVelocity = Vector2.Reflect(rb.velocity, normal).normalized * speed;
            rb.velocity = newVelocity;
        }

        controller control = collision.gameObject.GetComponent<controller>();
        if(control != null)
        {
            control.mentalDamage(awakenDamage);
        }
    }
}
