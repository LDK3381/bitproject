﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class UIServerJoin : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";   //게임 버전

    public Text connectionInfoText; //네트워크 정보를 표시
    public Button JoinButton;       //멀티 서버 접속 버튼
    public Text nowInfoText;
    public Text totalInfoText;

    const int MAXIMUM = 2;

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        JoinButton.interactable = false;
        connectionInfoText.text = "CONNECTING TO THE MASTER SERVER...";
        nowInfoText.text = "NOW : 0";
        totalInfoText.text = "MAX : 0";
    }

    private void Update()
    {
        JoinGame();
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
        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "CONNECT TO ROOM...";
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
    }

    private void JoinGame()
    {
        if (PhotonNetwork.InRoom)
        {
            nowInfoText.text = "NOW : " + PhotonNetwork.CurrentRoom.PlayerCount;
            totalInfoText.text = "MAX : " + PhotonNetwork.CurrentRoom.MaxPlayers;

            if (PhotonNetwork.CurrentRoom.PlayerCount == MAXIMUM)
            {
                Debug.Log(RandomRoom());
                PhotonNetwork.LoadLevel(RandomRoom());
            }
        }
    }

    //랜덤 맵선택
    private string RandomRoom()
    {
        if (Random.Range(0, 1) == 0)
            return "MtMap1";
        else
            return "MtMap2";
    }

    public void OnExit()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("UI");
    }
}
