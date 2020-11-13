using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform destination;
    public NavMeshAgent agent;

    private void Awake()
    {
        TryGetComponent(out agent);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(destination.position);
    }
}
