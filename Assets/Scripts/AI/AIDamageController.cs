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
        timer = FindObjectOfType<AITimer>();
        spawn = FindObjectOfType<AISpawn>();
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
        timer.UpdateByPlBullet();
        Destroy(transform.parent.gameObject);   //ai 오브젝트 파괴
        spawn.DecreaseCount();                  //ai 카운트 1 감소
        Debug.Log("적 AI를 총으로 파괴");
    }

    //폭탄으로 적 1마리 없앨 때마다 제한시간 증가  
    public void AIDamagedByBomb()
    {
        timer.UpdateByPlBomb();
        Destroy(transform.parent.gameObject);   //ai 오브젝트 파괴
        spawn.DecreaseCount();                  //ai 카운트 1 감소
        Debug.Log("적 AI를 폭탄으로 파괴");
    }

    #endregion
}
