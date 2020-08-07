using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MtShotGunController : MonoBehaviourPun
{
    public GameObject bulletSpawn;

    [Header("현재 장착된 총")]
    [SerializeField] Gun shotGun = null;
    public Text txt_ShotGunBullet;

    private float FireRate;
    private Transform playerTransform = null;

    void Start()
    {
        try
        {
            FireRate = 0.5f;

            //시작과 동시에 총알 개수 설정
            BulletUiSetting();
        }
        catch
        {
            Debug.Log("MtShotGunController.Start Error");
        }
    }

    private void Update()
    {
        try
        {
            //캐릭터 프리팹 좌표값 가져옴
            playerTransform = FindToName();

            photonView.RPC("TryFire", RpcTarget.AllBuffered);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    [PunRPC]
    public void BulletUiSetting()
    {
        try
        {
            txt_ShotGunBullet.text = "x " + shotGun.bulletCount;
        }
        catch
        {
            Debug.Log("MtShotGunController.BulletUiSetting Error");
        }
    }

    [PunRPC]
    // 총알 발사 시도
    void TryFire()
    {
        try
        {
            if (!photonView.IsMine)
                return;

            if (FireRate > 0)
            {
                //Time.deltaTime : 현재 프레임을 실행하는데 걸리는 시간(60분의 1)
                FireRate -= Time.deltaTime;
            }

            // Fire1(마우스 좌클릭)과 노말건의 총알이 0발 이상일떄
            if (Input.GetButton("Fire1") && shotGun.bulletCount > 0)
            {
                if (FireRate <= 0)
                {
                    FireRate = 0.5f;
                    photonView.RPC("SGFire", RpcTarget.AllBuffered);
                    Debug.Log("TryFire");
                }
            }
        }
        catch
        {
            Debug.Log("MtShotGunController.TryFire Error");
        }
    }

    [PunRPC]
    // 총알 발사
    void SGFire()
    {
        try
        {
            //총알감소
            shotGun.bulletCount--;

            photonView.RPC("BulletUiSetting", RpcTarget.All);

            //애니메이터
            shotGun.animator.SetTrigger("ShotGunFire");

            //효과음
            SoundManager.instance.PlaySE(shotGun.sound_Fire);

            //총알 발사 이펙트
            shotGun.ps_MuzzleFlash.Play();

            //총알 Instantiate(무한 생성)
            var clone = Instantiate
                (shotGun.go_Bullet_Prefab, bulletSpawn.transform.position, Quaternion.identity);

            clone.transform.rotation = playerTransform.rotation;

            //총알 AddForce(발사)
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * shotGun.speed);
        }
        catch
        {
            Debug.Log("MtShotGunController.SGFire Error");
        }
    }

    //자신이 선택한 캐릭터 찾기
    private Transform FindToName()
    {
        switch (PhotonNetwork.LocalPlayer.NickName)
        {
            case "Kitchen":
                return GameObject.Find("Kitchen(Clone)").transform.Find("ChickenPrefab");
            case "Gurow":
                return GameObject.Find("Gurow(Clone)").transform.Find("CrowPrefabs");
            case "PapaGu":
                return GameObject.Find("PapaGu(Clone)").transform.Find("ParrotPrefabs");
            case "RainGuw":
                return GameObject.Find("RainGuw(Clone)").transform.Find("ChickenRainbowPrefab");
            default: return null;
        }
    }
}
