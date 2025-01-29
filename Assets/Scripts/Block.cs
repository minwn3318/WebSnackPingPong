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
        UnenableInteract();
    }

    public void UnenableInteract()
    {
        renderer.enabled = false;
        col.enabled = false;
    }

    public void EnableInteract()
    {
        renderer.enabled = true;
        col.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        count--;
        if(count <= 0)
        {
            UnenableInteract();
        }
    }
}
