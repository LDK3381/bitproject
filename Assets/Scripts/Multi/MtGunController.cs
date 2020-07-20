using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   // 총알 개수 UI 설정을 위한 네임스페이스
using Photon.Pun;
using Photon.Realtime;

public class MtGunController : MonoBehaviourPun
{
    public GameObject obj;      //총알 스폰

    [Header("현재 장착된 총")]
    [SerializeField] Gun nomalGun = null;

    public float fireRate = 0;
    public float speed = 10f;

    //[SerializeField] Text txt_NomalGunBullet = null;

    void Start()
    {
        fireRate = 0.5f;

        //시작과 동시에 총알 개수 설정
        BulletUiSetting();
    }

    private void Update()
    {
        photonView.RPC("TryFire", RpcTarget.AllBuffered);
    }

    public void BulletUiSetting()
    {
        //txt_NomalGunBullet.text = "x " + nomalGun.bulletCount;
    }

    // 총알 발사 시도
    [PunRPC]
    public void TryFire()
    {
        if (!photonView.IsMine) return;

        if (fireRate > 0)
        {
            //Time.deltaTime : 현재 프레임을 실행하는데 걸리는 시간(60분의 1)
            fireRate -= Time.deltaTime;
        }

        // Fire1(마우스 좌클릭)과 노말건의 총알이 0발 이상일떄
        if (Input.GetButton("Fire1") && nomalGun.bulletCount > 0)
        {
            if (fireRate <= 0)
            {
                fireRate = 0.5f;
                photonView.RPC("Fire", RpcTarget.All);
                Debug.Log("TryFire");
            }
        }
    }

    // 총알 발사
    [PunRPC]
    public void Fire()
    {
        //총알감소
        //nomalGun.bulletCount--;

        //BulletUiSetting();

        //애니메이터
        nomalGun.animator.SetTrigger("GunFire");

        //효과음
        SoundManager.instance.PlaySE(nomalGun.sound_Fire);

        //총알 발사 이펙트
        nomalGun.ps_MuzzleFlash.Play();

        //총알 Instantiate(무한 생성)
        var clon = Instantiate(nomalGun.go_Bullet_Prefab, obj.transform.position, Quaternion.identity);
        //총알 AddForce(발사)
        clon.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        Debug.Log("Fire");
    }
}
