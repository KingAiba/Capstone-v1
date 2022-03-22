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

    public Bullet(Vector3 InitialPosition, Vector3 InitialVelocity, TrailRenderer bulletTracer)
    {
        initialPosition = InitialPosition;
        initialVelocity = InitialVelocity;

        tracer = bulletTracer;
    }
    public Vector3 CalculatePosition(float BulletSpeed, float BulletDrop)
    {
        Vector3 bulletG = Physics.gravity * BulletDrop;
        return initialPosition + (initialVelocity * time) + (0.5f * bulletG * time * time);
    }

    public void UpdateBullet(float deltaTime)
    {
        time += deltaTime;
    }


}
