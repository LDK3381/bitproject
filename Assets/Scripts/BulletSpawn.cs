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
}
