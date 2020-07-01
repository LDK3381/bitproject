using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public float rotSpeed;      //마우스 회전 속도
    Camera viewCamera;        

    void Start()
    {
        viewCamera = Camera.main;                       //메인 카메라
    }  

    //캐릭터가 향하는 방향을 마우스에 맞춰서
    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightPoint);  //해당 시점으로 마우스 이동
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
            Debug.DrawLine(ray.origin, point, Color.red);
            LookAt(point);                   //교차 지점이 캐릭터가 바라볼 시점이 됨.    
        }
    }

    //플레이어가 맵 밖으로 빠지면 익사 판정
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Map")
        {
            Debug.Log("플레이어가 익사했습니다.");
            gameObject.SetActive(false);
        }
    }
}
