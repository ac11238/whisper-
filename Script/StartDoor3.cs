using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor3 : MonoBehaviour
{
    public Transform teleportTarget;
    public bool TopDown = false;
    public enemyFiring enemyFiring;
    public float newCameraSize = 3f;
    public EnemySpawner[] enemySpawners;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player tag detected.");
            controller player = collision.gameObject.GetComponent<controller>();

            if (player != null)
            {
                collision.gameObject.transform.position = teleportTarget.position;
                enemyFiring.attacking = true;

            foreach (var spawner in enemySpawners)
            {
                spawner.spawning = true;
            }

            camZoom cameraZoom = Camera.main.GetComponent<camZoom>();

            if (cameraZoom != null)
            {
                cameraZoom.SetCameraSize(newCameraSize);
            }
            }
        }
    }
}
