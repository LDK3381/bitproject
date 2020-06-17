using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;      //씬 변환에 필요한 네임스페이스

public class PlayerControll : MonoBehaviour
{

    public float move_speed;    //이동 거리
    public float knockbackPower;    //넉백 파워
    public float knockbackTime;     //넉백 지속시간
    private float knockbackCounter; 

    void Start()
    {
    }


    void Update()
    {
        //씬 변환 함수
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("AnotherScene");

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        PlayerMove();
    }

    //캐릭터 조작 함수(WASD)
    public void PlayerMove()
    {
        //if (Input.GetKeyDown(KeyCode.D))
        //    transform.Translate(Vector3.right * move_speed * Time.deltaTime);
        //else if (Input.GetKeyDown(KeyCode.A))
        //    transform.Translate(Vector3.left * move_speed * Time.deltaTime);
        //else if (Input.GetKeyDown(KeyCode.W))
        //    transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
        //else if (Input.GetKeyDown(KeyCode.S))
        //    transform.Translate(Vector3.back * move_speed * Time.deltaTime);       
        
        if(knockbackCounter <= 0)
        {
        if (Input.GetKeyDown(KeyCode.W))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.25f);
        else if (Input.GetKeyDown(KeyCode.S))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.25f);
        else if (Input.GetKeyDown(KeyCode.A))
            transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z);
        else if (Input.GetKeyDown(KeyCode.D))
            transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z);
        }
        else
        {
            knockbackCounter -= Time.deltaTime;
        }
    }


    //캐릭터가 향하는 방향을 마우스에 맞춰서
    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightPoint);
    }


    //캐릭터가 폭탄과 충돌시,
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            PlayerKnockback();
            //playerDamaged();
        }
    }
    
    //넉백 이벤트
    public void PlayerKnockback()
    {
        Vector3 knockoutDirection = gameObject.transform.position - transform.position;
        knockoutDirection = knockoutDirection.normalized;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(knockoutDirection * knockbackPower, ForceMode.Impulse);
    }
}
