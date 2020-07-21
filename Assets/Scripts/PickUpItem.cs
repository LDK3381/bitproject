﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] Gun[] guns = null;
    const int NOMAL_GUN = 0;
    const int SHOT_GUN = 1;

    [SerializeField] private SgGunController theGC = null;
    [SerializeField] private SgShotGunController theSGC = null;
    [SerializeField] private BombSpawn theBS = null;

    // 아이템과 충돌
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();

            int extra = 0;

            // 점수 아이템을 획득했을 때
            if (item.itemType == ItemType.Score)
            {
                SoundManager.instance.PlaySE("Score");
                extra = item.itemScore;
                ScoreManager.extraScore += extra;
            }
            // 일반 총알을 획득했을 떄
            else if (item.itemType == ItemType.NomalGun_Bullet)
            {
                //SoundManager.instance.PlaySE("Bullet");
                extra = item.itemBullet;
                guns[NOMAL_GUN].bulletCount += extra;
                theGC.BulletUiSetting();
            }
            // 샷건 총알을 획득했을 때
            else if (item.itemType == ItemType.ShotGun_Bullet)
            {
                //SoundManager.instance.PlaySE("Bullet");
                extra = item.itemBullet;
                guns[SHOT_GUN].bulletCount += extra;
                theSGC.BulletUiSetting();
            }
            else if (item.itemType == ItemType.Bomb_Bullet)
            {
                //SoundManager.instance.PlaySE("Bullet");
                extra = item.itemBomb;
                theBS.BombCountUp(extra);
                theBS.BombUiSetting();
            }

            string message = "+" + extra;

            //FloatingTextManager.instance.CreateFloatingText(other.transform.position, message);

            Destroy(other.gameObject);
        }
    }
}
