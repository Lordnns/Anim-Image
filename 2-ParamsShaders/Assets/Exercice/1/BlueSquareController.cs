using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class BlueSquareController : MonoBehaviour
{
    public float speed = 0.5f;
    private Material material;

    public void Start()
    {
        var mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = new Vector3[]
        {
            new Vector3(-0.5f,  0.5f, 0),
            new Vector3( 0.5f,  0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3( 0.5f, -0.5f, 0)
        };
        
        mesh.triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2 
        };

        var meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(meshRenderer.sharedMaterial);
        meshRenderer.material = material;
    }

    public void Update()
    {

        float xOffset = Time.time * speed;

        var modelView = Matrix4x4.Translate(new Vector3(xOffset, 0, 0));
        material.SetMatrix("modelMatrix", modelView);
    }


}
