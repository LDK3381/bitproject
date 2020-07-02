using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControllers : MonoBehaviour
{
    [Header("현재 장착된 총")]
    [SerializeField] Gun nomalGun;

    float FireRate;

    void Start()
    {
        // 시작과 동시에 발사
        FireRate = 0;
    }

    void Update()
    {
        FireRateCalc();
        TryFire();
    }

    void FireRateCalc()
    {
        if (FireRate > 0)
        {
            // Time.deltaTime : 현재 프레임을 실행하는데 걸리는 시간(60분의 1)
            FireRate -= Time.deltaTime;
        }
    }

    void TryFire()
    {
        if (Input.GetButton("Fire1"))
        {
            if (FireRate <= 0)
            {
                FireRate = nomalGun.fireRate;
                Fire();
            }
        }
    }

    // 총알 발사
    void Fire()
    {
        Debug.Log("총알 발사");
        //애니메이터
        nomalGun.animator.SetTrigger("GunFire");

        //효과음
        SoundManager.instance.PlaySE(nomalGun.sound_Fire);

        //총알 발사 이펙트
        nomalGun.ps_MuzzleFlash.Play();

        //총알 Instantiate(무한 생성)
        var clone = Instantiate
            (nomalGun.go_Bullet_Prefab, nomalGun.ps_MuzzleFlash.transform.position, Quaternion.identity);
        //총알 AddForce(발사)
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * nomalGun.speed);
    }
}
