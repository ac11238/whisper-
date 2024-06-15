using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor : MonoBehaviour
{
    public Transform teleportTarget;
    public bool TopDown = false;
    public float newCameraSize = 2.5f;
    public Vector3 newPlayerScale = new Vector3(5f, 5f, 1.0f); 
    public BouncingObject[] bouncingObjects;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player tag detected.");
            controller player = collision.gameObject.GetComponent<controller>();
            camZoom cameraZoom = Camera.main.GetComponent<camZoom>();

            if (cameraZoom != null)
            {
                cameraZoom.SetCameraSize(newCameraSize);
            }

            if (player != null)
            {
                collision.gameObject.transform.position = teleportTarget.position;
                player.SetBool("IsTopDown", TopDown);
                collision.transform.localScale = newPlayerScale;
            }

            foreach (BouncingObject bouncingObject in bouncingObjects)
            {
                bouncingObject.isBouncing = true;
            }
        }
    }
}
