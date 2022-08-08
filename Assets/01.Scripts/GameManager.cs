using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager instance;

    //Contains all players photon view
    List<PhotonView> players = new List<PhotonView>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //클라이언트에서 보내는 비율 1초에 30번
        PhotonNetwork.SendRate = 30;
        //OnPhotonSerializeView의 호출 비율 1초에 30번
        PhotonNetwork.SerializationRate = 30;
        //Player Spawn
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0),Quaternion.identity);

    }

    public void AddPlayer(PhotonView pv)
    {
        players.Add(pv);

        //인원수가 다 들어왔다면 턴 계산
        if(players.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            //턴계산
            print("all came!!");


            //SetPlayerTurn();
        }

    }

    ////current turn index
    //int currTurnIdx = -1;
    ////next turn insdex
    //int nextTurnIdx = 0;
    //public void SetPlayerTurn()
    //{
    //    if (!PhotonNetwork.IsMasterClient) return;

    //    if (currTurnIdx != -1)
    //    {
    //        //previous turn -> false
    //        players[currTurnIdx].RPC("SetPlayerTurn", RpcTarget.All, false);

    //    }


    //    //next turn -> true
    //    players[nextTurnIdx].RPC("SetPlayerTurn", RpcTarget.All, true);


    //    //update current turn index
    //    currTurnIdx = nextTurnIdx;

    //    //update nect turn index
    //    nextTurnIdx++;
    //    nextTurnIdx %= players.Count;

    //}

    
}
