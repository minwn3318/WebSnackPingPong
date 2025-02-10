using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Collider2D col;
    [SerializeField] private GameObject textingObj;

    [SerializeField] private int count = 0;
    [SerializeField] private TMP_Text textCount;
    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        textingObj = transform.GetChild(0).gameObject;
        textCount = textingObj.GetComponent<TMP_Text>();
        textCount.text = count.ToString();
    }

    public void UnenableInteract()
    {
        renderer.enabled = false;
        col.enabled = false;
        textingObj.SetActive(false);
    }

    public void EnableInteract(int count)
    {
        renderer.enabled = true;
        col.enabled = true;
        textCount.text = count.ToString();
        textingObj.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        count--;
        textCount.text = count.ToString();
        if(count <= 0)
        {
            UnenableInteract();
        }
    }
}
