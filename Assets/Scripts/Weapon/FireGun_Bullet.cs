using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun_Bullet : MonoBehaviour
{

    //[Header("피격 효과음")]
    //[SerializeField] string sound_Effect = null;

    void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("캐릭터 충돌");
            other.transform.GetComponent<StatusManager>().MtDecreaseHp(1);
        }
        //폭탄 범위 내 벽이 있으면 벽 폭파
        if (other.transform.CompareTag("BreakableWall"))
        {
            Debug.Log("벽 충돌");
            other.transform.GetComponent<WallEvent>().Explode();
        }
        //샷건으로 적 피격시 파괴 이벤트
        if(other.transform.CompareTag("Enemy"))
        {
            Debug.Log("샷건으로 적 파괴");
            other.transform.GetComponent<AIDamageController>().AIDamagedByBullet();
        }
    }
}
