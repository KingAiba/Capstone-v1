using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : WeaponRayCastScript
{
    public float bulletForce = 10f;

    public float reloadTime = 1f;
    public float currReloadTimer = 0f;

    public int currMagAmount = 10;
    public int maxMagSize = 10;

    public bool isReloading = false;



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

    protected override void FireBullet()
    {
        if(!isReloading)
        {
            currMagAmount--;
            //Debug.Log("HERE");
            base.FireBullet();

            if (currMagAmount <= 0)
            {
                Reload();
            }

        }

    }

    public virtual void Reload()
    {
        if(!isReloading)
        {
            isReloading = true;
            OnReload?.Invoke();
        }

    }
    public virtual void ReloadUpdate()
    {
        if(isReloading)
        {
            currReloadTimer += Time.deltaTime;
            if(currReloadTimer >= reloadTime)
            {
                isReloading = false;
                currReloadTimer = 0f;
                currMagAmount = maxMagSize;
            }
        }
    }

    public override void UpdateBullets()
    {
        base.UpdateBullets();
        ReloadUpdate();
    }

}
