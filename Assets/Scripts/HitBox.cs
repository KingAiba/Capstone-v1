using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public EntityManager entityManager;

    public void OnRayCastHit(WeaponRayCastScript weapon, Vector3 dir)
    {
        entityManager.TakeDamage(weapon.bulletDamage, dir);
    }
}
