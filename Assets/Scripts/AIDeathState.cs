using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{

    public AIStateID GetID()
    {
        return AIStateID.Die;
    }
    public void Enter(EnemyController agent)
    {
       agent.enemyNavAgent.isStopped = true;
    }

    public void Exit(EnemyController agent)
    {

    }

    public void Update(EnemyController agent)
    {

    }
}
