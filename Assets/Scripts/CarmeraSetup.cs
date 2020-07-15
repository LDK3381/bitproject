using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CarmeraSetup : MonoBehaviourPun
{
    void Start()
    {
        if (photonView.IsMine)
        {
            CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();

            followCam.Follow = transform;
            //followCam.LookAt = transform;
        }
    }
}
