using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] int damage;

    [SerializeField] float force;

    void OnCollisionEnter(Collision other)
    {
        // CompareTag() : 특정 객체의 태그를 비교하는 메소드
        // 부디친 other의 태그가 Player인지를 확인!
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log(damage + "를 플레이어에게 입혔습니다");
            // AddExplosionForce() : 폭발 반경 내에 있는 다른 Rigidbody를 날려보냄
            other.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);
            other.transform.GetComponent<StatusManager>().DecreaseHp(damage);
        }
    }
}
