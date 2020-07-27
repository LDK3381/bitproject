using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MtPlayerCount : MonoBehaviour
{
    [SerializeField] Text nowInfo = null;
    [SerializeField] Text totalInfo = null;

    const int MAXIMUM = 2;

    void Start()
    {
        nowInfo.text = "NOW : 0";
        totalInfo.text = "MAX : 0";
    }

    void Update()
    {
        JoinGame();
    }

    private void JoinGame()
    {
        if (PhotonNetwork.InRoom)
        {

            //info.text = "현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name;
            //string playerStr = "방에 있는 플레이어 목록 : ";
            //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            //print(playerStr);

            nowInfo.text = "NOW : " + PhotonNetwork.CurrentRoom.PlayerCount;
            totalInfo.text = "MAX : " + PhotonNetwork.CurrentRoom.MaxPlayers;

            if (PhotonNetwork.CurrentRoom.PlayerCount == MAXIMUM)
            {
                PhotonNetwork.LoadLevel("MultiScene");
            }
        }
    }
}
