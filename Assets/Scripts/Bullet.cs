using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet
{ 
    public float time;
    public Vector3 initialPosition;
    public Vector3 initialVelocity;

    public TrailRenderer tracer;
    /// <summary>
    /// Initialize bullet
    /// </summary>
    /// <param name="InitialPosition"></param>
    /// <param name="InitialVelocity"></param>
    /// <param name="bulletTracer"></param>
    public Bullet(Vector3 InitialPosition, Vector3 InitialVelocity, TrailRenderer bulletTracer)
    {
        initialPosition = InitialPosition;
        initialVelocity = InitialVelocity;

        tracer = bulletTracer;
    }
    /// <summary>
    /// calculate postion of bullet at given time using projectile motion
    /// </summary>
    /// <param name="BulletSpeed"></param>
    /// <param name="BulletDrop"></param>
    /// <returns>calculated postion</returns>
    public Vector3 CalculatePosition(float BulletSpeed, float BulletDrop)
    {
        Vector3 bulletG = Physics.gravity * BulletDrop;
        return initialPosition + (initialVelocity * time) + (0.5f * bulletG * time * time);
    }
    /// <summary>
    /// update bullet timer
    /// </summary>
    /// <param name="deltaTime"></param>
    public void UpdateBullet(float deltaTime)
    {
        time += deltaTime;
    }


}
