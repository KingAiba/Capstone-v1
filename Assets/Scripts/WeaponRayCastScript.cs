using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public enum WeaponType
{
    Primary,
    Secondary,
}

public class WeaponRayCastScript : MonoBehaviour
{
    public float bulletDamage = 5f;
    public float fireRate = 25;
    public float bulletSpeed = 100f;
    public float bulletDrop = 0.0f;

    public float bulletTTL = 3.0f;

    public bool isFiring = false;
    bool canFire = true;

    public Transform muzzle;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hiteffect;

    protected Ray ray;
    protected RaycastHit hitInfo;
    public Transform rayCastDestination;

    public TrailRenderer tracerEffect;

    public List<Bullet> bulletLst = new List<Bullet>();

    public Transform rightHandGripIK;
    public Transform leftHandGripIK;

    public WeaponType weaponType = WeaponType.Primary;

    public LayerMask rayCastLayer;

    public delegate void OnWeaponShootDelegate();
    public OnWeaponShootDelegate OnWeaponShoot;

    public Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet(position, velocity, Instantiate(tracerEffect, position, Quaternion.identity));
        bullet.tracer.AddPosition(ray.origin);
        bullet.time = 0.0f;

        return bullet;
    }

    public void StartFiring()
    {
        isFiring = true;
        //fireTime = 0f;
        //FireBullet();
    }

    public void UpdateFiring(float deltatime)
    {
        if(canFire)
        {
            FireBullet();
            OnWeaponShoot?.Invoke();
            StartCoroutine(FireCooldownRoutine());
        }
/*        fireTime += deltatime;
        float fireInterval = 1.0f / fireRate;
        while(fireTime >= 0.0f)
        {
            FireBullet();
            fireTime -= fireInterval;
            Debug.Log(fireTime);
        }*/
    }

    public virtual void UpdateBullets()
    {
        foreach(Bullet bullet in bulletLst)
        {
            Vector3 p0 = bullet.CalculatePosition(bulletSpeed, bulletDrop);
            bullet.UpdateBullet(Time.deltaTime);
            Vector3 p1 = bullet.CalculatePosition(bulletSpeed, bulletDrop);

            FireRayCastSegment(p0, p1, bullet);
        }

        DestroyBullets();
    }

    public void DestroyBullets()
    {
        bulletLst.RemoveAll(bullet => bullet.time >= bulletTTL);
    }

    protected virtual void FireRayCastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 dir = end - start;

        ray.origin = start;
        ray.direction = dir;

        float rayDistance = (dir).magnitude;

        if (Physics.Raycast(ray, out hitInfo, rayDistance, rayCastLayer))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
            hiteffect.transform.position = hitInfo.point;
            hiteffect.transform.forward = hitInfo.normal;
            hiteffect.Emit(1);
            //Debug.Log(hiteffect.transform.position);
            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = bulletTTL;
            OnEnemyHitProcedure(hitInfo.collider);
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }

    protected virtual void FireBullet()
    {
        muzzleFlash.Emit(1);
        bulletLst.Add(CreateBullet(muzzle.position, (rayCastDestination.position - muzzle.position).normalized * bulletSpeed));
/*        ray.origin = muzzle.position;
        ray.direction = rayCastDestination.position - muzzle.position;

        TrailRenderer tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
            hiteffect.transform.position = hitInfo.point;
            hiteffect.transform.forward = hitInfo.normal;
            hiteffect.Emit(1);
            //Debug.Log(hiteffect.transform.position);
            tracer.transform.position = hitInfo.point;
        }*/
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    IEnumerator FireCooldownRoutine()
    {
        canFire = false;
        yield return new WaitForSeconds(1/fireRate);
        canFire = true;
    }
    public virtual void OnEnemyHitProcedure(Collider hitCollider)
    {

    }
}
