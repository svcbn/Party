using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class ChatManager : MonoBehaviour
{
    public InputField inputChat;
    public GameObject chatItemFactory;

    public RectTransform chatContent;
    public RectTransform chatView;

    Color nameColor;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        nameColor = new Color32(
            (byte)Random.Range(0,256),
            (byte)Random.Range(0, 256),
            (byte)Random.Range(0, 256),255);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (inputChat.text.Length == 0) return;

            string chatText = "<color=#" + ColorUtility.ToHtmlStringRGB(nameColor) + ">" + PhotonNetwork.NickName + "</color>" +  " : " + inputChat.text;

            GameObject chatItem = Instantiate(chatItemFactory);
            chatItem.GetComponent<Text>().text = chatText;

            StartCoroutine(AddChat(chatItem));

            inputChat.text = "";

            //photonView.RPC("RpcAddChat", RpcTarget.All, chatText);

        }

        //esc키를 누르면 커서가 나타난다
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Cursor.visible = true;
        //}

        //마우스 클릭시 해당 위치에 UI가 없으면 커서가 사라지게
        //if(Input.GetMouseButtonDown(0))
        //{
        //    if(EventSystem.current.IsPointerOverGameObject() == false)
        //    {
        //        Cursor.visible = false;
        //    }
        //}
    }

    IEnumerator AddChat(GameObject chatItem)
    {
        float prevChatContentH = chatContent.sizeDelta.y;

        yield return new WaitForSeconds(0.1f);
        chatItem.transform.SetParent(chatContent);

        yield return new WaitForSeconds(0.1f);
        //chatContent H > chatView H
        if (chatContent.sizeDelta.y > chatView.sizeDelta.y)
        {

            if(prevChatContentH - chatView.sizeDelta.y <= chatContent.anchoredPosition.y)
            {
                //chatContent y = chatContent H - chatView H
                chatContent.anchoredPosition = new Vector2(0, chatContent.sizeDelta.y - chatView.sizeDelta.y);
            }

        }

    }
}
