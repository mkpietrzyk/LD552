using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;
    public float speed = 5.0f;
    public int health = 10;
    public int damageValue = 5;
    public List<Sprite> sprites;
    public AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        int enemyDifficulty = Random.Range(1, 10);
        
        switch (enemyDifficulty)
        {
            case < 5:
                speed = 5f;
                health = 10;
                damageValue = 5;
                renderer.sprite = sprites[0];
                break;
            case >= 5 and < 9:
                speed = 4f;
                health = 20;
                damageValue = 15;
                transform.localScale = new Vector2(2, 2);
                renderer.sprite = sprites[1];
                break;
            case >= 9:
                speed = 3f;
                health = 30;
                damageValue = 25;
                transform.localScale = new Vector2(4, 4);
                renderer.sprite = sprites[2];
                break;
        }
        
        Vector2 playerPosition = player.transform.position;
        int newX = Random.Range(20, 25) * (Random.Range(0, 2) * 2 - 1);
        int newY = Random.Range(20, 25) * (Random.Range(0, 2) * 2 - 1);
        
        Vector2 spawnPosition = new Vector2(playerPosition.x + newX, playerPosition.y + newY);
        transform.position = spawnPosition;
    }

    private void Update()
    {
        if (player)
        {
            if (health > 0)
            {
                Vector2 playerPosition = player.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, (speed * Time.deltaTime));
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (health > 0)
        {
            if (health - damageAmount <= 0)
            {
                health = 0;
                Die();
            }
            else
            {
                health -= damageAmount;
            }
        }
    }

    void Die()
    {
        gameManager.enemiesCount--;
        gameManager.enemiesBodyCount++;
        Destroy(gameObject);
    }
}