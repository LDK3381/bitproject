using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletSpawn : MonoBehaviourPun
{
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        try
        {
            //총알이 벽에 부딪치면 바로 소멸
            if (other.tag == "Wall" || other.tag == "BreakableWall")
            {
                Debug.Log("벽 충돌");
                photonView.RPC("DestroyRPC", RpcTarget.AllBuffered);
            }
            if (!photonView.IsMine && other.tag == "Player" && other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("캐릭터 충돌");
                other.GetComponent<StatusManager>().MtDecreaseHp(1);
                photonView.RPC("DestroyRPC", RpcTarget.AllBuffered);
            }
        }
        catch
        {
            Debug.Log("BulletSpawn.OnTriggerEnter Error");
        }
    }

    [PunRPC]
    void DestroyRPC()
    {
        Destroy(gameObject);
    }
}
