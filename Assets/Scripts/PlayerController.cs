using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public int mana = 100;
    public int spellsCasted = 0;
    public int manaRestore = 30;
    public int healthRestore = 25;
    public float moveSpeed = 5.0f; // Movement speed of the player
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    public List<GameObject> spells = new List<GameObject>();
    public AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the GameObject
    }

    private void Update()
    {
        if (health > 0)
        {
            MovePlayer();
            if (mana > 1 && Input.GetButtonDown("Fire1"))
            {
                CastSpell();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            TakeDamage(enemy.damageValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ManaPickup"))
        {
            RestoreMana(manaRestore);
        }
        
        if (other.CompareTag("HealthPickup"))
        {
            Heal(healthRestore);
        }
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal"); // Get horizontal input
        float moveY = Input.GetAxis("Vertical"); // Get vertical input

        Vector2 moveVector = new Vector2(moveX, moveY); // Create a movement vector
        rb.velocity = moveVector * moveSpeed; // Apply the movement vector to the Rigidbody2D
    }

    void CastSpell()
    {
        int randomIndex = Random.Range(0, spells.Count);
        GameObject spell = spells[randomIndex];
        Instantiate(spell, transform);
        spellsCasted++;
    }

    void TakeDamage(int damageAmount)
    {
        if (health > 0)
        {
            if (health - damageAmount < 0)
            {
                health = 0;
            }
            else
            {
                audioSource.pitch = Random.Range(0.5f, 0.9f);
                audioSource.Play();
                health -= damageAmount;
            }
        }
    }

    void Heal(int healthAmount)
    {
        if (health > 0)
        {
            if (health + healthAmount > 100)
            {
                health = 100;
            }
            else
            {
                health += healthAmount;
            }
        }
    }

    void RestoreMana(int manaAmount)
    {
        if (mana + manaAmount > 100)
        {
            mana = 101;
        }
        else
        {
            mana += manaAmount;
        }
    }
}