using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerChoice : MonoBehaviourPun, IPunObservable
{
    public Button[] button;

    [PunRPC]
    public void Choice(string name)
    {
        foreach (Button b in button)
        {
            if (b.CompareTag(name))
            {
                Debug.Log("button false");
                b.interactable = false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {

        }
    }
}
