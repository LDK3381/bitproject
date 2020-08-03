using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtPickUpItem : MonoBehaviour
{
    [SerializeField] Gun[] guns = null;
    [SerializeField] private MtGunController     theHGC = null;
    [SerializeField] private MtShotGunController theSGC = null;
    [SerializeField] private MtBombSpawn         theBS  = null;

    const int NOMAL_GUN = 0;
    const int SHOT_GUN = 1;

    void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.transform.CompareTag("Item"))
            {
                Item item = other.GetComponent<Item>();

                int extra = 0;

                switch (item.itemType)
                {
                    case ItemType.NomalGun_Bullet: GetNomal(item, extra); break;
                    case ItemType.ShotGun_Bullet:  GetShot(item, extra);  break;
                    case ItemType.Bomb_Bullet:     GetBomb(item, extra);  break;
                    default:
                        break;
                }
                string message = "+" + extra;
                //FloatingTextManager.instance.CreateFloatingText(other.transform.position, message);
                Destroy(other.gameObject);
            }
        }
        catch
        {
            Debug.Log("MtPickUpItem.OnTriggerEnter Error");
        }
    }

    private void GetNomal(Item item, int extra)
    {
        try
        {
            SoundManager.instance.PlaySE("Bullet");
            extra = item.itemBullet;
            guns[NOMAL_GUN].bulletCount += extra;
            theHGC.BulletUiSetting();
        }
        catch
        {
            Debug.Log("MtPickUpItem.GetNomal Error");
        }
    }
    private void GetShot(Item item, int extra)
    {
        try
        {
            SoundManager.instance.PlaySE("Bullet");
            extra = item.itemBullet;
            guns[SHOT_GUN].bulletCount += extra;
            theSGC.BulletUiSetting();
        }
        catch
        {
            Debug.Log("MtPickUpItem.GetShot Error");
        }
    }
    private void GetBomb(Item item, int extra)
    {
        try
        {
            //SoundManager.instance.PlaySE("Bullet");
            extra = item.itemBomb;
            theBS.BombCountUp(extra);
            theBS.BombUiSetting();
        }
        catch
        {
            Debug.Log("MtPickUpItem.GetBomb Error");
        }
    }
}
