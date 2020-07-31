using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerChoice : MonoBehaviourPun, IPunObservable
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
                Debug.Log("button false");
                Debug.Log(name);
                ChoiceAllPlayer(name);
                btn = b;
                btn.interactable = false;
            }
            else
            {
                b.interactable = false;
            }
        }
    }

    private void ChoiceAllPlayer(string Nick)
    {
        for(int i =0; i< PhotonNetwork.PlayerList.Length; i++)
        {
            choiceText.text = photonView.ViewID.ToString() + " : " + Nick + "\n";
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(btn);
        }
        else
        {
            btn = (Button)stream.ReceiveNext();
        }
    }
}
