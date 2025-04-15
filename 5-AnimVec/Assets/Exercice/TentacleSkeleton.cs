using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
public class TentacleSkeleton : MonoBehaviour
{
    private Material material;
    private List<Matrix4x4> boneTranslation = new List<Matrix4x4>();
    private List<Matrix4x4> boneRotation = new List<Matrix4x4>();
    private List<Matrix4x4> boneInverse = new List<Matrix4x4>();

    public Matrix4x4[] bones
    {
        get
        {
            var vals = new Matrix4x4[boneInverse.Count];
            var cumul = Matrix4x4.identity;
            for (int i = 0; i < boneInverse.Count; i++)
            {
                cumul *= boneRotation[i] * boneTranslation[i];
                vals[i] = cumul * boneInverse[i];
            }
            return vals;
        }
    }

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        material.SetMatrixArray("_Bones", bones);
    }

    public void PushBone(Vector3 position, Quaternion orientation)
    {
        var newTrans = Matrix4x4.Translate(position);
        boneTranslation.Add(newTrans);

        var newRot = Matrix4x4.Rotate(orientation);
        boneRotation.Add(newRot);

        var boneMat = newTrans * newRot;

        var parent = boneInverse.Count > 0 ? boneInverse[boneInverse.Count - 1] : Matrix4x4.identity;
        var invBone = boneMat.inverse * parent;

        boneInverse.Add(invBone);
    }

    public void SetRotation(int index, Quaternion orientation)
    {
        Debug.Assert(index >= 0 && index < boneRotation.Count);
        boneRotation[index] = Matrix4x4.Rotate(orientation);
    }
}
