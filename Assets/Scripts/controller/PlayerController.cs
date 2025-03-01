using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Figure
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public static PlayerController instance { private set; get; }

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        MovePlayer();
    }
    public Vector3 GetSizeBg()
    {

        if (spriteRenderer != null)
        {
             return spriteRenderer.bounds.size; 
        }
        return Vector2.zero;


    }
    public void MovePlayer()
    {
        var direction = Vector3.zero;
        float moveSpeed = 5f; 
        Vector3 nextPosition = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (direction != Vector3.zero)
        {
            nextPosition = transform.position + direction.normalized * moveSpeed * Time.deltaTime;

            var bgSize = GetSizeBg();
            float xLimit = bgSize.x / 2 - 0.3f;
            float yLimit = bgSize.y / 2 - 0.4f;

            nextPosition.x = Mathf.Clamp(nextPosition.x, -xLimit, xLimit);
            nextPosition.y = Mathf.Clamp(nextPosition.y, -yLimit, yLimit);

            transform.position = nextPosition;
        }

        anim.SetFloat("Move", Mathf.Abs(direction.magnitude));
    }

}
