using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeapon : WeaponRayCastScript
{
    public float bulletDamage = 5f;
    public float bulletForce = 10f;

    public override void OnEnemyHitProcedure(Collider hitCollider)
    {
        base.OnEnemyHitProcedure(hitCollider);
        if(hitCollider.CompareTag("Player"))
        {
            EntityManager player = hitCollider.GetComponent<EntityManager>();
            player.TakeDamage(bulletDamage, this.ray.direction);
        }
    }
}
