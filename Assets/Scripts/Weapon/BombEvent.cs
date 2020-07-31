using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombEvent : MonoBehaviourPun
{
    private float obRayLength = 0.5f;
    private float waitTime = 0f;
    public float explosionTime = 2f;  //폭파하기까지 걸리는 시간

    Ray rightRay, leftRay, upRay, downRay, rightRay1, leftRay1, upRay1, downRay1;
    RaycastHit hit = new RaycastHit();

    [SerializeField] int damage = 0;

    public GameObject ps_BombExplode;     //폭탄 폭발 이펙트

    void Update()
    {
        BombToDetectOthers();
    }

    //폭탄의 충돌 범위 감지(주변 오브젝트에 데미지용)
    public void BombToDetectOthers()
    {
        try
        {
            #region 장애물 판정 위한 Ray 생성

            upRay = new Ray(transform.position, transform.forward);
            leftRay = new Ray(transform.position, -transform.right);
            downRay = new Ray(transform.position, -transform.forward);
            rightRay = new Ray(transform.position, transform.right);

            upRay1 = new Ray(transform.position, transform.forward + transform.right);
            leftRay1 = new Ray(transform.position, -transform.right + transform.forward);
            downRay1 = new Ray(transform.position, -transform.forward + transform.right);
            rightRay1 = new Ray(transform.position, -transform.right - transform.forward);

            //==============================================================================

            Debug.DrawRay(upRay.origin, transform.forward, Color.blue);
            Debug.DrawRay(leftRay.origin, -transform.right, Color.blue);
            Debug.DrawRay(downRay.origin, -transform.forward, Color.blue);
            Debug.DrawRay(rightRay.origin, transform.right, Color.blue);

            Debug.DrawRay(upRay1.origin, transform.forward + transform.right, Color.blue);
            Debug.DrawRay(leftRay1.origin, -transform.right + transform.forward, Color.blue);
            Debug.DrawRay(downRay1.origin, -transform.forward + transform.right, Color.blue);
            Debug.DrawRay(rightRay1.origin, -transform.right - transform.forward, Color.blue);
            #endregion

            BombCheck();
        }
        catch
        {
            Debug.Log("BombEvent.BombToDetectOthers Error");
        }
    }

    private void BombCheck()
    {
        #region 폭탄 범위 내 장애물 처리
        try
        {
            waitTime += Time.deltaTime;

            //2초 후에 발동
            if (waitTime > explosionTime)
            {
                GameObject effect = Instantiate(ps_BombExplode, transform.position, transform.rotation);

                //윗쪽 광선 범위에 장애물이 들어온 경우,
                if (Physics.Raycast(upRay, out hit, obRayLength))
                {
                    //폭탄 범위 내 벽이 있으면 벽 폭파
                    if (hit.collider.tag == "BreakableWall")
                    {
                        hit.transform.GetComponent<WallEvent>().explode();
                    }
                    //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                    if (hit.collider.tag == "Player")
                    {
                        CheckInRoom();
                    }
                }
                //아랫쪽 광선 범위에 장애물이 들어온 경우,
                if (Physics.Raycast(downRay, out hit, obRayLength))
                {
                    //폭탄 범위 내 벽이 있으면 벽 폭파
                    if (hit.collider.tag == "BreakableWall")
                    {
                        hit.transform.GetComponent<WallEvent>().explode();
                    }
                    //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                    if (hit.collider.tag == "Player")
                    {
                        CheckInRoom();
                    }
                }
                //왼쪽 광선 범위에 장애물이 들어온 경우,
                if (Physics.Raycast(leftRay, out hit, obRayLength))
                {
                    //폭탄 범위 내 벽이 있으면 벽 폭파
                    if (hit.collider.tag == "BreakableWall")
                    {
                        hit.transform.GetComponent<WallEvent>().explode();
                    }
                    //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                    if (hit.collider.tag == "Player")
                    {
                        CheckInRoom();
                    }
                }
                //오른쪽 광선 범위에 장애물이 들어온 경우,
                if (Physics.Raycast(rightRay, out hit, obRayLength))
                {
                    //폭탄 범위 내 벽이 있으면 벽 폭파
                    if (hit.collider.tag == "BreakableWall")
                    {
                        hit.transform.GetComponent<WallEvent>().explode();
                    }
                    //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                    if (hit.collider.tag == "Player")
                    {
                        CheckInRoom();
                    }
                }

                //대각선 광선 범위에 장애물이 들어온 경우,
                if (Physics.Raycast(upRay1, out hit, obRayLength))
                {
                    //폭탄 범위 내 벽이 있으면 벽 폭파
                    if (hit.collider.tag == "BreakableWall")
                    {
                        hit.transform.GetComponent<WallEvent>().explode();
                    }
                    //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                    if (hit.collider.tag == "Player")
                    {
                        CheckInRoom();
                    }
                }
                if (Physics.Raycast(downRay1, out hit, obRayLength))
                {
                    //폭탄 범위 내 벽이 있으면 벽 폭파
                    if (hit.collider.tag == "BreakableWall")
                    {
                        hit.transform.GetComponent<WallEvent>().explode();
                    }
                    //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                    if (hit.collider.tag == "Player")
                    {
                        CheckInRoom();
                    }
                }
                if (Physics.Raycast(leftRay1, out hit, obRayLength))
                {
                    //폭탄 범위 내 벽이 있으면 벽 폭파
                    if (hit.collider.tag == "BreakableWall")
                    {
                        hit.transform.GetComponent<WallEvent>().explode();
                    }
                    //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                    if (hit.collider.tag == "Player")
                    {
                        CheckInRoom();
                    }
                }
                if (Physics.Raycast(rightRay1, out hit, obRayLength))
                {
                    //폭탄 범위 내 벽이 있으면 벽 폭파
                    if (hit.collider.tag == "BreakableWall")
                    {
                        hit.transform.GetComponent<WallEvent>().explode();
                    }
                    //폭탄 범위 내 캐릭터가 있으면 캐릭터 데미지 주기
                    if (hit.collider.tag == "Player")
                    {
                        CheckInRoom();
                    }
                }

                Destroy(effect, 2);     //이펙트 2초후에 소멸
            }
            #endregion
        }
        catch
        {
            Debug.Log("BombEvent.BombCheck Error");
        }
    }

    //싱글&멀티 확인용
    private void CheckInRoom()
    {
        try
        {
            if (PhotonNetwork.InRoom)
            {
                hit.transform.GetComponent<StatusManager>().MtDecreaseHp(damage);
            }
            else
            {
                hit.transform.GetComponent<StatusManager>().SgDecreaseHp(damage);
            }
        }
        catch
        {
            Debug.Log("CheckInRoom");
        }
    }
}
