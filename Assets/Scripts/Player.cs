using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveAmount;

    private Animator anim;

    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    private Animator camAnim;

    private SceneTransition sceneTransition;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        camAnim = Camera.main.GetComponent<Animator>();
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }
    public void TakeDamage(int damageAmount)
    {
        camAnim.SetTrigger("shake");
        health -= damageAmount;
        UpdateHeatlhUI(health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
            sceneTransition.LoadScene("Lose");
        }
    }
     public void Healing(int HealAmmount)
    {
        if (health < hearts.Length)
        {
            health += HealAmmount;
        }
        else
        {
            health = hearts.Length;
        }
        UpdateHeatlhUI(health);
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    public void UpdateHeatlhUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
