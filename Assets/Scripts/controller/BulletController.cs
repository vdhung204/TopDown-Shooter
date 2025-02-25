using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MoveBase
{
    public Vector3 direction;
    public float damage;
    public float lifeTime;
    private float TIMELIFE = 3f;

    private void Start()
    {
        damage = PlayerController.instance.damage;
        lifeTime = TIMELIFE;
    }

    private void Update()
    {
        MoveTo(direction);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DestroyMe();
            lifeTime = TIMELIFE;
        }
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
    public void DestroyMe()
    {
        SmartPool.Instance.Despawn(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage);
            DestroyMe();
        }
    }
}
