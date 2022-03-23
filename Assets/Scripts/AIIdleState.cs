using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    public void Enter(EnemyController agent)
    {

    }

    public void Exit(EnemyController agent)
    {

    }

    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }

    public void Update(EnemyController agent)
    {
        float dist = Vector3.Distance(agent.playerTarget.transform.position, agent.transform.position);
        if(dist <= agent.aiAgentParam.StoppingDistance)
        {
            Vector3 dir = agent.playerTarget.transform.position - agent.transform.position;
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(dir), agent.aiAgentParam.rotationSpeed);

            if (dist <= agent.aiAgentParam.shootingRange)
            {
                agent.stateMachine.ChangeState(AIStateID.RangeAttack);
            }
        }
        else
        {
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
        }
    }
}
