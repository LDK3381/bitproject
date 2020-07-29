using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerChoice : MonoBehaviourPun, IPunObservable
{
    public Button[] button;

    private Button btn;

    [PunRPC]
    public void Choice(string name)
    {
        foreach (Button b in button)
        {
            if (b.CompareTag(name))
            {
                Debug.Log("button false");
                btn = b;
                btn.interactable = false;
            }
            else
            {
                b.interactable = false;
            }
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
