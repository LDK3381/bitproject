using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;      //씬 변환에 필요한 네임스페이스

public class SgPlayerController : MonoBehaviour
{
    NoteTimingManager noteTimingManager;

    Ray forwardRay, leftRay, backwardRay, rightRay, underRay;

    public float Move = 0.375f;
    public float rayLength = 0.375f;            //Ray와 장애물 간 판정거리
    public GameObject playerRay;

    RaycastHit hit = new RaycastHit();

    void Start()
    {
        try
        {
            noteTimingManager = FindObjectOfType<NoteTimingManager>();
        }
        catch
        {
            Debug.Log("SgPlayerController.Start Error");
        }
    }

    void Update()
    {
        RaySet();
        PlayerMove();
    }
    private void RaySet()
    {
        try
        {
            #region 장애물 판정 위한 Ray 생성
            forwardRay = new Ray(playerRay.transform.position, transform.forward);
            leftRay = new Ray(playerRay.transform.position, -transform.right);
            backwardRay = new Ray(playerRay.transform.position, -transform.forward);
            rightRay = new Ray(playerRay.transform.position, transform.right);
            underRay = new Ray(playerRay.transform.position, -transform.up);

            Debug.DrawRay(forwardRay.origin, transform.forward, Color.red);
            Debug.DrawRay(leftRay.origin, -transform.right, Color.red);
            Debug.DrawRay(backwardRay.origin, -transform.forward, Color.red);
            Debug.DrawRay(rightRay.origin, transform.right, Color.red);
            Debug.DrawRay(underRay.origin, -transform.up, Color.red);
            #endregion
        }
        catch
        {
            Debug.Log("SgPlayerController.RaySet Error");
        }
    }

    //캐릭터 조작 함수(WASD)
    public void PlayerMove()
    {
        try
        {
            #region 칸 단위로 이동
            if (Under_ObstacleCheck()) //Ground 여부 판정.
            {
                if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Up"))))
                {
                    W_MoveCheck();
                }
                else if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Down"))))
                {
                    S_MoveCheck();
                }
                else if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Left"))))
                {
                    A_MoveCheck();
                }
                else if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Right"))))
                {
                    D_MoveCheck();
                }
            }
            #endregion
        }
        catch
        {
            Debug.Log("SgPlayerController.PlayerMove Error");
        }
    }

    #region WASD 작동여부 결정
    private void W_MoveCheck()
    {
        try
        {
            if (W_ObstacleCheck() == true)
            {
                if (noteTimingManager.CheckTiming())
                {
                    //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                    Vector3 MoveDir_W = new Vector3(transform.position.x, transform.position.y, transform.position.z + Move);
                    transform.position = Vector3.Slerp(transform.position, MoveDir_W, 1f);
                }
            }
            else
                return;
        }
        catch
        {
            Debug.Log("SgPlayerController.W_MoveCheck Error");
        }
    }
    private void S_MoveCheck()
    {
        try
        {
            if (S_ObstacleCheck() == true)
            {
                if (noteTimingManager.CheckTiming())
                {
                    //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                    Vector3 MoveDir_S = new Vector3(transform.position.x, transform.position.y, transform.position.z - Move);
                    transform.position = Vector3.Slerp(transform.position, MoveDir_S, 1f);
                }
            }
            else
                return;
        }
        catch
        {
            Debug.Log("SgPlayerController.S_MoveCheck Error");
        }
    }
    private void A_MoveCheck()
    {
        try
        {
            if (A_ObstacleCheck() == true)
            {
                if (noteTimingManager.CheckTiming())
                {
                    //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                    Vector3 MoveDir_A = new Vector3(transform.position.x - Move, transform.position.y, transform.position.z);
                    transform.position = Vector3.Slerp(transform.position, MoveDir_A, 1f);
                }
            }
            else
                return;
        }
        catch
        {
            Debug.Log("SgPlayerController.A_MoveCheck Error");
        }
    }
    private void D_MoveCheck()
    {
        try
        {
            if (D_ObstacleCheck() == true)
            {
                if (noteTimingManager.CheckTiming())
                {
                    //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                    Vector3 MoveDir_D = new Vector3(transform.position.x + Move, transform.position.y, transform.position.z);
                    transform.position = Vector3.Slerp(transform.position, MoveDir_D, 1f);
                }
            }
            else
                return;
        }
        catch
        {
            Debug.Log("SgPlayerController.D_MoveCheck Error");
        }
    }
    #endregion

    #region 앞 혹은 옆에 장애물이 있을때 해당 방향으로의 움직임 봉쇄(4방향)
    private bool W_ObstacleCheck()
    {
        try
        {
            //근처 장애물 여부 판단       
            if (Physics.Raycast(forwardRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                    hit.collider.tag == "Enemy")
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("SgPlayerController.W_ObstacleCheck Error");
            return true;
        }
    }
    private bool A_ObstacleCheck()
    {
        try
        {
            //근처 장애물 여부 판단 
            if (Physics.Raycast(leftRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                     hit.collider.tag == "Enemy")
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("SgPlayerController.A_ObstaclecCheck Error");
            return true;
        }
    }
    private bool S_ObstacleCheck()
    {
        try
        {
            //근처 장애물 여부 판단 
            if (Physics.Raycast(backwardRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                    hit.collider.tag == "Enemy")
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("SgPlayerController.S_ObstacleCheck Error");
            return true;
        }
    }
    private bool D_ObstacleCheck()
    {
        try
        {
            //근처 장애물 여부 판단 
            if (Physics.Raycast(rightRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                    hit.collider.tag == "Enemy")
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Debug.Log("SgPlayerController.D_ObstacleCheck Error");
            return true;
        }
    }
    private bool Under_ObstacleCheck()
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
            Debug.Log("SgPlayerController.Under_ObstacleCheck Error");
            return true;
        }
    }
    #endregion

}