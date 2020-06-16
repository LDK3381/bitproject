﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControll))]
public class MouseRotate : MonoBehaviour
{
    public float rotSpeed;      //마우스 회전 속도

    Camera viewCamera;        
    PlayerControll controller;  


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerControll>();    //PlayerControll 클래스 내 LookAt 함수 사용 목적으로 가져옴.  
        viewCamera = Camera.main;                       //메인 카메라
    }  

    // Update is called once per frame
    void Update()
    {
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition); //Ray(광선)을 메인카메라에 맞춰서 발사
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);    //발판(Plane)
        float rayDistance;

        //만약 Ray(광선)이 발판(Plane)을 거리(rayDistance)에 맞춰 교차한다면,
        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);  //그 교차 지점을 point로 지정

            //Debug.DrawLine(ray.origin, point, Color.red);

            controller.LookAt(point);                   //교차 지점이 캐릭터가 바라볼 시점이 됨.
        }
    }
}