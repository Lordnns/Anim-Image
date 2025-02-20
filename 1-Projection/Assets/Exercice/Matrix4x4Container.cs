using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Matrix4x4", order = 1)]
public class Matrix4x4Container : ScriptableObject
{
    public Matrix4x4 matrix = Matrix4x4.identity;

    public static implicit operator Matrix4x4(Matrix4x4Container m)
    {
        return m.matrix;
    }
}
