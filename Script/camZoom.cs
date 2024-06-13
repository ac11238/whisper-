using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camZoom : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    public void SetCameraSize(float newSize)
    {
        _camera.orthographicSize = newSize;
    }
}
