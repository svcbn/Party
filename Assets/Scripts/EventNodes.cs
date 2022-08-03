using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodes : MonoBehaviour
{
    public Route allRoutes;
    public List<Transform> eventNodeList = new(65);
    // Start is called before the first frame update

    private void Start()
    {
        allRoutes = GetComponent<Route>();
        FillEventNodes();
    }

    private void FillEventNodes()
    {
        eventNodeList.Clear();
        eventNodeList.Insert(8, allRoutes.childNodeList[8]);

    }
}
