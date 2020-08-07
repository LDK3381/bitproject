using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireGun_Bullet : MonoBehaviourPun
{
    private bool flag = true;

    private void Start()
    {
        try
        {
            flag = PlayerInRoom();
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private bool PlayerInRoom()
    {
        if (PhotonNetwork.InRoom)
            return true;    //멀티
        else
            return false;   //싱글
    }

    private void DecreaseHp(GameObject other)
    {
        try
        {
            if (flag == true)
            {
                other.transform.GetComponent<StatusManager>().MtDecreaseHp(1);
            }
            else
            {
                other.transform.GetComponent<StatusManager>().SgDecreaseHp(1);
            }
        }
        catch
        {
            Debug.Log("Bullet.DecreaseHp Error");
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("캐릭터 충돌");
            DecreaseHp(other);
        }
        //폭탄 범위 내 벽이 있으면 벽 폭파
        if (other.transform.CompareTag("BreakableWall"))
        {
            Debug.Log("벽 충돌");
            other.transform.GetComponent<WallEvent>().Explode();
        }
    }
}
