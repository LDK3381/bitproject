using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletSpawn : MonoBehaviourPun
{
    //public float bulletSpeed = 10f;                 //발사 속도

    void Start()
    {
        Destroy(gameObject, 2f);
        
    }

    private void Update()
    {
        //총알 AddForce(발사)
        //GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //총알이 벽에 부딪치면 바로 소멸
        if (other.tag == "Wall" || other.tag == "BreakableWall")
        {
            Debug.Log("벽 충돌");
            photonView.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
        if(!photonView.IsMine && other.tag == "Player" && other.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("캐릭터 충돌");
            other.GetComponent<StatusManager>().DecreaseHp(1);
            photonView.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void DestroyRPC()
    {
        Destroy(gameObject);
    }
}
