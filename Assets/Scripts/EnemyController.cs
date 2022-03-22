using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : EntityManager
{
    public NavMeshAgent enemyAgent;
    public GameObject target;

    public bool canPath = true;

    public float pathingCooldown = 3f;
    public float enemyRange = 2f;

    public float blinkIntensity;
    public float blinkDuration;
    public float blinkTimer = 0f;

    SkinnedMeshRenderer skinnedMeshRenderer;

    public delegate void OnDamageDelegate(float percent);
    public OnDamageDelegate OnDamage;
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

    public override void TakeDamage(float val, Vector3 dir)
    {
        base.TakeDamage(val, dir);
        OnDamage?.Invoke(GetPercentHP());
        //blinkTimer = blinkDuration;
    }

    public override void Die()
    {
        base.Die();
        enemyAgent.isStopped = true;
    }

    public void StartNavMeshAgent()
    {
        enemyAgent.isStopped = false;
    }

    private void BlinkOnDamage()
    {
        if(blinkTimer >= 0)
        {
            blinkTimer -= Time.deltaTime;
            float val = Mathf.Clamp01(blinkTimer / blinkDuration);
            float intensity = (val * blinkIntensity) + 1f;
            skinnedMeshRenderer.material.color = Color.white * intensity;
        }
    }

    private float GetPercentHP()
    {
        return currHealth / maxHealth;
    }
    protected override void Start()
    {
        base.Start();
        target = GameObject.Find("Player");
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        SetDestinationToTarget();
        //BlinkOnDamage();
    }




}
