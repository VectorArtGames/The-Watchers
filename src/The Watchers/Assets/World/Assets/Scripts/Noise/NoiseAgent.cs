using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoiseAgent : MonoBehaviour
{
    public float range;
    private CharacterController controller;
    public EnemyAI[] Enemies;
    private void Awake()
    {
        TryGetComponent(out controller);
        Enemies = FindObjectsOfType<EnemyAI>();
    }
    private void FixedUpdate()
    {
        if (controller == null) return;
        var v = controller.velocity;
        if (v.x != 0 || v.z != 0)
            GenerateNoise();
    }

    private void GenerateNoise()
    {
        foreach (var ai in (Enemies = FindObjectsOfType<EnemyAI>().Where(x => x != null && Vector3.Distance(x.transform.position, transform.position) < range).ToArray()))
        {
            ai?.AddNoise(transform.position);
            Debug.Log("Adding noise..");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}