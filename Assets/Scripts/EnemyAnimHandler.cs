using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAnimHandler : MonoBehaviour
{
    public EnemyController enemyController;
    public Animator enemyAnimator;

    public void PlayAgentMovement(float mag)
    {
        enemyAnimator.SetFloat("Speed", mag);
    }

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void LateUpdate()
    {
        PlayAgentMovement(enemyController.enemyAgent.velocity.magnitude);
    }
}
