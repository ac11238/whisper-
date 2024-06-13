using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finalcollision : MonoBehaviour
{
    public float healingAwakness = 20f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        controller player = other.GetComponent<controller>();
        if (other.CompareTag("Player"))
        {
            player.mentalHealing(healingAwakness);
        }
    }

}
