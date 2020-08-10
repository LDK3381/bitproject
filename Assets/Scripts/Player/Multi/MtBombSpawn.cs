using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Photon.Pun;

public class MtBombSpawn : MonoBehaviourPun
{
    public Transform throwPoint = null;
    public GameObject bomb = null;
    public GameObject weapon = null;
    public Text txt_Bomb = null;

    private GameObject bombInstance = null;
    private int bombCount = 1;

    void Start()
    {
        BombUiSetting();
    }

    public void BombCountUp(int extra)
    {
        bombCount += extra;
    }

    [PunRPC]
    public void BombUiSetting()
    {
        txt_Bomb.text = "x " + bombCount;
    }

    [PunRPC]
    public void CreateBomb()
    {
        //현재 무기가 폭탄일 때에만 투척하도록 제한
        if (weapon.activeSelf == true && bombCount > 0)
        {
            bombCount--;
            photonView.RPC("BombUiSetting", RpcTarget.All);
            bombInstance = Instantiate(bomb, throwPoint.position, throwPoint.rotation);
            Destroy(bombInstance, 2);
        }
    }
}
