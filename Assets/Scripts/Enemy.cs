using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;

    public float speed;
    [HideInInspector]
    public Transform player;

    Player playerScript;

    public float timeBetweenAttacks;
    public int damage;

    public int pickupChance;
    public GameObject[] pickups;

    public int heatlPickupChance;
    public GameObject healthPickup;

    public bool drop = false;

    public GameObject deathEffect;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <=0)
        {
            if (playerScript.health < 3)
            {
                int randHealth = Random.Range(40, 101);
                if (randHealth < pickupChance)
                {
                    Instantiate(healthPickup, transform.position, transform.rotation);
                    drop = true;
                }
            }
            else
            {
                int randHealth = Random.Range(0, 101);
                if (randHealth < pickupChance)
                {
                    Instantiate(healthPickup, transform.position, transform.rotation);
                    drop = true;
                }
            }
            if (drop == false)
            { 
                int randomNumber = Random.Range(0, 101);
                if (randomNumber < pickupChance)
                {
                    GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                    Instantiate(randomPickup, transform.position, transform.rotation);
                    drop = true;
                }
            }
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
