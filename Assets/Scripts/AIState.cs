using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateID
{
    ChasePlayer,
    RangeAttack,
    MeleeAttack,
    Idle,
    Die,
}
public interface AIState
{
    AIStateID GetID();
    void Enter(EnemyController agent);
    void Update(EnemyController agent);
    void Exit(EnemyController agent);

}

