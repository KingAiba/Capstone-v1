using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentParameters : ScriptableObject
{
    public float pathingCooldown = 3f;
    public float enemyRange = 2f;

    public float StoppingDistance = 7f;

    public float rotationSpeed = 4f;

    public float shootingRange = 8f;
    public float aimTime = 2f;
    public float firingTime = 2f;
    public float shootCooldown = 3f;
}
