using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : EntityManager
{
    public NavMeshAgent enemyNavAgent;
    public GameObject playerTarget;

    /*    public float blinkIntensity;
        public float blinkDuration;
        public float blinkTimer = 0f;
        SkinnedMeshRenderer skinnedMeshRenderer;*/

    public EnemyAnimHandler enemyAnimHandler;

    public AIWeapon agentWeapon;

    public Transform lookAtTarget;
    public float lookAtSpeed = 2f;

    public AIStateMachine stateMachine;
    public AIStateID initialState;
    public AIStateID currState;
    public AiAgentParameters aiAgentParam;

    public delegate void OnDamageDelegate(float percent = 0);
    public OnDamageDelegate OnDamage;


    public override void TakeDamage(float val, Vector3 dir)
    {
        base.TakeDamage(val, dir);
        OnDamage?.Invoke(GetPercentHP());
        //blinkTimer = blinkDuration;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(AIStateID.Die);
    }

    public void StartNavMeshAgent()
    {
        enemyNavAgent.isStopped = false;
    }

/*    private void BlinkOnDamage()
    {
        if(blinkTimer >= 0)
        {
            blinkTimer -= Time.deltaTime;
            float val = Mathf.Clamp01(blinkTimer / blinkDuration);
            float intensity = (val * blinkIntensity) + 1f;
            skinnedMeshRenderer.material.color = Color.white * intensity;
        }
    }*/

    private float GetPercentHP()
    {
        return currHealth / maxHealth;
    }

    IEnumerator EnemyCooldownRoutine(float cdTime, bool givenBool)
    {
        yield return new WaitForSeconds(cdTime);

    }
    protected override void Start()
    {
        base.Start();
        playerTarget = GameObject.FindGameObjectWithTag("Player");

        enemyAnimHandler = GetComponent<EnemyAnimHandler>();
        OnDamage += enemyAnimHandler.PlayDamageAnim;
        //OnEntityDeath += enemyAnimHandler.PlayDamageAnim;
        //skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        stateMachine = new AIStateMachine(this);
        stateMachine.AddState(new AIChasePlayerState());
        stateMachine.AddState(new AIDeathState());
        stateMachine.AddState(new AIIdleState());
        stateMachine.AddState(new AIRangedAttackState());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();
    }

    private void LateUpdate()
    {
        agentWeapon.UpdateBullets();
        enemyAnimHandler.PlayAgentMovement(enemyNavAgent.velocity.magnitude);
        lookAtTarget.position = Vector3.Lerp(lookAtTarget.position, playerTarget.transform.position + new Vector3(0, 0.5f, 0), lookAtSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        OnDamage -= enemyAnimHandler.PlayDamageAnim;
    }




}
