using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(TentacleSkeleton))]
public class TentacleMeshGenerator : MonoBehaviour
{
    // Fonction pour générer un modèle de debug avec squelette. Tres brut.
    public void Start()
    {
        var indices = new List<int>();
        var positions = new List<Vector3>();
        var texCoords = new List<Vector2>();
        var bones = new List<Vector2>();
        var boneWeights = new List<Vector2>();

        var skeleton = GetComponent<TentacleSkeleton>();
        skeleton.PushBone(new Vector3(0.2f, 0f, 0f), Quaternion.identity);

        for (int b = 0; b < 5; b++)
        {
            if (b < 4)
            {
                var offset = new Vector3(0.4f, 0f, 0f);
                skeleton.PushBone(offset, Quaternion.identity);
            }

            for (int i = 0; i < 10; i++)
            {
                var absIdx = b * 10 + i;
                var u = absIdx / 67f;
                var x = u * 2f;

                // Tâtonnement avec une calculatrice graphique...
                var scaledY = Mathf.Sqrt(1f - Mathf.Pow(Mathf.Log(1.1f - u), 2f));
                var y = 0.125f * scaledY;
                var bone1 = b;
                var bone2 = b + 1;
                var boneW2 = i / 10f;
                var boneW1 = 1f - boneW2;

                positions.AddRange(new[] {
                    new Vector3(x, -y, 0f),
                    new Vector3(x, y, 0f),
                });
                texCoords.AddRange(new[] {
                    new Vector2(u, 0.5f - scaledY / 2f),
                    new Vector2(u, 0.5f + scaledY / 2f),
                });
                bones.AddRange(new[]
                {
                    new Vector2(bone1, bone2),
                    new Vector2(bone1, bone2),
                });
                boneWeights.AddRange(new[]
                {
                    new Vector2(boneW1, boneW2),
                    new Vector2(boneW1, boneW2),
                });
                if (absIdx > 0)
                {
                    indices.AddRange(new[]
                    {
                        absIdx * 2 - 2, absIdx * 2 - 1, absIdx * 2 + 0,
                        absIdx * 2 + 1, absIdx * 2 + 0, absIdx * 2 - 1,
                    });
                }
            }
        }

        var tentacleMesh = new Mesh();
        tentacleMesh.SetVertices(positions);
        tentacleMesh.SetUVs(0, texCoords);
        tentacleMesh.SetUVs(1, bones);
        tentacleMesh.SetUVs(2, boneWeights);
        tentacleMesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);

        GetComponent<MeshFilter>().mesh = tentacleMesh;
    }
}
