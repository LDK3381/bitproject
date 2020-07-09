using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Score,
    NomalGun_Bullet,
}
public class Item : MonoBehaviour
{
    public ItemType itemType;   //아이템 유형

    public int itemScore;       //추가 점수
    public int itemBullet;      //추가 획득 총알

    void Update()
    {
        // 1초에 y 축을 90도씩 회전
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }
}
