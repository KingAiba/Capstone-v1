using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AIChasePlayerState : AIState
{
    public GameObject target;
    public bool canPath = true;

    
    public void SetDestinationToTarget(EnemyController agent)
    {
        if (canPath)
        {
            float dist = (target.transform.position - agent.enemyNavAgent.destination).sqrMagnitude;
            if (dist > agent.aiAgentParam.enemyRange * agent.aiAgentParam.enemyRange)
            {
                agent.enemyNavAgent.SetDestination(target.transform.position);
            }
            agent.StartCoroutine(PathingCooldDownTimer(agent));
        }
    }
    IEnumerator PathingCooldDownTimer(EnemyController agent)
    {
        canPath = false;
        yield return new WaitForSeconds(agent.aiAgentParam.pathingCooldown);
        canPath = true;
    }

    public void Enter(EnemyController agent)
    {
        target = agent.playerTarget;
        agent.enemyNavAgent.stoppingDistance = agent.aiAgentParam.StoppingDistance;
    }

    public void Exit(EnemyController agent)
    {

    }

    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }

    public void Update(EnemyController agent)
    {

        if (agent.enemyNavAgent.remainingDistance <= agent.enemyNavAgent.stoppingDistance)
        {
            agent.stateMachine.ChangeState(AIStateID.Idle);
        }
        SetDestinationToTarget(agent);

    }
}
