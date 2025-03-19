using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public virtual void Awake()
    {
        // LOAD DATA 
    }
    private Transform target;

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target == null)
            return;

        this.transform.position = Vector3.Lerp(this.transform.position, target.position, 0.4f);

        if (Vector3.Distance(target.position, this.transform.position) < 0.4f)
        {
            ItemEffect(target.gameObject);
            Destroy(this.gameObject);
        }
    }

    protected abstract void ItemEffect(GameObject target);
}