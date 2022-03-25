using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdollHandler : MonoBehaviour
{
    protected Rigidbody[] ragdollRBs;
    protected Animator animator;
    protected EntityManager entityManager;

    public bool isRagDollActivated = false;

    public virtual void DeactivateRagdoll()
    {
        foreach(Rigidbody rb in ragdollRBs)
        {
            rb.isKinematic = true;
        }
        animator.enabled = true;
        isRagDollActivated = false;
    }

    public virtual void ActivateRagdoll()
    {
        foreach (Rigidbody rb in ragdollRBs)
        {
            rb.isKinematic = false;
        }
        animator.enabled = false;
        isRagDollActivated = true;
    }

    public virtual void AttachHitBoxComponent()
    {
        foreach(Rigidbody rb in ragdollRBs)
        {
            HitBox hitbox = rb.gameObject.AddComponent<HitBox>();
            hitbox.entityManager = entityManager;
        }
    }

    protected virtual void Start()
    {
        ragdollRBs = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        entityManager = GetComponent<EntityManager>();

        entityManager.OnEntityDeath += ActivateRagdoll;
        
        DeactivateRagdoll();
        AttachHitBoxComponent();
    }
}
