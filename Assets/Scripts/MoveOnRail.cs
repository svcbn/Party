using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveOnRail : MonoBehaviour
{
    public Route currentRoute;

    public float speed = 10f;

    public int steps = 0;

    public static MoveOnRail Instance;

    public int routePosition = 0;

    public bool canPass;
    public bool isMoving;

    public Text stepsUI;

    public int Steps
    {
        get
        {
            return steps;
        }
        set
        {
            steps = value;
            stepsUI.text = steps + "";
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            StartCoroutine(RollDice());

        }
    }

    IEnumerator RollDice()
    {
        yield return null;
        Steps = Random.Range(1, 11);
        //Steps = 100;                      // 테스트주사위!!!!!!!!!!!!!
        
        Debug.Log("주사위 숫자 : " + Steps);

        StartCoroutine(Move());

    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;
        canPass = false;

        while (Steps > 0)
        {

            // 지나가면서 해야되는 이벤트 있는지(칸번호 확인)
            routePosition = EventNodes.instance.PassCheck(routePosition);

            canPass = EventNodes.instance.PassingEvent(routePosition);
            yield return new WaitUntil(() => canPass);
            routePosition++;

            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            nextPos.y = 1.5f;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            Steps--;

        }
        isMoving = false;

        // 멈춘 칸 이벤트 실행(칸번호 확인)
    }

    bool MoveToNextNode(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime));
    }


}