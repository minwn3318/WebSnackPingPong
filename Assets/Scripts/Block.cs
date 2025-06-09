using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D col;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private int count = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        countText = GetComponentInChildren<TMP_Text>();
        UpdateText();
    }

    public void SetCount(int value)
    {
        count = value;
        UpdateText();
    }

    public void EnableInteract()
    {
        spriteRenderer.enabled = true;
        col.enabled = true;
        countText.gameObject.SetActive(true);
        UpdateText();
    }

    public void DisableInteract()
    {
        spriteRenderer.enabled = false;
        col.enabled = false;
        countText.gameObject.SetActive(false);
    }

    private void UpdateText()
    {
        if (countText != null)
        {
            countText.text = count.ToString();
            countText.alignment = TextAlignmentOptions.Center;
            countText.fontSize = 5;
            countText.color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            AudioManager.Instance.PlayblockCountDownClip();
            count--;
            UpdateText();

            if (count <= 0)
            {
                AudioManager.Instance.PlayblockBreakClip();
                DisableInteract();
            }
        }
    }
}
