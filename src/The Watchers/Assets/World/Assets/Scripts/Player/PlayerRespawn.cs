using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRespawn : MonoBehaviour
{
    public float DeadZoneY;
    private PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (player.transform.position.y <= DeadZoneY)
        {
            if (NavMesh.FindClosestEdge(player.transform.position, out NavMeshHit hit, NavMesh.AllAreas))
            {
                player?.Teleport(hit.position);
                Debug.Log("yep");
            }
            Debug.Log("Under");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(0, DeadZoneY, 0), new Vector3(100, 0, 100));
    }

}