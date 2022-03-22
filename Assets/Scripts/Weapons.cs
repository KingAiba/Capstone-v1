using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : WeaponRayCastScript
{
    public float bulletDamage = 5f;
    public float bulletForce = 10f;

    public override void OnEnemyHitProcedure(Collider hitCollider)
    {
        base.OnEnemyHitProcedure(hitCollider);
        Rigidbody hitRB = hitCollider.GetComponent<Rigidbody>();
        HitBox hitbox = hitCollider.GetComponent<HitBox>();

        if(hitRB != null)
        {
            hitRB.AddForceAtPosition(this.ray.direction * bulletForce, this.hitInfo.point, ForceMode.Impulse);
        }

        if(hitbox != null)
        {
            hitbox.OnRayCastHit(this, this.ray.direction);
        }
    }
}
