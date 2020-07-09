using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] Gun[] guns;

    GunControllers theGC;

    void Start()
    {
        theGC = FindObjectOfType<GunControllers>();
    }

    const int NOMAL_GUN = 0;

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
                SoundManager.instance.PlaySE("Bullet");
                extra = item.itemBullet;
                guns[NOMAL_GUN].bulletCount += extra;
                theGC.BulletUiSetting();
            }

            string message = "+" + extra;

            //FloatingTextManager.instance.CreateFloatingText(other.transform.position, message);

            Destroy(other.gameObject);
        }
    }
}
