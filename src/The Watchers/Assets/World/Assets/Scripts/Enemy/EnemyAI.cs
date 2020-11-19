using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using static EnemyAIState;

public class EnemyAI : MonoBehaviour, INoiseAI
{
    /*
     * If can see player, increase aggression.
     * 
     */
    public EnemyAIState CurrentState = IdleState;

    public float SightRange;
    private PlayerMovement player;

    // Aggression - Value between (0.00) to (1.00)
    public float _aggression;
    public float aggression
    {
        get => _aggression;
        set => _aggression = Mathf.Clamp(value, 0f, 1f);
    }

    public Transform destination;
    public NavMeshAgent agent;

    private Vector3 LastSeenPoint;

    private Vector3 initialPosition;
    private NoiseAlert alert;

    private void Awake()
    {
        TryGetComponent(out agent);
        player = GameObject.FindObjectOfType<PlayerMovement>();
        initialPosition = transform.position;
        alert = transform.parent.GetComponentInChildren<NoiseAlert>();
    }

    void Start()
    {
        agent.SetDestination(destination.position);
    }

    private void FixedUpdate()
    {
        NextAction();
    }

    private void NextAction()
    {
        switch (CurrentState)
        {
            case IdleState:
                Idle();
                break;
            case Attacking:

                break;
            case Chasing:
                Chase();
                break;
            case Discovering:
                if (startedDeAggro) break;
                startedDeAggro = true;
                StartCoroutine(nameof(DeAggro));
                break;
            case Reset:
                WalkBack();
                break;
        }
    }

    private bool startedDeAggro;

    private IEnumerator DeAggro()
    {
        yield return new WaitForSeconds(2.0f);
        if (CurrentState != Discovering) yield return null;
        CurrentState = Lost;
        while (aggression > 0)
        {
            aggression -= Time.fixedDeltaTime;
        }
        CurrentState = Reset;
        startedDeAggro = false;
    }

    private void WalkBack()
    {
        if (CurrentState != Reset) return;

        if (agent.destination != initialPosition)
            agent.SetDestination(initialPosition);

        Debug.Log(agent.pathStatus.ToString());

        if (agent.remainingDistance <= 0.5f)
            CurrentState = IdleState;

        alert.SetState(false);
    }

    private void Idle()
    {
        alert.SetState(false);
    }
    private void Chase()
    {

        if (agent.destination != LastSeenPoint && agent.SetDestination(LastSeenPoint))
            alert?.ChangeDesination(LastSeenPoint);

        Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance <= 0.5)
        {
            CurrentState = Discovering;
            agent.speed = 5f;
        }
        else
        {
            agent.speed = 15f;
        }

        alert.SetState(true);
    }

    public void AddNoise(Vector3 point)
    {
        if (CurrentState == Lost) return;

        LastSeenPoint = point;
        Debug.Log("Noise");
        var a = aggression += Time.fixedDeltaTime;
        if (a >= 1)
        {
            CurrentState = Chasing;
        }
    }

    private void OnDrawGizmos()
    {
        if (agent == null) return;
        Gizmos.DrawLine(transform.position, agent.nextPosition);
    }
}

public enum EnemyAIState { IdleState, Discovering, Chasing, Lost, Attacking, Reset }
