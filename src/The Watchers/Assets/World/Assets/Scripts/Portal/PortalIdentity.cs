using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PortalIdentity : MonoBehaviour
{
    public string ID;
    private PortalCore core;
    private void Awake()
    {
        var c = core = GetComponentInChildren<PortalCore>();

        c.ID = ID;
    }
}
