using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] Gun[] guns = null;
    const int NOMAL_GUN = 0;
    const int SHOT_GUN  = 1;

    [SerializeField] private SgGunController     theGC  = null;
    [SerializeField] private SgShotGunController theSGC = null;
    [SerializeField] private BombSpawn           theBS  = null;

    SgItemSpawn spawn;

    void Start()
    {
        spawn = FindObjectOfType<SgItemSpawn>();
    }

    // 아이템과 충돌
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
                    case ItemType.Score:            GetScore(item, extra); break;
                    case ItemType.NomalGun_Bullet:  GetNomal(item, extra); break;
                    case ItemType.ShotGun_Bullet:   GetShot(item, extra);  break;
                    case ItemType.Bomb_Bullet:      GetBomb(item, extra);  break;
                    default:
                        break;
                }
                string message = "+" + extra;
                //FloatingTextManager.instance.CreateFloatingText(other.transform.position, message);
                spawn.InsertQueue(other.gameObject);    //아이템을 먹으면 큐에서 다시 비활성화 처리(Destroy X)
            }
        }
        catch
        {
            Debug.Log("PickUpItem.OnTriggerEnter Error");
        }
    }
    private void GetScore(Item item, int extra)
    {
        try
        {
            SoundManager.instance.PlaySE("Score");
            extra = item.itemScore;
            ScoreManager.extraScore += extra;
        }
        catch
        {
            Debug.Log("PickUpItem.GetScore Error");
        }
    }
    private void GetNomal(Item item, int extra)
    {
        try
        {
            //SoundManager.instance.PlaySE("Bullet");
            extra = item.itemBullet;
            guns[NOMAL_GUN].bulletCount += extra;
            theGC.BulletUiSetting();
        }
        catch
        {
            Debug.Log("PickUpItem.GetNomal Error");
        }
    }
    private void GetShot(Item item, int extra)
    {
        try
        {
            //SoundManager.instance.PlaySE("Bullet");
            extra = item.itemBullet;
            guns[SHOT_GUN].bulletCount += extra;
            theSGC.BulletUiSetting();
        }
        catch
        {
            Debug.Log("PickUpItem.GetShot Error");
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
            Debug.Log("PickUpItem.GetBomb Error");
        }
    }
}
