using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class parabolaController : MonoBehaviour
{
    public int resolution = 20; // Number of points on the parabola
    public float width = 2.0f;
    public float a = 1.0f;

    private void Start()
    {
        GenerateParabola();
    }

    void GenerateParabola()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int vertexCount = (resolution + 1) * 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[resolution * 6]; // 2 triangles per segment

        float step = width / resolution;

        for (int i = 0; i <= resolution; i++)
        {
            float x = -width / 2 + i * step;
            float y = a * x * x; // Parabola equation

            vertices[i] = new Vector3(x, y, 0); // Parabola curve
            if (a >= 0)
            {
                vertices[i + resolution + 1] = new Vector3(x, 1, 0); // Base
            }
            else
            {
                vertices[i + resolution + 1] = new Vector3(x, -1, 0); // Base
            }
        }

        int triIndex = 0;
        for (int i = 0; i < resolution; i++)
        {

            int topLeft = i;
            int topRight = i + 1;
            int bottomLeft = i + resolution + 1;
            int bottomRight = i + resolution + 2;
            
            if (a >= 0)
            {
                // Normal order (CCW) when `a > 0`
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = topRight;

                triangles[triIndex++] = topRight;
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = bottomRight;
            }
            else
            {
                // Inverted order (CW) when `a < 0` to avoid flipping
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = topRight;
                triangles[triIndex++] = bottomLeft;

                triangles[triIndex++] = topRight;
                triangles[triIndex++] = bottomRight;
                triangles[triIndex++] = bottomLeft;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

}
