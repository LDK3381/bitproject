﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;      //씬 변환에 필요한 네임스페이스
using Photon.Pun;

public class PlayerControll : MonoBehaviourPun, IPunObservable
{
    public PhotonView PV;
    public GameObject obj;

    NoteTimingManager noteTimingManager;
    Ray forwardRay, LeftRay, BackwardRay, RightRay, UnderRay;

    public float Move = 0.375f;
    public float move_speed = 0.375f;    //이동 거리

    float rayLength = 0.25f;            //Ray와 장애물 간 판정거리

    RaycastHit hit;

    private Vector3 currPos;

    private void Start()
    {
        noteTimingManager = FindObjectOfType<NoteTimingManager>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            #region 장애물 판정 위한 Ray 생성
            forwardRay = new Ray(transform.position, transform.forward);
            LeftRay = new Ray(transform.position, -transform.right);
            BackwardRay = new Ray(transform.position, -transform.forward);
            RightRay = new Ray(transform.position, transform.right);
            UnderRay = new Ray(transform.position, -transform.up);

            Debug.DrawRay(forwardRay.origin, transform.forward, Color.red);
            Debug.DrawRay(LeftRay.origin, -transform.right, Color.red);
            Debug.DrawRay(BackwardRay.origin, -transform.forward, Color.red);
            Debug.DrawRay(RightRay.origin, transform.right, Color.red);
            Debug.DrawRay(UnderRay.origin, -transform.up, Color.red);
            #endregion

            PlayerMove();   //캐릭터 조작

            //if(Input.GetMouseButtonDown(0))
            //    obj.GetComponent<GunControll>().photonView.RPC("Fire", RpcTarget.All);
            //obj.GetComponent<GunControll>().FireRateCalc();
            obj.GetComponent<GunControll>().photonView.RPC("TryFire", RpcTarget.AllBuffered);
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, currPos, Time.deltaTime * 10.0f);
        }
    }

    //캐릭터 조작 함수(WASD)
    public void PlayerMove()
    {
        #region 칸 단위로 이동   
        //if (Under_ObstacleCheck())
        //{
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
        //}
        #endregion
    }

    #region WASD 작동여부 결정
    private void W_MoveCheck()
    {
        if (W_ObstacleCheck() == true)
        {
            if (noteTimingManager.CheckTiming())
            {
                //MoveDir : 캐릭터가 이동할 방향(이동 목표지점)
                Vector3 MoveDir_W = new Vector3(transform.position.x, transform.position.y, transform.position.z + Move);
                transform.position = Vector3.Slerp(transform.position, MoveDir_W, 1f);
            }
            else
                return;
        }
    }
    private void S_MoveCheck()
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
    private void A_MoveCheck()
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
    private void D_MoveCheck()
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
    #endregion

    #region 앞 혹은 옆에 장애물이 있을때 해당 방향으로의 움직임 봉쇄(4방향)
    public bool W_ObstacleCheck()
    {  
        //근처 장애물 여부 판단       
        if (Physics.Raycast(forwardRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" || hit.collider.tag == "Player")
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
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" || hit.collider.tag == "Player")
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
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" || hit.collider.tag == "Player")
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
            if (hit.collider.tag == "Wall" || hit.collider.tag == "BreakableWall" || hit.collider.tag == "Player")
            {
                return false;
            }
        }
        return true;
    }

    public bool Under_ObstacleCheck()
    {
        //바닥 여부 판단
        if (Physics.Raycast(UnderRay, out hit, rayLength))
        {
            if (hit.collider.tag != "Ground")
            {
                Debug.Log("Ground 없음");
                return false;
            }
        }
        return true;
    }
    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(this.transform.position);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
        }
    }
}
