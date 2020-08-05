using System.Collections;
using System.Collections.Generic;
using System.Linq;      //Enumerable.Range(1, 100) 사용목적
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    Ray forwardRay, leftRay, backwardRay, rightRay, underRay;
    RaycastHit hit = new RaycastHit();
    public GameObject aiPlayer;  //Ray가 나오는 위치(플레이어 프리펩)

    public float Move = 0.375f;       //AI의 이동거리
    float rayLength = 0.5f;          //Ray와 장애물 간 판정거리

    public void Start()
    {
        try
        {
            InvokeRepeating("AIMove", 0f, 0.5f);    //2초 후, 0.5초마다 AIMove() 실행(ai 이동)
        }
        catch
        {
            Debug.Log("AIController.AIStart Error");
        }
    }

    void Update()
    {
        RaySet();
    }

    //장애물 판정 위한 Ray 생성
    private void RaySet()
    {
        try
        {
            #region 장애물 판정 위한 Ray 생성
            forwardRay = new Ray(aiPlayer.transform.position, transform.forward);
            leftRay = new Ray(aiPlayer.transform.position, -transform.right);
            backwardRay = new Ray(aiPlayer.transform.position, -transform.forward);
            rightRay = new Ray(aiPlayer.transform.position, transform.right);
            underRay = new Ray(aiPlayer.transform.position, -transform.up);

            Debug.DrawRay(forwardRay.origin, transform.forward, Color.red);
            Debug.DrawRay(leftRay.origin, -transform.right, Color.red);
            Debug.DrawRay(backwardRay.origin, -transform.forward, Color.red);
            Debug.DrawRay(rightRay.origin, transform.right, Color.red);
            Debug.DrawRay(underRay.origin, -transform.up, Color.red);
            #endregion
        }
        catch
        {
            Debug.Log("AIController.RaySet Error");
        }
    }

    //AI 조작 함수(WASD)
    public void AIMove()
    {
        try
        {
            #region 칸 단위로 이동
            if (Under_ObstacleCheck())      //Ground 여부 판정.
            {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        if (W_ObstacleCheck() == false)
                            goto case 1;    //장애물 땜에 해당 방향으로 못가면? 다음 case문으로 이동
                        else
                            W_MoveCheck(); break;  //장애물이 없으면 해당 방향 이동 후, switch문 종료(break)
                    case 1:
                        if (S_ObstacleCheck() == false)
                            goto case 2;
                        else
                            S_MoveCheck(); break;
                    case 2:
                        if (A_ObstacleCheck() == false)
                            goto case 3;
                        else
                            A_MoveCheck(); break;
                    case 3:
                        if (D_ObstacleCheck() == false)
                            goto case 0;
                        else
                            D_MoveCheck(); break;
                    default: break;
                }

                KeepCenterPos();
            }
            #endregion
        }
        catch
        {
            Debug.Log("AIController.AIMove Error");
        }
    }

    #region WASD 작동여부 결정
    private void W_MoveCheck()
    {
        try
        {
            //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
            Vector3 MoveDir_W = new Vector3(transform.position.x, transform.position.y, transform.position.z + Move);
            transform.position = Vector3.Slerp(transform.position, MoveDir_W, 1f);
        }
        catch
        {
            Debug.Log("AIController.W_MoveCheck Error");
        }
    }
    private void S_MoveCheck()
    {
        try
        {
            //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
            Vector3 MoveDir_S = new Vector3(transform.position.x, transform.position.y, transform.position.z - Move);
            transform.position = Vector3.Slerp(transform.position, MoveDir_S, 1f);
        }
        catch
        {
            Debug.Log("AIController.S_MoveCheck Error");
        }
    }
    private void A_MoveCheck()
    {
        try
        {
            //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
            Vector3 MoveDir_A = new Vector3(transform.position.x - Move, transform.position.y, transform.position.z);
            transform.position = Vector3.Slerp(transform.position, MoveDir_A, 1f);
        }
        catch
        {
            Debug.Log("AIController.A_MoveCheck Error");
        }
    }
    private void D_MoveCheck()
    {
        try
        {
            //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
            Vector3 MoveDir_D = new Vector3(transform.position.x + Move, transform.position.y, transform.position.z);
            transform.position = Vector3.Slerp(transform.position, MoveDir_D, 1f);
        }
        catch
        {
            Debug.Log("AIController.D_MoveCheck Error");
        }
    }
    #endregion

    #region 앞 혹은 옆에 장애물이 있을때 해당 방향으로의 움직임 봉쇄(4방향)
    public bool W_ObstacleCheck()
    {
        try
        {
            //근처 장애물 여부 판단       
            if (Physics.Raycast(forwardRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                    hit.collider.tag == "Player" || hit.collider.tag == "Enemy" || hit.collider.tag == "CliffCollider")
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("AIController.W_ObstacleCheck Error");
            return true;
        }
    }

    public bool A_ObstacleCheck()
    {
        try
        {
            //근처 장애물 여부 판단 
            if (Physics.Raycast(leftRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                     hit.collider.tag == "Player" || hit.collider.tag == "Enemy" || hit.collider.tag == "CliffCollider")
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("AIController.A_ObstacleCheck Error");
            return true;
        }
    }

    public bool S_ObstacleCheck()
    {
        try
        {
            //근처 장애물 여부 판단 
            if (Physics.Raycast(backwardRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                    hit.collider.tag == "Player" || hit.collider.tag == "Enemy" || hit.collider.tag == "CliffCollider")
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("AIController.S_ObstacleCheck Error");
            return true;
        }
    }

    public bool D_ObstacleCheck()
    {
        try
        {
            //근처 장애물 여부 판단 
            if (Physics.Raycast(rightRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                    hit.collider.tag == "Player" || hit.collider.tag == "Enemy" || hit.collider.tag == "CliffCollider")
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("AIController.D_ObstacleCheck Error");
            return true;
        }
    }

    public bool Under_ObstacleCheck()
    {
        try
        {
            //바닥 여부 판단
            if (Physics.Raycast(underRay, out hit, rayLength))
            {
                if (hit.collider.tag != "Ground" && hit.collider.tag != "Bomb")
                {
                    Debug.Log("Ground 없음");
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("AIController.Under_ObstacleCheck Error");
            return true;
        }
    }
    #endregion

    //항상 타일 정중앙에 있도록 조정
    public void KeepCenterPos()
    {
        try
        {
            float posX, posZ;

            posX = (float)(transform.position.x % 0.375);
            posZ = (float)(transform.position.z % 0.375);

            if (posX != 0)
                posX = 0;
            if (posZ != 0)
                posZ = 0;
        }
        catch
        {
            Debug.Log("AIController.KeepCenterPos Error");
        }
    }
}
