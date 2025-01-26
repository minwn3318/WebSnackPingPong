using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Collider2D col;
    [SerializeField] private int count = 20;
    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        count--;
        Debug.Log(count);
        if(count == 0)
        {
            renderer.enabled = false;
        }
    }
}
