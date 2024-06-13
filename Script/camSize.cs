using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camSize : MonoBehaviour
{
    public float newCameraSize = 3f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camZoom cameraZoom = Camera.main.GetComponent<camZoom>();

            if (cameraZoom != null)
            {
                cameraZoom.SetCameraSize(newCameraSize);
            }
        }
    }

}
