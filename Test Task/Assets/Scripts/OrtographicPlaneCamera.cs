using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class OrtographicPlaneCamera : MonoBehaviour
{
    [SerializeField]
    private bool uniform = true;
    [SerializeField]
    private bool autoSetUniform = false;
    private Camera camera2D; 

    private void Awake()
    {
        camera2D = Camera.main;
        camera2D.orthographic = true;

        if (uniform)
            SetUniform();
    }
    private void LateUpdate()
    {
        if (autoSetUniform && uniform)
            SetUniform();
    }
    private void SetUniform()
    {
        float orthographicSize = camera2D.pixelHeight / 2;
        if (orthographicSize != camera2D.orthographicSize)
            camera2D.orthographicSize = orthographicSize;
    }
}

