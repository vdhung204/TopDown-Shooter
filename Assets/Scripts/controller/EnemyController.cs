using Pathfinding;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        GetPlayerPosition();
        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath);
    }

    void GetPlayerPosition()
    {
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
}
