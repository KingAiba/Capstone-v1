using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdollHandler : MonoBehaviour
{
    Rigidbody[] ragdollRBs;
    Animator animator;
    EntityManager enemyManager;

    public bool isRagDollActivated = false;

    public void DeactivateRagdoll()
    {
        foreach(Rigidbody rb in ragdollRBs)
        {
            rb.isKinematic = true;
        }
        animator.enabled = true;
        isRagDollActivated = false;
    }

    public void ActivateRagdoll()
    {
        foreach (Rigidbody rb in ragdollRBs)
        {
            rb.isKinematic = false;
        }
        animator.enabled = false;
        isRagDollActivated = true;
    }

    public void AttachHitBoxComponent()
    {
        foreach(Rigidbody rb in ragdollRBs)
        {
            HitBox hitbox = rb.gameObject.AddComponent<HitBox>();
            hitbox.entityManager = enemyManager;
        }
    }

    private void Start()
    {
        ragdollRBs = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        enemyManager = GetComponent<EntityManager>();

        enemyManager.OnEntityDeath += ActivateRagdoll;
        
        DeactivateRagdoll();
        AttachHitBoxComponent();
    }
}
