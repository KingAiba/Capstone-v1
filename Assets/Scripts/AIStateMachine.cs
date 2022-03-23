using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    public AIState[] states;
    public EnemyController agent;

    public AIStateID currState;

    public AIStateMachine(EnemyController Agent)
    {
        agent = Agent;
        int numStates = System.Enum.GetNames(typeof(AIStateID)).Length;
        states = new AIState[numStates];
    }

    public void AddState(AIState state)
    {
        states[(int)state.GetID()] = state;
    }

    public AIState GetState(AIStateID stateID)
    {
        return states[(int)stateID];
    }
    public void Update()
    {
        AIState curr = GetState(currState);
        curr.Update(agent);
    }

    public void ChangeState(AIStateID newState)
    {
        AIState prev = GetState(currState);
        prev.Exit(agent);

        currState = newState;
        AIState curr = GetState(currState);
        curr.Enter(agent);

        agent.currState = currState;
    }
}
