using System.Collections;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [Header("Prefabs")]
    public GameObject blockPrefab;
    public GameObject diamondPrefab;

    [Header("Stage Settings")]
    public int width = 7;
    public int height = 11;
    public float blockSize = 2f;

    public Transform playerTransform;
    public bool isStageMoving = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 현재 스테이지 제거
    public void ClearCurrentStage()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private GameObject currentStage;

    // 새로운 스테이지 생성
    public GameObject GenerateStage(bool isFirstStage = true)
    {
        if (currentStage != null)
        {
            Destroy(currentStage);
            currentStage = null;
        }

        GameObject stageGroup = new GameObject("StageGroup");
        stageGroup.transform.parent = this.transform;

        // 아래로 이동시킬 오프셋
        float yOffset = -1.0f;

        float startY = (isFirstStage ? -blockSize * height / 2f : -blockSize * height) + yOffset;
        stageGroup.transform.position = new Vector3(0, startY, 0);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject block = Instantiate(blockPrefab, Vector3.zero, Quaternion.identity, stageGroup.transform);
                block.name = $"Block({x},{y})";
                block.transform.localPosition = new Vector3((x - width / 2) * blockSize, y * blockSize, 0);

                Block blockScript = block.GetComponent<Block>();
                blockScript.SetCount(Random.Range(1, 17));
                blockScript.EnableInteract();
            }
        }

        // 다이아몬드 생성
        int randomX = Random.Range(0, width);
        float diamondX = (randomX - width / 2) * blockSize;
        float diamondY = 0.8f;

        GameObject diamond = Instantiate(diamondPrefab, Vector3.zero, Quaternion.identity, stageGroup.transform);
        diamond.transform.localPosition = new Vector3(diamondX, diamondY, 0);
        diamond.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        diamond.name = "Diamond";

        currentStage = stageGroup; // 현재 스테이지 저장
        return stageGroup;
    }

    // 스테이지를 위로 이동시키는 애니메이션
    //public IEnumerator MoveStageUp(GameObject stageGroup)
    //{
    //    isStageMoving = true;

    //    float moveTime = 1.0f;
    //    float elapsedTime = 0f;

    //    Vector3 startPos = stageGroup.transform.position;
    //    Vector3 endPos = new Vector3(startPos.x, -blockSize * height / 2f, startPos.z);

    //    while (elapsedTime < moveTime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float t = elapsedTime / moveTime;
    //        stageGroup.transform.position = Vector3.Lerp(startPos, endPos, t);
    //        yield return null;
    //    }

    //    stageGroup.transform.position = endPos;
    //    isStageMoving = false;
    //}
}
