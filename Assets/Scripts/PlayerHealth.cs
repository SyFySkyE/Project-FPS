using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] Canvas damageFX;
    [SerializeField] float secondsBetweenDamageFXFlicker = 0.2f;

    public void OnAttackPlayer(int damage)
    {
        StartCoroutine(EnableAndDisableDamageFX());
        health -= damage;
        if (health <= 0)
        {            
            print("Players's health is 0.");
            GetComponent<DeathHandler>().HandleDeath();
        }
    }

    private IEnumerator EnableAndDisableDamageFX()
    {
        damageFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(secondsBetweenDamageFXFlicker);
        damageFX.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        damageFX.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
