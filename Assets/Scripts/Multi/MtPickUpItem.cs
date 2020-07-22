using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtPickUpItem : MonoBehaviour
{
    [SerializeField] Gun[] guns = null;

    public MtGunController theGC;

    const int NOMAL_GUN = 0;
    const int SHOT_GUN = 1;
    // 아이템과 충돌
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();

            int extra = 0;

            // 일반 총알을 획득했을 떄
            if (item.itemType == ItemType.NomalGun_Bullet)
            {
                SoundManager.instance.PlaySE("Bullet");
                extra = item.itemBullet;
                guns[NOMAL_GUN].bulletCount += extra;
                theGC.BulletUiSetting();
            }
            else if (item.itemType == ItemType.ShotGun_Bullet)
            {
                SoundManager.instance.PlaySE("Bullet");
                extra = item.itemBullet;
                guns[SHOT_GUN].bulletCount += extra;
                theGC.BulletUiSetting();
            }

            string message = "+" + extra;

            //FloatingTextManager.instance.CreateFloatingText(other.transform.position, message);

            Destroy(other.gameObject);
        }
    }
}
