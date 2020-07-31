using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MtFinal : MonoBehaviourPun
{
    //게임 나가기 버튼
    public void ExitGame()
    {
        Debug.Log("게임 나가기");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene("UI");
    }
}
