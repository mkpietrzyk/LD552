using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class Mine : MonoBehaviour
{
    public int damage = 10;
    public AudioSource audioSource;
    public AudioClip playSound;
    void Start()
    {
        audioSource.pitch = Random.Range(1f, 1.5f);
        audioSource.PlayOneShot(playSound);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.pitch = Random.Range(1f, 1.5f);
            audioSource.Play();
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
        }
    }
}
