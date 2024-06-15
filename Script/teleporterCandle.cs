using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporterCandle : MonoBehaviour
{
    public Transform teleportLocation1;
    public Transform teleportLocation2;
    public float healingAmount = 50f;
    public float healingAwakness = 10f;
    public bool TopDown = true;
    public float newCameraSize = 2.5f;
    public Vector3 newPlayerScale = new Vector3(5f, 5f, 1.0f); 
    public BouncingObject[] bouncingObjects;
    public enemyFiring enemyFiring;
    public float newCameraSize2 = 3f;
    public EnemySpawner[] enemySpawners;
    public Dropper[] droppers;
    public Vector3 PlatformPlayerScale = new Vector3(5f, 5f, 1.0f); 
    public float newCameraSize3 = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        controller player = other.GetComponent<controller>();
        if (other.CompareTag("Player"))
        {
            player.heal(healingAmount);
            player.mentalHealing(healingAwakness);
            TeleportPlayer(other.transform, player);
        }
    }

    private void TeleportPlayer(Transform playerTransform, controller player)
    {
        float randomValue = Random.value;
        Transform destination = randomValue < 0.5f ? teleportLocation1 : teleportLocation2;
        Rigidbody2D playerRb = playerTransform.GetComponent<Rigidbody2D>();

        foreach (BouncingObject bouncingObject in bouncingObjects)
        {
            bouncingObject.isBouncing = false;
        }

        if (destination == teleportLocation1)
        {
            playerRb.gravityScale = 1f;
            player.SetBool("IsTopDown", false);

            camZoom cameraZoom = Camera.main.GetComponent<camZoom>();
            if (cameraZoom != null)
            {
                cameraZoom.SetCameraSize(newCameraSize3);
            }

            foreach (Dropper dropper in droppers)
            {
                dropper.active = true;
            }

            playerTransform.localScale = newPlayerScale;
        }
        else
        {
            playerRb.gravityScale = 0f;
            camZoom cameraZoom = Camera.main.GetComponent<camZoom>();
            player.SetBool("IsTopDown", TopDown);
            foreach (var spawner in enemySpawners)
            {
                spawner.spawning = true;
            }

            if (cameraZoom != null)
            {
                cameraZoom.SetCameraSize(newCameraSize2);
            }
        }
        playerTransform.position = destination.position;
    }
}
