using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinePractice : MonoBehaviour
{
    public GameObject pivot;
    public GameObject Gun;

    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isMoving)
        {
            isMoving = true;
            //Gun.transform.RotateAround(pivot.transform.position, Vector3.left, 10000 * Time.deltaTime);
            print("nice0");
            StartCoroutine(delay());
        }


    }
    IEnumerator delay()
    {
        yield return StartCoroutine(RotateFunction(Gun, pivot, 45));
        yield return StartCoroutine(RotateBackFunction(Gun, pivot, 45));
    }

    IEnumerator RotateFunction(GameObject poll, GameObject pivotPos, float degree)
    {
        print("nice1");
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime * 0.5f;
            poll.transform.RotateAround(pivotPos.transform.position, Vector3.left, degree * Time.deltaTime);
            // If the object has arrived, stop the coroutine
            if (timeSinceStarted >= 0.45f)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

    IEnumerator RotateBackFunction(GameObject poll, GameObject pivotPos, float degree)
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime * 0.5f;
            poll.transform.RotateAround(pivotPos.transform.position, Vector3.right, degree * Time.deltaTime);
            // If the object has arrived, stop the coroutine
            if (timeSinceStarted >= 0.45f)
            {
                isMoving = false;
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }
}