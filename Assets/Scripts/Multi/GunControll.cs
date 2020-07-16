using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   // 총알 개수 UI 설정을 위한 네임스페이스
using Photon.Pun;
using Photon.Realtime;

public class GunControll : MonoBehaviourPun
{
    public GameObject obj;      //총알 스폰

    [Header("현재 장착된 총")]
    [SerializeField] Gun nomalGun = null;

    float FireRate;
    float Speed = 10f;

    [SerializeField] Text txt_NomalGunBullet = null;

    void Start()
    {
        //시작과 동시에 발사
        FireRate = 0;

        //시작과 동시에 총알 개수 설정
        BulletUiSetting();
    }

    public void BulletUiSetting()
    {
        //txt_NomalGunBullet.text = "x " + nomalGun.bulletCount;
    }

    public void FireRateCalc()
    {
        if (FireRate > 0)
        {
            //Time.deltaTime : 현재 프레임을 실행하는데 걸리는 시간(60분의 1)
            FireRate -= Time.deltaTime;
        }
    }

    // 총알 발사 시도
    public void TryFire()
    {
        //FireRateCalc();
        // Fire1(마우스 좌클릭)과 노말건의 총알이 0발 이상일떄
        if (Input.GetButton("Fire1") && nomalGun.bulletCount > 0)
        {
            if (FireRate <= 0)
            {
                FireRate = nomalGun.fireRate;
                //Fire();
                photonView.RPC("Fire", RpcTarget.All);
                Debug.Log("TryFire");
            }
            else
            {
                Debug.Log("FireRate Error");
            }
        }
        else
        {
            Debug.Log("TryFire Error");
        }
    }

    // 총알 발사
    [PunRPC]
    public void Fire()
    {
        //총알감소
        //nomalGun.bulletCount--;

        BulletUiSetting();

        //애니메이터
        nomalGun.animator.SetTrigger("GunFire");

        //효과음
        SoundManager.instance.PlaySE(nomalGun.sound_Fire);

        //총알 발사 이펙트
        nomalGun.ps_MuzzleFlash.Play();

        //총알 Instantiate(무한 생성)
        var clone = PhotonNetwork.Instantiate("Bullet", obj.transform.position, Quaternion.identity);
        //총알 AddForce(발사)
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * Speed);
        Debug.Log("Fire");
    }
}
