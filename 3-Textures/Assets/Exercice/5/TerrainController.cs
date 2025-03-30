using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TerrainController : MonoBehaviour
{
    private Material material;
    public float rotationSpeed = 30f;
    
    public int Size = 10;
    void Start()
    {
        Mesh mesh = CreateGridMesh(Size);
        GetComponent<MeshFilter>().mesh = mesh;

        var meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(meshRenderer.sharedMaterial);
        meshRenderer.material = material;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }



    public static Mesh CreateGridMesh(int x)
    {
        Mesh mesh = new Mesh();

        int vertCount = (x + 1) * (x + 1);
        Vector3[] vertices = new Vector3[vertCount];
        Vector2[] uvs = new Vector2[vertCount];
        int[] triangles = new int[x * x * 6]; // 2 triangles per quad

        int index = 0;
        for (int y = 0; y <= x; y++)
        {
            for (int i = 0; i <= x; i++)
            {
                vertices[index] = new Vector3((float)i / x - 0.5f, (float)y / x - 0.5f, 0);
                uvs[index] = new Vector2((float)i / x, (float)y / x);
                index++;
            }
        }

        int triIndex = 0;
        for (int y = 0; y < x; y++)
        {
            for (int i = 0; i < x; i++)
            {
                int topLeft = y * (x + 1) + i;
                int bottomLeft = (y + 1) * (x + 1) + i;

                // First triangle
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = topLeft + 1;
                triangles[triIndex++] = bottomLeft + 1;

                // Second triangle
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = bottomLeft + 1;
                triangles[triIndex++] = bottomLeft;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

}
