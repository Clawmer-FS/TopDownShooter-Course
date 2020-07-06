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
            Destroy(gameObject);
        }
    }
}
