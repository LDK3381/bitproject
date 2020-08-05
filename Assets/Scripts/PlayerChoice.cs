using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerChoice : MonoBehaviourPun
{
    public Button[] button;
    public Text choiceText;

    private Button btn;

    [PunRPC]
    public void Choice(string name)
    {
        foreach (Button b in button)
        {
            if (b.CompareTag(name))
            {
                PhotonNetwork.LocalPlayer.NickName = name;
                Debug.Log("button false");
                Debug.Log(PhotonNetwork.LocalPlayer.NickName);
                btn = b;
                btn.interactable = false;
            }
            else
            {
                b.interactable = false;
            }
        }
    }
}
