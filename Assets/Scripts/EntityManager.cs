using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;

    public bool isDead = false;



    public delegate void OnEntityDeathDelegate();
    public OnEntityDeathDelegate OnEntityDeath;



    public void InitializeObject()
    {
        isDead = false;
        currHealth = maxHealth;
    }

    public virtual void Die()
    {
        isDead = true;
        OnEntityDeath?.Invoke();
    }

    public virtual void TakeDamage(float val, Vector3 dir)
    {
        currHealth -= val;
        if(currHealth <= 0.0f)
        {
            currHealth = 0.0f;
            Die();
        }
    }

    protected virtual void Start()
    {
        InitializeObject();
    }
}
