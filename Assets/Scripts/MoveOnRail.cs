using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnRail : MonoBehaviour
{
    public Route currentRoute;

    public float speed = 10f;

    public int steps;

    int routePosition;

    bool isMoving;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            steps = Random.Range(1, 11);
            Debug.Log("�ֻ��� ���� : " + steps);

            StartCoroutine(Move());
            
        }
    }

    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;

        while(steps > 0)
        {
            // �������鼭 �ؾߵǴ� �̺�Ʈ �ִ���(ĭ��ȣ Ȯ��)
            
            
            //if (routePosition != )
            routePosition++;
            
            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            nextPos.y = 1.5f;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            steps--;
        }
        isMoving = false;
        // ���� ĭ �̺�Ʈ ����(ĭ��ȣ Ȯ��)
    }

    bool MoveToNextNode(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
}
