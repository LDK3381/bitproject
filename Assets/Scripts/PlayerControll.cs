using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;      //씬 변환에 필요한 네임스페이스

public class PlayerControll : MonoBehaviour
{
    NoteTimingManager _TimingManager;

    Ray forwardRay, LeftRay, BackwardRay, RightRay;

    public float Move = 0.375f;
    float rayLength = 0.375f;            //Ray와 장애물 간 판정거리

    RaycastHit hit = new RaycastHit();

    void Start()
    {
        _TimingManager = FindObjectOfType<NoteTimingManager>();
    }

    void Update()
    {
        #region 장애물 판정 위한 Ray 생성
        forwardRay = new Ray(transform.position, transform.forward);
        LeftRay = new Ray(transform.position, -transform.right);
        BackwardRay = new Ray(transform.position, -transform.forward);
        RightRay = new Ray(transform.position, transform.right);

        Debug.DrawRay(forwardRay.origin, transform.forward, Color.red);
        Debug.DrawRay(LeftRay.origin, -transform.right, Color.red);
        Debug.DrawRay(BackwardRay.origin, -transform.forward, Color.red);
        Debug.DrawRay(RightRay.origin, transform.right, Color.red);
        #endregion

        //씬 변환 함수(스페이스바)
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("AnotherScene");

        PlayerMove();
    }

    //캐릭터 조작 함수(WASD)
    public void PlayerMove()
    {
        #region 누른만큼 이동
        //if (Input.GetKeyDown(KeyCode.D))
        //    transform.Translate(Vector3.right * move_speed * Time.deltaTime);
        //else if (Input.GetKeyDown(KeyCode.A))
        //    transform.Translate(Vector3.left * move_speed * Time.deltaTime);
        //else if (Input.GetKeyDown(KeyCode.W))
        //    transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
        //else if (Input.GetKeyDown(KeyCode.S))
        //    transform.Translate(Vector3.back * move_speed * Time.deltaTime); 
        #endregion

        #region 칸 단위로 이동       
        if (Input.GetKeyDown(KeyCode.W))
        {
            W_MoveCheck();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            S_MoveCheck();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            A_MoveCheck();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            D_MoveCheck();
        }
        #endregion
    }

    #region WASD 작동여부 결정
    private void W_MoveCheck()
    {
        if (W_ObstacleCheck() == true)
        {
            //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
            Vector3 MoveDir_W = new Vector3(transform.position.x, transform.position.y, transform.position.z + Move);
            //transform.position = Vector3.Slerp(transform.position, MoveDir_W, 1f);

            iTween.MoveBy(gameObject, iTween.Hash("islocal", true, "y", 1f, "time", 0.7f/2, "easeType", iTween.EaseType.easeOutQuad));
            iTween.MoveBy(gameObject, iTween.Hash("islocal", true, "y", 1f, "time", 0.7f/2, "delay", 0.7f/2, "easeType", iTween.EaseType.easeInCubic));
            iTween.MoveTo(gameObject, iTween.Hash("islocal", true, "z", transform.position.z + Move, "time", 0.7f,
                                                    "easetype", iTween.EaseType.linear));
            _TimingManager.CheckTiming();           
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
            _TimingManager.CheckTiming();
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
            _TimingManager.CheckTiming();
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
            _TimingManager.CheckTiming();
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
        if (Physics.Raycast(LeftRay, out hit, rayLength))
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
        if (Physics.Raycast(BackwardRay, out hit, rayLength))
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
        if (Physics.Raycast(RightRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" ||
                hit.collider.tag == "Player")
            {
                return false;
            }
        }
        return true;
    }
    #endregion

}
