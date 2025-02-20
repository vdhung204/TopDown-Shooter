using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    public float speed;
    public void MoveTo(Vector3 diriection)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + diriection, speed * Time.deltaTime);
    }
}
