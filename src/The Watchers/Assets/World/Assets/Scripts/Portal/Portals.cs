using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using UnityEditor;

using UnityEngine;

[ExecuteInEditMode]
public class Portals : MonoBehaviour
{
    public static Portals Instance { get; set; }
    public PortalIdentity[] portals;
    private void Awake()
    {
        portals = GameObject.FindObjectsOfType<PortalIdentity>();
        Instance = this;
    }

    public void ScanPortals()
    {
        portals = GameObject.FindObjectsOfType<PortalIdentity>();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (portals == null || portals != null && portals.Length <= 0) return;

        var p = portals.Where(x => x != null).ToArray();
        if (p == null) return;

        for (var i = 0; i < p.Length; i++)
        {
            for (var j = i + 1; j < p.Length; j++)
            {
                if (p.Length < i || p.Length < i) return;

                if (p[i].ID == p[j].ID)
                {
                    var a = p[i];
                    var b = p[j];
                    Gizmos.DrawLine(a.transform.position, b.transform.position);
                }
            }
        }
    }

#endif

    public PortalIdentity GetByID(PortalIdentity self)
    {
        var p = portals.ToArray();
        for (var i = 0; i < p.Length; i++)
        {
            if (self.ID == p[i].ID && self != p[i])
            {
                return p[i];
            }
        }

        return null;
    }
}

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
