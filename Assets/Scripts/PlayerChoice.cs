using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerChoice : MonoBehaviourPun, IPunObservable
{
    public Button[] button;
    public Text choiceText;

    private Button btn;
    private List<string> namelist;

    [PunRPC]
    public void Choice(string name)
    {
        foreach (Button b in button)
        {
            if (b.CompareTag(name))
            {
                photonView.name = name;
                Debug.Log("button false");
                Debug.Log(name);
                namelist.Add(photonView.name);
                Debug.Log(namelist.Count);

                photonView.RPC("ChoiceAllPlayer", RpcTarget.AllBuffered, name);
                btn = b;
                btn.interactable = false;
            }
            else
            {
                b.interactable = false;
            }
        }
    }

    [PunRPC]
    private void ChoiceAllPlayer(string Nick)
    {
        Debug.Log(PhotonNetwork.PlayerList.Length);
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            choiceText.text += photonView.ControllerActorNr + " : " + Nick + "\n";
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(namelist);
        }
        else
        {
            namelist = (List<string>)stream.ReceiveNext();
        }
    }
}
