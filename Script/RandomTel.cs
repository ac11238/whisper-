using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTel : MonoBehaviour
{
    public Transform teleportLocation1;
    public Transform teleportLocation2;
    public float healingAmount = 50f;
    public float healingAwakness = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        controller player = other.GetComponent<controller>();
        if (other.CompareTag("Player"))
        {
            player.heal(healingAmount);
            player.mentalHealing(healingAwakness);
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform playerTransform)
    {
        float randomValue = Random.value;

        Transform destination = randomValue < 0.5f ? teleportLocation1 : teleportLocation2;

        playerTransform.position = destination.position;
    }
}
