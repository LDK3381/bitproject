using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CarmeraSetup : MonoBehaviourPun
{
    void Start()
    {
        CameraSet();
    }
    private void CameraSet()
    {
        try
        {
            if (photonView.IsMine)
            {
                CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();

                followCam.Follow = transform;
            }
        }
        catch
        {
            Debug.Log("CameraSetUp.CameraSet Error");
        }
    }
}
