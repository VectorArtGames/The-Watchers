using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(Portals))]
[CanEditMultipleObjects]
public class PortalsInspector : Editor
{
    public override void OnInspectorGUI()
    {

        if (!(target is Portals portal)) return;

        base.DrawDefaultInspector();
        serializedObject.Update();
        GUILayout.Space(15f);
        GUILayout.Label("Debugging");
        if (GUILayout.Button("Scan"))
        {
            portal.ScanPortals();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
