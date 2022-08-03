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
        // 1 : +����
        // 2 : -����
        // 3 : ��Ű
        // 4 : ��Ÿ ��ġ����
        // 5 : ������ĭ
        // 6 : ������
        // 7 : ������
        // 8 : �����̺�Ʈĭ
        // 9 : ����ĭ
        // 10 : ����ĭ
        // 11 : ��Ÿĭ
        // 12 : 1��1ĭ
        // 13 : ����ĭ
        // 14 : ����ĭ
        // 15 : ��ȯĭ

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
        // ���� ����ºκе� ���� üũ�ؼ� ����
        // 

        switch(eventNum)        // ���� �ֻ������� +1 ����� �Ǵ� ĭ��
        {
            case 6:             // ������ ����
                break;
            case 7:             // ������ �����
                break;          
            case 9:             // ���� ������ ����
                break;          
            case 10:            // ������ ����
                break;          
            case 11:            // ��Ÿ ���� ����
                break;          
            case 13:            // ����
                break;          
            case 14:            // �÷��̾� �����ؼ� ������
                break;
            default:
                break;

        }
    }


    IEnumerator StopCheck()
    {

    }
}

