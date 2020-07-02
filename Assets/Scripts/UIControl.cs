using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UIControl : MonoBehaviourPunCallbacks
{
    public GameObject obj;

    void Awake()
    {
        Screen.SetResolution(900, 900, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
        Debug.Log("서버 연결");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("서버 연결");
        obj.SetActive(false);
        PhotonNetwork.LoadLevel("PlayerScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
