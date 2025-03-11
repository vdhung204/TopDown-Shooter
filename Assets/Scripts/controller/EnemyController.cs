using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyController : Figure
{
    public Seeker seeker;
    public Animator anim;
    public float repeatTimeUpdatePath = 0.5f;
    Path path;
    public Transform target;
    Coroutine moveCoroutine;
    public Data_Infor data_Infor;
    public Enemies_Drop enemiesDrop;
    public int exp;

    private void Awake()
    {
        data_Infor = Resources.Load<Data_Infor>("CSV_Data/Data_Infor");
        enemiesDrop = Resources.Load<Enemies_Drop>("CSV_Data/Enemies_Drop");
    }
    private void Start()
    {
        GetPlayerPosition();
        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (hp <= 0)
        {
            this.PostEvent(EventID.EnemyDie, exp);
            this.PostEvent(EventID.RemoveEnemyTransform, gameObject.name);

            Destroy(gameObject);
        }
    }

    
    public override void ObjectDie()
    {
        base.ObjectDie();
       /* var x = Random.Range(0, 15);
        ItemFactory.Instance.Create(x, this.transform.position);
        SoundService.Instance.PlaySound(SoundType.sound_enemy_die);*/
    }
    public void InitInforEnemy(int level)
    {
        var temp = data_Infor.GetInforEnemiesByLevel(level);
        var temp2 = enemiesDrop.GetExpEnemiesByLevel(level);
        SetInforEnemy(temp);
        SetInforExp(temp2);
    }
    void GetPlayerPosition()
    {
        if (PlayerController.instance.transform == null)
            return;
        target = PlayerController.instance.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
    }

    void CalculatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            MovetoTarget();
        }
    }

    void MovetoTarget()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
            moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
        
        
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < 0.5f)
                currentWP++;

            if (force.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            anim.SetFloat("Move_Enemies", Mathf.Abs(direction.magnitude));

            yield return null;
        }
    }
    void SetInforEnemy(Data_object data_Object)
    {
        hp = data_Object.hp;
        damage = data_Object.damage;
        speed = data_Object.speed;
    }
    void SetInforExp(Drop drop)
    {
        exp = drop.expDrop;
    }
    
    
}
