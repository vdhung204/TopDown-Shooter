using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public static BgController instance { private set; get; }
    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public Vector3 GetSizeBg()
    {
        if (spriteRenderer != null)
        {
            return spriteRenderer.bounds.size;
        }
        return Vector3.zero;
    }
}
