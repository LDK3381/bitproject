using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UIControl : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";   //게임 버전

    public Text connectionInfoText; //네트워크 정보를 표시
    public Button JoinButton;       //멀티 서버 접속 버튼

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        JoinButton.interactable = false;
        connectionInfoText.text = "마스터 서버에 접속중...";
    }

    public override void OnConnectedToMaster()
    {
        JoinButton.interactable = true;
        connectionInfoText.text = "온라인 : 마스터 서버와 접속 성공...";

        Debug.Log("마스터 연결");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        JoinButton.interactable = false;
        connectionInfoText.text = "오프라인 : 마스터 서버와 접속 실패...\n접속 재시도중...";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        JoinButton.interactable = false;
        Debug.Log("Connect()실행");
        if(PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "방에 접속중...";
            Debug.Log("IsConnect안");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "오프라인 : 마스터 서버와 접속 실패...\n접속 재시도중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "빈 방이 없음, 새로운 방 생성...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("서버 연결");
        connectionInfoText.text = "방 참가 성공";
        PhotonNetwork.LoadLevel("MultiScene");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
