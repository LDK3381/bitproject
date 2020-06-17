using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControll : MonoBehaviour
{
    [SerializeField] ParticleSystem ps_MuzzleFlash;     //적용할 파티클 효과
    [SerializeField] GameObject go_Bullet_Prefab;       //적용 대상 총알 오브젝트
    [SerializeField] float bulletSpeed;                 //발사 속도

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
