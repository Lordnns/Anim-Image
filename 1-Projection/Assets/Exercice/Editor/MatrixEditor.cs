using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Matrix4x4Container), true)]
public class MatrixEditor : Editor
{
    const float CELL_HEIGHT = 16;

    SerializedProperty matrixProperty;

    public void OnEnable()
    {
        matrixProperty = serializedObject.FindProperty("matrix");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Matrix4x4 matrix = Matrix4x4.identity;

        for (int r = 0; r < 4; r++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int c = 0; c < 4; c++)
            {
                DrawCell(c, r);
                matrix[r, c] = matrixProperty.FindPropertyRelative("e" + r + c).floatValue;
            }
            EditorGUILayout.EndHorizontal();
        }

        Vector3 translation = matrix.GetColumn(3);

        Quaternion rotation = Quaternion.LookRotation(
            matrix.GetColumn(2),
            matrix.GetColumn(1)
        );

        Vector3 scale = new Vector3(
            matrix.GetColumn(0).magnitude,
            matrix.GetColumn(1).magnitude,
            matrix.GetColumn(2).magnitude
        );

        bool wasEnabled = GUI.enabled;
        GUI.enabled = false;
        translation = EditorGUILayout.Vector3Field("Translation", translation);
        rotation.eulerAngles = EditorGUILayout.Vector3Field("Rotation (Euler)", rotation.eulerAngles);
        rotation = Vector4ToQuaternion(EditorGUILayout.Vector4Field("Rotation (Quaternion)", QuaternionToVector4(rotation)));
        scale = EditorGUILayout.Vector3Field("Scale", scale);
        GUI.enabled = wasEnabled;


        if (GUILayout.Button("From Camera"))
        {
            matrix = Camera.main.projectionMatrix;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    matrixProperty.FindPropertyRelative("e" + r + c).floatValue = matrix[r, c];
                }
            }

        }

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        serializedObject.ApplyModifiedProperties();
    }

    void DrawCell(int column, int row)
    {
        EditorGUILayout.PropertyField(
            matrixProperty.FindPropertyRelative("e" + row + column),
            GUIContent.none
        );
    }

    static Vector4 QuaternionToVector4(Quaternion rot)
    {
        return new Vector4(rot.x, rot.y, rot.z, rot.w);
    }

    static Quaternion Vector4ToQuaternion(Vector4 v)
    {
        return new Quaternion(v.x, v.y, v.z, v.w);
    }

}