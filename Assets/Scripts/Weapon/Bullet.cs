using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("피격 이펙트")]
    [SerializeField] GameObject go_RicochetEffect = null;

    [Header("총알 데미지")]
    [SerializeField] int damage = 0;

    [Header("피격 효과음")]
    [SerializeField] string sound_Effect = null;

    private void Start()
    {
        Destroy(gameObject, 7.0f);
    }

    // OnCollisionEnter : 다른 컬라이더와 충돌하는 순간 호출되는 함수
    // (Collision other) : 충돌한 객체의 정보는 other에 담김
    void OnCollisionEnter(Collision other)
    {
        //ContactPoint : 충돌한 객체의 '접촉면'에 대한 정보가 담긴 클래스
        //other.contacts[0] : 총돌한 객체의 접촉면 정보가 담김
        ContactPoint contactPoint = other.contacts[0];

        //효과음
        SoundManager.instance.PlaySE(sound_Effect);

        //피격이펙트 변수
        //Instantiate : 프리팹을 특정 위치에 특정한 방향으로 생성시킴
        //Quaternion.LookRotation : 특정 방향을 바라보게 만드는 메서드
        //normal : 충돌한 컬라이더의 표면 방향
        var clone = Instantiate(go_RicochetEffect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));

        // 이펙트 0.5초후 파괴
        Destroy(clone, 0.5f);
        Destroy(gameObject);
        // 총알 파괴
        if (other.transform.CompareTag("Wall") || other.transform.CompareTag("BreakableWall"))
        {
            Debug.Log("벽 충돌");
            Destroy(this.gameObject);
        }

        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("캐릭터 충돌");
            other.transform.GetComponent<StatusManager>().MtDecreaseHp(1);
            Destroy(this.gameObject);
        }
    }
}
