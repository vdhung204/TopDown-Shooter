using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MoveBase
{
    public Animator ani;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        MovePlayer();
    }
    public void MovePlayer()
    {
        var direction = Vector3.zero;
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
        if(direction != Vector3.zero)
        {
            MoveTo(direction);
        }
        ani.SetFloat("Move", Mathf.Abs(direction.magnitude));
    }
}
