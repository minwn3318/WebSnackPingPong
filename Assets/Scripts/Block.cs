using TMPro;
using UnityEngine;

public class Block : MonoBehaviour // 블럭 클래스다
{
    [SerializeField] private SpriteRenderer renderer; // 보여지는 랜더링이다
    [SerializeField] private Collider2D col; // 부딪혀질 때 판정하는 collider이다
    [SerializeField] private GameObject textingObj; // 블럭 게임 오브젝트이다
    [SerializeField] private int count = 0; // 블럭 카운트이다
    [SerializeField] private TMP_Text textCount; // 블럭에 보여질 카운트 숫자다

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        textingObj = transform.GetChild(0).gameObject;
        textCount = textingObj.GetComponent<TMP_Text>();
        textCount.text = count.ToString();
    }

    public void UnenableInteract() // 블럭을 안보이게 하는 함수다
    {
        renderer.enabled = false;
        col.enabled = false;
        textingObj.SetActive(false);
    }

    public void EnableInteract(int count) // 블럭을 보이게하는 함수다
    {
        renderer.enabled = true;
        col.enabled = true;
        textCount.text = count.ToString();
        textingObj.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision) // 부딪혔을 때 카운트를 낮추고 카운트 수를 수정한다 0이하가 되면 안보이게 한다
    {
        count--;
        textCount.text = count.ToString();
        if(count <= 0)
        {
            UnenableInteract();
        }
    }
}
