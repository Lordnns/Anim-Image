using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DollyZoomMatrix : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    public Matrix4x4Container projectionMatrix;

    [Header("Effect Adjustments")]
    [Range(0.1f, 10f)] public float effectMultiplier = 1.0f; // Strength of the effect
    public float scrollSpeed = 2.0f; // Speed of zoom with mouse wheel

    private float initialDistance;
    private bool dzEnabled = false;
    private float initialFOV;
    private Matrix4x4 originalProjection;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (target != null)
        {
            StartDZ();
        }

        // Store the original projection matrix
        originalProjection = cam.projectionMatrix;
    }

    public void StartDZ()
    {
        initialDistance = Vector3.Distance(transform.position, target.position);
        initialFOV = cam.fieldOfView;
        dzEnabled = true;
    }

    public void StopDZ()
    {
        dzEnabled = false;
        cam.ResetProjectionMatrix();
    }

    void Update()
    {
        if (!dzEnabled || target == null)
            return;

        // Mouse Scroll Input for Moving the Camera Forward/Backward
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            transform.position += transform.forward * scrollInput * scrollSpeed;
        }

        // Calculate the new distance from the camera to the target
        float currentDistance = Vector3.Distance(transform.position, target.position);

        // Compute a more aggressive scale factor
        float scaleFactor = Mathf.Tan(initialFOV * 0.5f * Mathf.Deg2Rad) * (initialDistance / currentDistance) * effectMultiplier;

        // Clone the original projection matrix and modify it
        Matrix4x4 modifiedMatrix = originalProjection;
        modifiedMatrix.m00 = originalProjection.m00 / scaleFactor; // Horizontal FOV widening
        modifiedMatrix.m11 = originalProjection.m11 / scaleFactor; // Vertical FOV widening

        // Apply the modified projection matrix to the camera
        cam.projectionMatrix = modifiedMatrix;
    }
}