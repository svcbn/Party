using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodes : MonoBehaviour
{
    public Route allRoutes;
    public int[] eventNodes = new int[65];
    // Start is called before the first frame update

    private void Start()
    {
        allRoutes = GetComponent<Route>();
        FillNodeEvents();
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

    public void passCheck(int eventNum)
    {
        // 루프 끊기는부분들 먼저 체크해서 연결
        // 

        switch(eventNum)        // 남은 주사위수에 +1 해줘야 되는 칸들
        {
            case 6:             // 갈림길 선택
                break;
            case 7:             // 쿵쿵이 통행료
                break;          
            case 9:             // 상점 아이템 구매
                break;          
            case 10:            // 아이템 강매
                break;          
            case 11:            // 스타 구매 찬스
                break;          
            case 13:            // 월급
                break;          
            case 14:            // 플레이어 선택해서 돈뺏기
                break;
            default:
                break;

        }
    }


    IEnumerator StopCheck()
    {

    }
}

