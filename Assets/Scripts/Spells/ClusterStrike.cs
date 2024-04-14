using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClusterStrike : MonoBehaviour
{
    public int projectileAmount = 25;
    public int counter = 0;
    public GameObject mine;
    
    void Start()
    {
        StartCoroutine(BeginClusterStrike());
    }

    private void Update()
    {
        if (counter >= projectileAmount)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BeginClusterStrike()
    {
        while (counter < projectileAmount)
        {
            var instance = Instantiate(mine, transform);
            float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
            Vector2 forceDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            instance.GetComponent<Rigidbody2D>().AddForce(forceDirection * 10f, ForceMode2D.Impulse);
            instance.GetComponent<Rigidbody2D>().AddTorque(10f, ForceMode2D.Impulse);
            counter++;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
