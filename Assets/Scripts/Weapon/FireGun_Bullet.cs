using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun_Bullet : MonoBehaviour
{
    [Header("총알 데미지")]
    [SerializeField] int damage = 0;

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
    }
}
