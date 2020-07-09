using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;

    public float speed;
    [HideInInspector]
    public Transform player;

    public float timeBetweenAttacks;
    public int damage;

    public int pickupChance;
    public GameObject[] pickups;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <=0 )
        {
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
