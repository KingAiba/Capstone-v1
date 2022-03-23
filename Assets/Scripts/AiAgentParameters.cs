using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentParameters : ScriptableObject
{
    public float pathingCooldown = 3f;
    public float enemyRange = 2f;

    public float StoppingDistance = 5f;
    //public float maxDistance = 3f;

    public float rotationSpeed = 4f;
}
