﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private Player playerScript;

    private Vector2 targetPosition;
    public float speed;
    public int damage;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        targetPosition = playerScript.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }    
        else
        {
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);

        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);

        }
    }
}
