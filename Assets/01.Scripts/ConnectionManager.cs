using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    //Input Field
    public InputField inputID;
    //Gameversion
    public string gameVersion = "1";

    public void OnClickConnect()
    {
        //만약에 아이디에 값이 있으면
        if(inputID.text.Length == 0)
        {
            print("ID가 없습니다 ");
            return;
        }

        //포톤기본 셋팅 후에
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.AutomaticallySyncScene = false;

        //접속시도
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }


    //로비에 들어갈 수 있는 상\
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        //닉네임 설정 
        PhotonNetwork.NickName = inputID.text;

        //로비 진입
        PhotonNetwork.JoinLobby();
        //PhotonNetwork.JoinLobby(new TypedLobby("MediciLobby",LobbyType.Default));
    }

    //로비 접속 성공시 호출 되는 함수
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //로비씬 이동
        PhotonNetwork.LoadLevel("SceneLobby");
    }
}
