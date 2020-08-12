using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MtSealKey : MonoBehaviourPun
{
    public PhotonView PV;

    public GameObject Pcontrol;
    public GameObject Gcontrol;
    public GameObject WeaponM;
    public GameObject BombS;
    public GameObject MouseR;

    StatusManager statusM;
    MtFinal final;

    private void Start()
    {
        statusM = FindObjectOfType<StatusManager>();
        final = FindObjectOfType<MtFinal>();
    }

    private void Update()
    {
        if(PV.IsMine)
            WinLoseCheck(); //승패 체크
    }

    //승패체크
    private void WinLoseCheck()
    {
        if (PhotonNetwork.PlayerList.Length == 1 && statusM.currentHp > 0)
            final.winPanel.SetActive(true);
        else if (statusM.currentHp <= 0)
            final.losePanel.SetActive(true);
        else
        {
            final.winPanel.SetActive(false);
            final.losePanel.SetActive(false);
        }

    }

    public void SealKey()
    {
        try
        {
            Pcontrol.GetComponent<MtPlayerController>().enabled = false;
            Gcontrol.GetComponent<MtGunController>().enabled = false;
            WeaponM.GetComponent<MtWeaponManager>().enabled = false;
            BombS.GetComponent<MtBombSpawn>().enabled = false;
            MouseR.GetComponent<MtMouseRotate>().enabled = false;
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
