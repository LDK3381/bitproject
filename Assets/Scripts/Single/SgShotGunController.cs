using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SgShotGunController : MonoBehaviour
{
    [Header("현재 장착된 총")]
    [SerializeField] Gun shotGun = null;
    public Text txt_ShotGunBullet;

    private float FireRate;

    void Start()
    {
        //시작과 동시에 발사
        FireRate = 0;

        //시작과 동시에 총알 개수 설정
        BulletUiSetting();
    }

    void Update()
    {
        FireRateCalc();
        TryFire();
    }

    public void BulletUiSetting()
    {
        try
        {
            txt_ShotGunBullet.text = "x " + shotGun.bulletCount;
        }
        catch
        {
            Debug.Log("SgShotGunController.BulletUiSetting Error");
        }
    }

    private void FireRateCalc()
    {
        try
        {
            if (FireRate > 0)
            {
                //Time.deltaTime : 현재 프레임을 실행하는데 걸리는 시간(60분의 1)
                FireRate -= Time.deltaTime;
            }

        }
        catch
        {
            Debug.Log("SgShotGunController.FireRateCalc Error");
        }
    }

    // 총알 발사 시도
    private void TryFire()
    {
        try
        {
            // Fire1(마우스 좌클릭)과 노말건의 총알이 0발 이상일떄
            if (Input.GetButton("Fire1") && shotGun.bulletCount > 0)
            {
                if (FireRate <= 0)
                {
                    FireRate = shotGun.fireRate;
                    Fire();
                }
            }
        }
        catch
        {
            Debug.Log("SgShotGunController.TryFire Error");
        }
    }

    // 총알 발사
    private void Fire()
    {
        try
        {
            //총알감소
            shotGun.bulletCount--;

            BulletUiSetting();

            //애니메이터
            shotGun.animator.SetTrigger("ShotGunFire");

            //효과음
            SoundManager.instance.PlaySE(shotGun.sound_Fire);

            //총알 발사 이펙트
            shotGun.ps_MuzzleFlash.Play();

            //총알 Instantiate(무한 생성)
            var clone = Instantiate
                (shotGun.go_Bullet_Prefab, shotGun.ps_MuzzleFlash.transform.position, Quaternion.identity);

            //총알 AddForce(발사)
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * shotGun.speed);
        }
        catch
        {
            Debug.Log("SgShotGunController.Fire Error");
        }
    }
}
