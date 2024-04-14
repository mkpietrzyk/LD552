using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    public AudioSource audioSource;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Cast());   
    }

    IEnumerator Cast()
    {
        Vector2 playerPosition = player.transform.position;
        int newX = Random.Range(5, 10) * Random.Range(-1, 1);
        int newY = Random.Range(5, 10) * Random.Range(-1, 1);
        Vector2 newPosition = new Vector2(playerPosition.x + newX, playerPosition.y + newY);
        player.transform.position = newPosition;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
