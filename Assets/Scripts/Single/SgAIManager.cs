using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SgAIManager : MonoBehaviour
{
    NoteTimingManager noteTimingManager;

    Ray forwardRay, leftRay, backwardRay, rightRay, underRay;

    public float Move = 0.375f;
    float rayLength = 0.375f;            //Ray와 장애물 간 판정거리

    RaycastHit hit = new RaycastHit();

    void Start()
    {
        noteTimingManager = FindObjectOfType<NoteTimingManager>();
    }

    void Update()
    {
        #region 장애물 판정 위한 Ray 생성
        forwardRay = new Ray(transform.position, transform.forward);
        leftRay = new Ray(transform.position, -transform.right);
        backwardRay = new Ray(transform.position, -transform.forward);
        rightRay = new Ray(transform.position, transform.right);
        underRay = new Ray(transform.position, -transform.up);

        Debug.DrawRay(forwardRay.origin, transform.forward, Color.red);
        Debug.DrawRay(leftRay.origin, -transform.right, Color.red);
        Debug.DrawRay(backwardRay.origin, -transform.forward, Color.red);
        Debug.DrawRay(rightRay.origin, transform.right, Color.red);
        Debug.DrawRay(underRay.origin, -transform.up, Color.red);
        #endregion
    }

    public void AIMove()
    {
        int random = Random.Range(0, 4);

        if (Under_ObstacleCheck()) //Ground 여부 판정.
        {
            switch (random)
            {
                case 0:
                    W_MoveCheck();
                    break;
                case 1:
                    S_MoveCheck();
                    break;
                case 2:
                    A_MoveCheck();
                    break;
                case 3:
                    D_MoveCheck();
                    break;
                default:
                    break;
            }
        }
    }

    #region WASD 작동여부 결정
    private void W_MoveCheck()
    {
        if (W_ObstacleCheck() == true)
        {
                //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                Vector3 MoveDir_W = new Vector3(transform.position.x, transform.position.y, transform.position.z + Move);
                transform.position = Vector3.Slerp(transform.position, MoveDir_W, 1f);
        }
        else
            return;
    }
    private void S_MoveCheck()
    {
        if (S_ObstacleCheck() == true)
        {
                //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                Vector3 MoveDir_S = new Vector3(transform.position.x, transform.position.y, transform.position.z - Move);
                transform.position = Vector3.Slerp(transform.position, MoveDir_S, 1f);
        }
        else
            return;
    }
    private void A_MoveCheck()
    {
        if (A_ObstacleCheck() == true)
        {
                //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                Vector3 MoveDir_A = new Vector3(transform.position.x - Move, transform.position.y, transform.position.z);
                transform.position = Vector3.Slerp(transform.position, MoveDir_A, 1f);
        }
        else
            return;
    }
    private void D_MoveCheck()
    {
        if (D_ObstacleCheck() == true)
        {
                //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                Vector3 MoveDir_D = new Vector3(transform.position.x + Move, transform.position.y, transform.position.z);
                transform.position = Vector3.Slerp(transform.position, MoveDir_D, 1f);
        }
        else
            return;
    }
    #endregion

    #region 앞 혹은 옆에 장애물이 있을때 해당 방향으로의 움직임 봉쇄(4방향)
    public bool W_ObstacleCheck()
    {
        //근처 장애물 여부 판단       
        if (Physics.Raycast(forwardRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                hit.collider.tag == "Player")
            {
                return false;
            }
        }
        return true;
    }
    public bool A_ObstacleCheck()
    {
        //근처 장애물 여부 판단 
        if (Physics.Raycast(leftRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                 hit.collider.tag == "Player")
            {
                return false;
            }
        }
        return true;
    }
    public bool S_ObstacleCheck()
    {
        //근처 장애물 여부 판단 
        if (Physics.Raycast(backwardRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                hit.collider.tag == "Player")
            {
                return false;
            }
        }
        return true;
    }
    public bool D_ObstacleCheck()
    {
        //근처 장애물 여부 판단 
        if (Physics.Raycast(rightRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                hit.collider.tag == "Player")
            {
                return false;
            }
        }
        return true;
    }
    public bool Under_ObstacleCheck()
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

    #endregion
}
