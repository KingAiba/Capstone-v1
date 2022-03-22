using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public GameObject target;

    public bool canPath = true;

    public float pathingCooldown = 3f;
    public float enemyRange = 2f;


    public void SetDestinationToTarget()
    {
        if(canPath)
        {
            float dist = (target.transform.position - enemyAgent.destination).sqrMagnitude;
            if(dist > enemyRange * enemyRange)
            {
                enemyAgent.SetDestination(target.transform.position);
            }
            StartCoroutine(PathingCooldDownTimer());
        }
    }
    IEnumerator PathingCooldDownTimer()
    {
        canPath = false;
        yield return new WaitForSeconds(pathingCooldown);
        canPath = true;
    }
    void Start()
    {
        target = GameObject.Find("Player");
    }

    void Update()
    {
        SetDestinationToTarget();
    }




}
