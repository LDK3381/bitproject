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
}
