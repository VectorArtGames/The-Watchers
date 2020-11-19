using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(PortalCore))]
[CanEditMultipleObjects]
public class PortalCoreInspector : Editor
{
    SerializedProperty colorProperty;

    private void OnEnable()
    {
        colorProperty = serializedObject.FindProperty("PortalColor");
    }

    public override void OnInspectorGUI()
    {

        if (!(target is PortalCore portal)) return;

        base.DrawDefaultInspector();
        serializedObject.Update();
        GUILayout.Space(15f);
        GUILayout.Label("Debugging");
        EditorGUILayout.PropertyField(colorProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
