using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BlueQuadController : MonoBehaviour
{
    public float speed = 10.0f;
    private float angle;

    private Material material;

    public void Start()
    {
        var mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
        };

        mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 };

        var meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(meshRenderer.sharedMaterial);
        meshRenderer.material = material;
    }

    public void Update()
    {
        angle += Time.deltaTime * speed;
        var modelView = Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));
        material.SetMatrix("modelMatrix", modelView);
    }
}
