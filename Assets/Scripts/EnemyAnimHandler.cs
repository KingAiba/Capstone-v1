using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAnimHandler : MonoBehaviour
{
    //public EnemyController enemyController;
    public Animator enemyAnimator;

    public void PlayAgentMovement(float mag)
    {
        enemyAnimator.SetFloat("Speed", mag);
    }

    public void ChangeAimAnim(bool isAiming)
    {
        enemyAnimator.SetBool("Aiming", isAiming);
    }

    public void PlayFireAnim()
    {
        enemyAnimator.SetTrigger("Attack");
    }

    public void PlayDamageAnim(float percent)
    {
        enemyAnimator.SetTrigger("Damage");
    }

    private void Start()
    {
        //enemyController = GetComponent<EnemyController>();
    }

    private void LateUpdate()
    {
        //PlayAgentMovement(enemyController.enemyNavAgent.velocity.magnitude);
    }
}
