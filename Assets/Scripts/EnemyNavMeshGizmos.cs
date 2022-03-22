using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyNavMeshGizmos : MonoBehaviour
{
    public bool velocity;
    public bool desiredVelocity;
    public bool path;

    public NavMeshAgent enemyAgent;
    void Start()
    {
        //enemyAgent = GetComponent<NavMeshAgent>();
    }


    private void OnDrawGizmos()
    {
        if (velocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + enemyAgent.velocity);
        }

        if (desiredVelocity)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + enemyAgent.desiredVelocity);
        }

        if (path)
        {
            Gizmos.color = Color.green;
            NavMeshPath agentPath = enemyAgent.path;
            Vector3 prevPos = transform.position;
            foreach (Vector3 corner in agentPath.corners)
            {
                Gizmos.DrawLine(prevPos, corner);
                Gizmos.DrawSphere(corner, 0.1f);
                prevPos = corner;
            }
        }
    }
}
