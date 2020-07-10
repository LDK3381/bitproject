using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   // 총알 개수 UI 설정을 위한 네임스페이스
using Photon.Pun;
using Photon.Realtime;

public class GunControll : MonoBehaviourPun
{
    public GameObject obj;      //총알 스폰

    [Header("총 연사속도 조정")]
    public float fireRate;

    [Header("총알 개수")]
    public int bulletCount;
    public int maxBulletCount;

    [Header("애니메이터")]
    public Animator animator;

    [Header("총알 발사 사운드")]
    public string sound_Fire;

    [SerializeField] ParticleSystem ps_MuzzleFlash = null;   //적용할 파티클 효과
    [SerializeField] float bulletSpeed = 10f;                 //발사 속도

    [SerializeField] Text txt_CurrentGunBullet;              // 총알 개수를 나타내는 텍스트 UI

    [PunRPC]
    public void Fire()
    {
        if (photonView.IsMine)
        {
            Debug.Log("총알 발사");
            ps_MuzzleFlash.Play();          //파티클 효과 발생

            //총알 Instantiate(무한 생성)
            var clone = PhotonNetwork.Instantiate("Bullet", obj.transform.position, Quaternion.identity);
            //총알 AddForce(발사)
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }
    }
}
