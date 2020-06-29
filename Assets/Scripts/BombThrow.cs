using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class BombThrow : MonoBehaviour
{
    public Transform throwPoint;
    public float throwForce = 2.0f;
    public GameObject bomb;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 왼쪽 버튼 누르는 동안 폭탄 던질 준비
        if (Input.GetMouseButton(0))
        {
        }

        //마우스 뗀 순간, 폭탄 투척
        if (Input.GetMouseButtonDown(0))
        {           
            ThrowAt();
        }
    }  

    private void ThrowAt()
    {
        //현재 무기가 폭탄일 때에만 투척하도록 제한
        if(weapon.activeSelf == true)
        {
            GameObject bombInstance = Instantiate(bomb, throwPoint.position, throwPoint.rotation);
            bombInstance.GetComponent<Rigidbody>().AddForce(throwPoint.forward * throwForce * 100);


        }
    }  
}
