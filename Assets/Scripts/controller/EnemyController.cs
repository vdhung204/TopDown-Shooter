using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Figure
{
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
    }
}
