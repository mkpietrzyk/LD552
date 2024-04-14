using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nova : MonoBehaviour
{
    public int damage = 25;
    public float expansionDuration = 0.5f;
    public Vector2 targetScale = new Vector2(12.0f, 12.0f);
    private Vector2 originalScale; // To store the original scale
    private float timer;
    public AudioSource audioSource;
    public AudioClip playSound;
    void Start()
    {
        originalScale = transform.localScale;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(playSound);
        StartCoroutine(ExpandCircle());
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

    IEnumerator ExpandCircle()
    {
        timer = 0f;

        while (timer < expansionDuration)
        {
            transform.localScale = Vector2.Lerp(originalScale, targetScale, timer / expansionDuration);
            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.localScale = targetScale; // Ensure it ends exactly at the target scale
        Destroy(gameObject);
    }
}
