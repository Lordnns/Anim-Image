using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TreeQuadController3 : MonoBehaviour
{
    public float speed = 0.2f; // Vitesse du défilement
    private float angle;
    private Material material;

    public void Start()
    {
        var mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f,  0.5f, 0),
            new Vector3( 0.5f,  0.5f, 0),
            new Vector3( 0.5f, -0.5f, 0),
        };

        mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 };

        mesh.uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
        };

        var meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(meshRenderer.sharedMaterial);
        meshRenderer.material = material;
    }

    public void Update()
    {
        angle += Time.deltaTime * speed;
        material.SetFloat("_TimeValue", Time.time * speed);

        var modelView = Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));
        material.SetMatrix("modelMatrix", modelView);
    }
}