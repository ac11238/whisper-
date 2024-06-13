using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float arrowSpeed = 5f;
    public LayerMask obstacleLayer;
    public Transform player;
    public bool active = false;

    private float lastShootTime;

    void Update()
    {
        if (active)
        {
            MoveLeftAndRight();
            HandleShooting();
        }
    }

    void MoveLeftAndRight()
    {
        if (player != null)
        {
            float direction = player.position.x - transform.position.x;

            if (direction > 0)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else if (direction < 0)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
        }
    }

    void HandleShooting()
    {
        if (Time.time - lastShootTime >= 0.3f)
        {
            lastShootTime = Time.time;
            ShootArrow();
        }
    }

    void ShootArrow()
    {
        Vector2 direction = (player.position - arrowSpawnPoint.position).normalized;
    GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
    Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
    arrowRb.velocity = direction * arrowSpeed;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
