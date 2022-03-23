using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRangedAttackState : AIState
{
    public Transform target;
    public bool canFire = false;

    public float currAimTime = 0;
    public float currFiringTime = 0;

    public AIStateID GetID()
    {
        return AIStateID.RangeAttack;
    }
    public void Enter(EnemyController agent)
    {
        target = agent.lookAtTarget;
        agent.enemyAnimHandler.ChangeAimAnim(true);
    }

    public void Exit(EnemyController agent)
    {
        agent.enemyAnimHandler.ChangeAimAnim(false);
    }

    public void Update(EnemyController agent)
    {
        Vector3 dir = agent.playerTarget.transform.position - agent.transform.position;
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(dir), agent.aiAgentParam.rotationSpeed);

        if(canFire)
        {
            if(currFiringTime < agent.aiAgentParam.firingTime)
            {
                agent.agentWeapon.StartFiring();
                agent.agentWeapon.UpdateFiring(Time.deltaTime);
                agent.enemyAnimHandler.PlayFireAnim();
                currFiringTime += Time.deltaTime;
            }
            else
            {
                agent.agentWeapon.StopFiring();
                currFiringTime = 0;
                canFire = false;
                agent.stateMachine.ChangeState(AIStateID.Idle);
            }
          
            return;
        }

        if(currAimTime < agent.aiAgentParam.aimTime)
        {
            currAimTime += Time.deltaTime;
        }
        else
        {
            canFire = true;
            currAimTime = 0;
        }
    }
}

