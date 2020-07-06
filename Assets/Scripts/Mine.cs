using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] float verticalMove = 0f;            //수직 움직임
    [SerializeField] float horizontalMove = 0f;          //수직 움직임
    [Range(0,1)]
    [SerializeField] float moveSpeed = 0f;               //움직임 스피드
    

    [SerializeField] int hp = 0;                        //지뢰체력
    [SerializeField] int damage = 0;                    //지뢰데미지
    [SerializeField] GameObject go_EffectPrefab = null;    //지뢰이펙트

    Vector3 endPos1;            //목적지1
    Vector3 endPos2;            //목적지2
    Vector3 currentDestination; //현재 목적지

    void Start()
    {
        #region 목적지를 향해 이동
        Vector3 originPos = transform.position;
        endPos1.Set(originPos.x, originPos.y + verticalMove, originPos.z + horizontalMove);
        endPos2.Set(originPos.x, originPos.y - verticalMove, originPos.z - horizontalMove);
        currentDestination = endPos1;
        #endregion
    }

    void Update()
    {
        #region 매 프레임마다 목적지를 향해 이동
        //목적지에 최대한 근접한 상태에서 목적지 변경
        if((transform.position - endPos1).sqrMagnitude<=0.1f)
            currentDestination = endPos2;
        else if ((transform.position - endPos1).sqrMagnitude <= 0.1f)
            currentDestination = endPos1;

        transform.position = Vector3.Lerp(transform.position, currentDestination, moveSpeed);
        #endregion
    }

    //충돌
    void OnCollisionEnter(Collision other)
    {
        //플에이어(ChickenPrefab)와 충돌
        if (other.transform.name == "ChickenPrefab")
        {
            //플레이어에게 데미지
            other.transform.GetComponent<StatusManager>().DecreaseHp(damage);

            Explosion();
        }
    }

    //데미지
    public void Damaged(int _num)
    {
        hp -= _num;
        if (hp <= 0)
            Explosion();
    }

    //폭발
    void Explosion()
    {
        //이펙트 생성 및 마인파괴
        GameObject clone = Instantiate(go_EffectPrefab, transform.position, Quaternion.identity);
        Destroy(clone, 2f);
        Destroy(gameObject);
    }
}
