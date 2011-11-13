//AlmostLogical Software - http://www.almostlogical.com - support@almostlogical.com
using UnityEditor;
using UnityEngine;

//This code was derived from via reflection of the Unity framework
public class PPTransformInspector
{
    private bool firstSet = true;
    private Quaternion oldQuaternion;
    private Vector3 rotation;

    private Vector3 FixIfNaN(Vector3 v)
    {
        if (float.IsNaN(v.x))
        {
            v.x = 0f;
        }
        if (float.IsNaN(v.y))
        {
            v.y = 0f;
        }
        if (float.IsNaN(v.z))
        {
            v.z = 0f;
        }
        return v;
    }

    public void DrawDefaultInspector(Transform target)
    {
        EditorGUIUtility.LookLikeControls();
        EditorGUI.indentLevel = 0;

        if (this.firstSet || (this.oldQuaternion != target.localRotation))
        {
            this.firstSet = false;
            this.rotation = target.localEulerAngles;
            this.oldQuaternion = target.localRotation;
        }
        Vector3 v = EditorGUILayout.Vector3Field("Position", target.localPosition, new GUILayoutOption[0]);
        this.rotation = EditorGUILayout.Vector3Field("Rotation", this.rotation, new GUILayoutOption[0]);
        Vector3 vector2 = EditorGUILayout.Vector3Field("Scale", target.localScale, new GUILayoutOption[0]);

        if (GUI.changed)
        {
            Undo.RegisterUndo(target, "Transform Change");
            this.rotation = this.FixIfNaN(this.rotation);
            target.localPosition = this.FixIfNaN(v);
            target.localEulerAngles = this.rotation;
            this.oldQuaternion = target.localRotation;
            target.localScale = this.FixIfNaN(vector2);
        }

        EditorGUIUtility.LookLikeInspector();
    }
}
