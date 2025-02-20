using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CustomMatrices : MonoBehaviour
{
    private Camera cam;

    public Matrix4x4Container projectionMatrix;

    public void Start()
    {
        cam = GetComponent<Camera>();
        //Debug.Log(transform.localToWorldMatrix);    // World
        //Debug.Log(cam.worldToCameraMatrix);         // View
        Debug.Log(cam.projectionMatrix);            // Projection
    }

    public void Update()
    {
        cam.projectionMatrix = projectionMatrix;
    }
}
