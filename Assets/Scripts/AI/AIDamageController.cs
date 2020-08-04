using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIDamageController : MonoBehaviour
{
    AITimer timer;
    AISpawn spawn;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            timer = FindObjectOfType<AITimer>();
            spawn = FindObjectOfType<AISpawn>();
        }
        catch
        {
            Debug.Log("AIDamageController.Start Error");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            AIDamagedByBullet();
        }
    }

    #region 외부에다 쓸 AI 피격 이벤트
    //총으로 적 1마리 없앨 때마다 제한시간 증가  
    public void AIDamagedByBullet()
    {
        try
        {
            timer.UpdateByPlBullet();                   //제한시간 증가(총으로 피격 성공 시)
            spawn.AIDie(transform.parent.gameObject);   //쓰러뜨린 ai를 다시 큐에다 비활성화 처리
            spawn.DecreaseCount();                      //ai 카운트 1 감소
            Debug.Log("적 AI를 총으로 파괴");
        }
        catch
        {
            Debug.Log("AIDamageController.AIDamagedByBullet Error");
        }
    }

    //폭탄으로 적 1마리 없앨 때마다 제한시간 증가  
    public void AIDamagedByBomb()
    {
        try
        {
            timer.UpdateByPlBomb();                     //제한시간 증가(폭탄으로 피격 성공 시)
            spawn.AIDie(transform.parent.gameObject);   //쓰러뜨린 ai를 다시 큐에다 비활성화 처리
            spawn.DecreaseCount();                      //ai 카운트 1 감소
            Debug.Log("적 AI를 폭탄으로 파괴");
        }
        catch
        {
            Debug.Log("AIDamageController.AIDamagedByBomb Error");
        }
    }

    #endregion
}
