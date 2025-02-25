using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class WeaponController : MonoBehaviour
{
    public float attackRange = 10f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public GameObject fireEfect;
    public float nextFireTime = 0f;
    public Transform shootPos;
    public Transform fireEPos;
    private void Start()
    {
        
    }
    private void Update()
    {
        //Fire();
        AutoAimAndShoot();
    }
    public Transform GetNeareatEnemy()
    {
        if(GameManager.instance.enemiesPos.Count == 0)
        {
            return null;
        }
        GameManager.instance.enemiesPos.Sort((x, y) 
            => Vector3.Distance(transform.position, x.position)
            .CompareTo(Vector3.Distance(transform.position, y.position)));
        return GameManager.instance.enemiesPos[0];
    }
    void TurnTheGunTowardEnemies(Vector3 enemyPosition)
    {
        Vector3 lookDir = new Vector3( enemyPosition.x-transform.position.x ,  enemyPosition.y-transform.position.y);
        transform.right = lookDir;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270) 
            transform.localScale = new Vector3(1, -1, 0);
        else 
            transform.localScale = new Vector3(1, 1, 0);
    }

    public bool IsInRange(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) <= attackRange;
    }
    public void Fire()
    {
        var effectClone = SmartPool.Instance.Spawn(fireEfect, fireEPos.position, Quaternion.identity);
        effectClone.transform.right = transform.right;

        var bullet = SmartPool.Instance.Spawn(bulletPrefab, shootPos.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetDirection(GetNeareatEnemy().position - transform.position);
    }
    public void AutoAimAndShoot()
    {
        Transform nearEnemy = GetNeareatEnemy();
        if (nearEnemy == null) return;
        if (IsInRange(nearEnemy))

        {
            
            TurnTheGunTowardEnemies(nearEnemy.position);
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate; 
            }
        }
    }
}
