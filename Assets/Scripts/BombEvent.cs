using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEvent : MonoBehaviour
{
    //private float GroundRayLength = 0.25f;
    private float ObRayLength = 0.5f;
    private float waitTime = 0f;
    public float explosionTime = 2f;  //폭파하기까지 걸리는 시간

    Ray rightRay, leftRay, upRay, downRay;
    RaycastHit hit = new RaycastHit();
    //Collision col = null;

    [SerializeField] int damage = 0;
    [SerializeField] float force = 0f;

    public GameObject ps_BombExplode;     //폭탄 폭발 이펙트

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        BombToDetectOthers();
    }

    //폭탄의 충돌 범위 감지(발판 파괴용)
    //private void BombToDetectGround()
    //{
    //    //폭탄에 Ray(광선) 추가(y축 아랫방향)
    //    bomb_Ray = new Ray(transform.position, -transform.up);
    //    Debug.DrawRay(bomb_Ray.origin, -transform.up, Color.yellow);

    //    if (Physics.Raycast(bomb_Ray, out hit, GroundRayLength))
    //    {
    //        //폭탄 바로 아래의 발판(Ground)을 2초 후에 파괴
    //        if (hit.collider.tag == "Ground")
    //        {                
    //            hit.transform.GetComponent<GroundExplode>().OnCollisionEnter(col);
    //        }
    //    }
    //}

    //폭탄의 충돌 범위 감지(주변 오브젝트에 데미지용)
    public void BombToDetectOthers()
    {
        #region 장애물 판정 위한 Ray 생성
        upRay = new Ray(transform.position, transform.forward);
        leftRay = new Ray(transform.position, -transform.right);
        downRay = new Ray(transform.position, -transform.forward);
        rightRay = new Ray(transform.position, transform.right);

        Debug.DrawRay(upRay.origin, transform.forward, Color.blue);
        Debug.DrawRay(leftRay.origin, -transform.right, Color.blue);
        Debug.DrawRay(downRay.origin, -transform.forward, Color.blue);
        Debug.DrawRay(rightRay.origin, transform.right, Color.blue);
        #endregion

        #region 폭탄 범위 내 장애물 처리
        waitTime += Time.deltaTime;

        //2초 후에 발동
        if(waitTime > explosionTime)
        {
            GameObject effect = Instantiate(ps_BombExplode, transform.position, transform.rotation);

            //윗쪽 광선 범위에 장애물이 들어온 경우,
            if (Physics.Raycast(upRay, out hit, ObRayLength))
            {
                //폭탄 범위 내 벽이 있으면 벽 폭파
                if(hit.collider.tag == "BreakableWall")
                {
                    hit.transform.GetComponent<WallEvent>().explode();
                }
                //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                if (hit.collider.tag == "Player")
                {
                    hit.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);
                    hit.transform.GetComponent<StatusManager>().DecreaseHp(damage);                   
                }
            }
            //아랫쪽 광선 범위에 장애물이 들어온 경우,
            if (Physics.Raycast(downRay, out hit, ObRayLength))
            {
                //폭탄 범위 내 벽이 있으면 벽 폭파
                if (hit.collider.tag == "BreakableWall")
                {
                    hit.transform.GetComponent<WallEvent>().explode();
                }
                //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                if (hit.collider.tag == "Player")
                {
                    hit.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);
                    hit.transform.GetComponent<StatusManager>().DecreaseHp(damage);
                }
            }
            //왼쪽 광선 범위에 장애물이 들어온 경우,
            if (Physics.Raycast(leftRay, out hit, ObRayLength))
            {
                //폭탄 범위 내 벽이 있으면 벽 폭파
                if (hit.collider.tag == "BreakableWall")
                {
                    hit.transform.GetComponent<WallEvent>().explode();
                }
                //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                if (hit.collider.tag == "Player")
                {
                    hit.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);
                    hit.transform.GetComponent<StatusManager>().DecreaseHp(damage);
                }
            }
            //오른쪽 광선 범위에 장애물이 들어온 경우,
            if (Physics.Raycast(rightRay, out hit, ObRayLength))
            {
                //폭탄 범위 내 벽이 있으면 벽 폭파
                if (hit.collider.tag == "BreakableWall")
                {
                    hit.transform.GetComponent<WallEvent>().explode();
                }
                //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                if (hit.collider.tag == "Player")
                {
                    hit.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);
                    hit.transform.GetComponent<StatusManager>().DecreaseHp(damage);
                }
            }

            Destroy(effect, 2);     //이펙트 2초후에 소멸
        }
        #endregion
    }

    ////폭탄 설치된 자리에 캐릭터가 있어도 데미지 받음.
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        //hit.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);
    //        hit.transform.GetComponent<StatusManager>().DecreaseHp(damage);
    //    }         
    //}
}
