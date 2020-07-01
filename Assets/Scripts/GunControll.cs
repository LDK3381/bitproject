using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   // 총알 개수 UI 설정을 위한 네임스페이스

public class GunControll : MonoBehaviour
{
    [Header("총 연사속도 조정")]
    public float fireRate;

    [Header("총알 개수")]
    public int bulletCount;
    public int maxBulletCount;

    [Header("애니메이터")]
    public Animator animator;

    [Header("총알 스피드")]
    public float speed;

    [Header("총알 발사 사운드")]
    public string sound_Fire;

    [SerializeField] ParticleSystem ps_MuzzleFlash = null;     //적용할 파티클 효과
    [SerializeField] GameObject go_Bullet_Prefab = null;       //적용 대상 총알 오브젝트
    [SerializeField] float bulletSpeed = 0f;                 //발사 속도

    [SerializeField] Text txt_CurrentGunBullet;             // 총알 개수를 나타내는 텍스트 UI

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    public void Fire()
    {
        if(Input.GetMouseButtonDown(0))     //마우스 좌클릭 시,
        {
            Debug.Log("총알 발사");
            ps_MuzzleFlash.Play();          //파티클 효과 발생

            //총알 Instantiate(무한 생성)
            var clone = Instantiate(go_Bullet_Prefab, ps_MuzzleFlash.transform.position, Quaternion.identity);
            //총알 AddForce(발사)
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }          
    }
}
