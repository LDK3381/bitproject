using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEvent : MonoBehaviour
{
    private float RayLength = 0.75f;

    Ray bomb_Ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BombDetection();
    }

    //폭탄의 충돌 범위 감지
    private void BombDetection()
    {
        //폭탄에 Ray(광선) 추가(y축 아랫방향)
        bomb_Ray = new Ray(transform.position, -transform.up);
        Debug.DrawRay(bomb_Ray.origin, -transform.up, Color.yellow);

        if (Physics.Raycast(bomb_Ray, out hit, RayLength))
        {
            //폭탄 바로 아래의 발판(Ground)을 2초 후에 파괴
            if (hit.collider.tag == "Ground")
            {
                Collision col = null;
                hit.transform.GetComponent<GroundExplode>().OnCollisionEnter(col);
            }

        }
    }
}
