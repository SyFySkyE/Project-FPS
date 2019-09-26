using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    bool isDead = false;
   
    public void TakeDamage(float damage)
    {
        // GetComponent<EnemyAI>().OnDamageTaken();
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0 && isDead == false)
        {
            isDead = true;
            StartEnemyDeath();
        }
    }

    private void StartEnemyDeath()
    {
        GetComponent<Animator>().SetTrigger("dead");
        BroadcastMessage("OnDeath");
        GetComponent<CapsuleCollider>().enabled = false;            
    }
}
