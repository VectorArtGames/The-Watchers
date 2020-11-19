using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NoiseAlert : MonoBehaviour
{
    public void ChangeDesination(Vector3 src)
    {
        transform.position = src;
    }

    public void SetState(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
