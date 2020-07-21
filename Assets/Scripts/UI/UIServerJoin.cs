﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UIServerJoin : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";   //게임 버전

    public Text connectionInfoText; //네트워크 정보를 표시
    public Button JoinButton;       //멀티 서버 접속 버튼

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        JoinButton.interactable = false;
        connectionInfoText.text = "CONNECTING TO THE MASTER SERVER...";
    }

    public override void OnConnectedToMaster()
    {
        JoinButton.interactable = true;
        connectionInfoText.text = "ONLINE : SUCCESSFUL CONNECT TO THE MASTER SERVER...";

        Debug.Log("마스터 연결");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        JoinButton.interactable = false;
        connectionInfoText.text = "OFFLINE : FAILED TO CONNECT TO THE MASTER SERVER...\n RETRY CONNECT...";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        JoinButton.interactable = false;
        Debug.Log("Connect()실행");
        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "CONNECT TO ROOM...";
            Debug.Log("IsConnect안");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "OFFLINE : FAILED TO CONNECT TO THE MASTER SERVER...\n RETRY CONNECT...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "NO EMPTY ROOM, CREATING A NEW ROOM...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("서버 연결");
        connectionInfoText.text = "CONNECT TO ROOM...";
        
        PhotonNetwork.LoadLevel("MultiScene");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
