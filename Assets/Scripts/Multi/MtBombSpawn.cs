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

    GameObject bombInstance = null;

    void Update()
    {
        //마우스 좌클릭 시, 폭탄 투척
        if (Input.GetMouseButtonDown(0))
        {
            photonView.RPC("CreateBomb", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void CreateBomb()
    {
        //현재 무기가 폭탄일 때에만 투척하도록 제한
        if (weapon.activeSelf == true)
        {
            bombInstance = Instantiate(bomb, throwPoint.position, throwPoint.rotation);
            Destroy(bombInstance, 2);
        }
    }
}
