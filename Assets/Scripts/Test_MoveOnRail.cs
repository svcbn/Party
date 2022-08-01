using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_MoveOnRail : MonoBehaviour
{
    //public Route currentRoute;

    int routePosition;

    public int steps;

    bool isMoving;

    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;

        while(steps > 0)
        {
            //Vector3 nextPos = currentRoute;
        }
        isMoving = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
