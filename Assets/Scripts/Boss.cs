using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public int health;
    public Enemy[] enemies;
    public float spawnOffset;
    public int damage;

    private int halfHealth;
    private Animator anim;

    public GameObject deathEffect;
    public GameObject blood;

    private Animator camaAnim;

    private Slider healthBar;

    private SceneTransition sceneTransition;


    public void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        camaAnim = Camera.main.GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Instantiate(blood, transform.position, transform.rotation);
            healthBar.gameObject.SetActive(false);
            Destroy(this.gameObject);
            sceneTransition.LoadScene("Win");
        }

        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0) , transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void ShakeCamara()
    {
        camaAnim.Play("BigShake");
    }

}
