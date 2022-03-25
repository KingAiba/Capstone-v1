using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdollHandler : EnemyRagdollHandler
{
    protected override void Start()
    {
        ragdollRBs = GetComponentsInChildren<Rigidbody>();
        animator = GetComponentInParent<Animator>();
        entityManager = GetComponentInParent<EntityManager>();

        entityManager.OnEntityDeath += ActivateRagdoll;
        DeactivateRagdoll();
        AttachHitBoxComponent();
    }

    private void OnDestroy()
    {
        entityManager.OnEntityDeath -= ActivateRagdoll;
    }
}

