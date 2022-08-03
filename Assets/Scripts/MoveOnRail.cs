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
            Debug.Log("주사위 숫자 : " + steps);

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
            // 지나가면서 해야되는 이벤트 있는지(칸번호 확인)
            
            
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
        // 멈춘 칸 이벤트 실행(칸번호 확인)
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
