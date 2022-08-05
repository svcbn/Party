using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodes : MonoBehaviour
{
    public Route allRoutes;
    public int[] eventNodes = new int[65];
    // Start is called before the first frame update9
    public static EventNodes instance;

    GameObject dArrowUp;
    GameObject dArrowRight;
    GameObject dArrowDown;
    GameObject dArrowLeft;
    GameObject ArrowUp;
    GameObject ArrowRight;
    GameObject ArrowDown;
    GameObject ArrowLeft;

    private void Awake() 
    {
        instance = this;

        allRoutes = GetComponent<Route>();

        FillNodeEvents();
    }

    private void Start()
    {
        dArrowUp = GameObject.Find("darkArrow_up");
        dArrowRight = GameObject.Find("darkArrow_right");
        dArrowDown = GameObject.Find("darkArrow_down");
        dArrowLeft = GameObject.Find("darkArrow_left");
        ArrowUp = GameObject.Find("arrow_up");
        ArrowRight = GameObject.Find("arrow_right");
        ArrowDown = GameObject.Find("arrow_down");
        ArrowLeft = GameObject.Find("arrow_left");
    }

    private void FillNodeEvents()
    {
        // 0 : default
        // 1 : +코인
        // 2 : -코인
        // 3 : 럭키
        // 4 : 스타 위치변경
        // 5 : 아이템칸
        // 6 : 갈림길
        // 7 : 쿵쿵이
        // 8 : 쿠파이벤트칸
        // 9 : 상점칸
        // 10 : 쿠파칸
        // 11 : 스타칸
        // 12 : 1대1칸
        // 13 : 일주칸
        // 14 : 유령칸
        // 15 : 교환칸

        int[] temp = { 0, 4, 5, 1, 3, 3, 1, 1, 6, 4,
                       1, 1, 8, 1, 9, 1, 3, 3, 1, 1,
                       2, 3, 3, 1, 2, 4, 10, 1, 1, 12,
                       3, 1, 13, 7, 2, 1, 1, 3, 1, 4,
                       1, 3, 1, 2, 11, 1, 4, 1, 8, 2,
                       14, 3, 1, 5, 12, 4, 1, 6, 7, 8,
                       1, 3, 15, 4, 5 };

        eventNodes = temp;

    }

    public int PassCheck(int currentPos)
    {
        // 루프 끊기는부분들 먼저 체크해서 연결
        if (currentPos == 32) currentPos = 0;
        if (currentPos == 64) currentPos = 33;
        if (currentPos == 58) currentPos = 15;
        return currentPos;
    }
    public bool PassingEvent(int Position)
    {
        bool canPass = true;
        switch (eventNodes[Position])        // 남은 주사위수에 +1 해줘야 되는 칸들
        {

            case 6:             // 갈림길 선택
                canPass = false;
                StartCoroutine(SelectWay(Position));
                MoveOnRail.Instance.steps++;
                Debug.Log("갈림길");
                break;
            case 7:             // 쿵쿵이 통행료
                //PayTolls();
                MoveOnRail.Instance.steps++;
                Debug.Log("쿵쿵이");
                break;
            case 9:             // 상점 아이템 구매
                //ItemShop();
                MoveOnRail.Instance.steps++;
                Debug.Log("상점");
                break;
            case 10:            // 아이템 강매
                //HardSell();
                MoveOnRail.Instance.steps++;
                Debug.Log("강매");
                break;
            case 11:            // 스타 구매 찬스
                //BuyStar();
                MoveOnRail.Instance.steps++;
                Debug.Log("스타구매");
                break;
            case 13:            // 월급
                //Salary();
                MoveOnRail.Instance.steps++;
                Debug.Log("월급");
                break;
            case 14:            // 플레이어 선택해서 돈뺏기
                //TakeFromOthers();
                MoveOnRail.Instance.steps++;
                Debug.Log("돈뺏기");
                break;
        }
        return canPass;

    }

    IEnumerator SelectWay(int Position)
    {

        if (Position == 8)
        {
            dArrowUp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

            dArrowRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            bool goUp = true;
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    dArrowUp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    ArrowUp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    dArrowRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    ArrowRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    goUp = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    dArrowRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    ArrowRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    dArrowUp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    ArrowUp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    goUp = false;
                }

                if (Input.GetKeyDown(KeyCode.J))
                {
                    if (!goUp)
                    {
                        MoveOnRail.Instance.routePosition = 32;
                    }
                    MoveOnRail.Instance.canPass = true;
                    dArrowRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    ArrowRight.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    dArrowUp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    ArrowUp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    break;
                }
                yield return null;
            }
        }
        if (Position == 57)
        {
            dArrowDown.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

            dArrowLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            bool goDown = true;
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    dArrowDown.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    ArrowDown.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    dArrowLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    ArrowLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    goDown = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    dArrowLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    ArrowLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    dArrowDown.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    ArrowDown.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    goDown = false;
                }

                if (Input.GetKeyDown(KeyCode.J))
                {
                    if (goDown)
                    {
                        MoveOnRail.Instance.routePosition = 58;
                    }
                    MoveOnRail.Instance.canPass = true;
                    dArrowLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    ArrowLeft.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    dArrowDown.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    ArrowDown.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    break;
                }
                yield return null;
            }
        }


    }
    private void PayTolls()
    {
        throw new NotImplementedException();
    }

    private void ItemShop()
    {
        throw new NotImplementedException();
    }

    private void HardSell()
    {
        throw new NotImplementedException();
    }

    private void BuyStar()
    {
        throw new NotImplementedException();
    }

    private void Salary()
    {
        throw new NotImplementedException();
    }

    private void TakeFromOthers()
    {
        throw new NotImplementedException();
    }

}

