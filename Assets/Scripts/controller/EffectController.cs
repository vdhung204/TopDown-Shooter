using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    private float time;
    public float TIMELIFE = 0.2f;
    private void Start()
    {
        time = TIMELIFE;
    }
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            SmartPool.Instance.Despawn(gameObject);
            time = TIMELIFE;
        }
    }
}
