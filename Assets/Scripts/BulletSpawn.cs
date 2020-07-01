using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public float delay = 0.5f;      //총알 삭제 시간

    void Update()
    {
        //발사 후 일정 시간 지나면 총알 삭제 처리
        delay -= Time.deltaTime;
        if (delay <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //총알이 벽에 부딪치면 바로 소멸
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "BreakableWall")
            Destroy(gameObject);
    }
}
