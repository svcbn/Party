using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RoomInfoButton : MonoBehaviour
{
    public Text btnText;

    Action<string> onClickAction;


    public void SetInfo(string roomName, int currentPlayer, int maxPlayer)
    {
        gameObject.name = roomName;
        btnText.text = roomName + " (" + currentPlayer + " / " + maxPlayer + ")";
    }

    public void OnClick()
    {
        if(onClickAction != null)
        {
            onClickAction(gameObject.name);
        }

        ////Put "roomName" into "roomname inputField"
        //GameObject input = GameObject.Find("Canvas/InputRoomName");
        //if(input != null)
        //{
        //    InputField field = input.GetComponent<InputField>();
        //    field.text = gameObject.name;
        //}
    }

    public void AddOnClickAction(Action<string> action)
    {
        onClickAction = action;
    }

}
