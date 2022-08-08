using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField inputRoomName;
    public InputField inputMaxPlayer;

    public Button btnCreateRoom;
    public Button btnJoinRoom;

    public GameObject roomInfoFactory;
    public Transform roomlistContent;

    private void Start()
    {
        inputRoomName.onValueChanged.AddListener(OnChangedRoomName);
        inputMaxPlayer.onValueChanged.AddListener(OnChangedMaxPlayer);
    }

    private void Update()
    {

    }

    void OnChangedRoomName(string text)
    {
        //1. input room name 애 값이 있으면 join room 활성화 
        btnJoinRoom.interactable = text.Length > 0;
        OnChangedMaxPlayer(inputMaxPlayer.text);
    }

    void OnChangedMaxPlayer(string text)
    {
        //2.Input room name 과 input max player 의 값이 있으면 create room 활성화
        btnCreateRoom.interactable =
            btnJoinRoom.interactable && text.Length > 0;
    }



    //방 생성
    public void OnClickCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = byte.Parse(inputMaxPlayer.text);

        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

    }




    //방 참가 
    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(inputRoomName.text);

    }

    //방 참가 완료 
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);


        //게임씬으로 이동
        PhotonNetwork.LoadLevel("SceneGame");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }





    Dictionary<string, RoomInfo> cacheRoom = new Dictionary<string, RoomInfo>();

    //해당 방 목록에 변경사항이 있으면 호출 (추가, 삭제, 참가 )
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);


        //1. room list UI Delete
        DeleteRoomListUI();
        //2. room cache update
        UpdateCacheRoom(roomList);

        //3. create room list UI using room cache
        CreateRoomListUI();
    }

    void DeleteRoomListUI()
    {
        //delete all chidren of "roomlistContent"
        foreach(Transform tr in roomlistContent)
        {
            Destroy(tr.gameObject);
        }
    }

    void CreateRoomListUI()
    {
        foreach(RoomInfo info in cacheRoom.Values)
        {
            //1. Create roomInfo
            GameObject room = Instantiate(roomInfoFactory);

            //2. Setting as roomlistContent children
            room.transform.SetParent(roomlistContent);

            //3. Type room information
            RoomInfoButton roomInfoBtn = room.GetComponent<RoomInfoButton>();
            room.GetComponent<RoomInfoButton>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
            roomInfoBtn.AddOnClickAction(UpdateRoomName);
            

        }
    }


    void UpdateRoomName(string roomName)
    {
        inputRoomName.text = roomName;
    }

    void UpdateCacheRoom(List<RoomInfo> roomList)
    {
        //1. room list 순차적으로 돌면서
        for (int i = 0; i < roomList.Count; i++)
        {
            //2. 해당이름이 cacheRoom 에 key 값으로 설정이 되었다면
            if(cacheRoom.ContainsKey(roomList[i].Name))
            {
                //3. 해당 roomInfo 갱신(변경 , 삭제)
                //만약에 삭제가 되었다면
                if(roomList[i].RemovedFromList)
                {
                    cacheRoom.Remove(roomList[i].Name);
                    continue;
                }
            }

            //4. 그렇지 않으면 roomInfo 를 cacheRoom에 추가 또는 변경한다 
            cacheRoom[roomList[i].Name] = roomList[i];
        }
    }


}
