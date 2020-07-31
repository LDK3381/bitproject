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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            //총으로 적 1마리 없앨 때마다 제한시간 5초 증가
            timer.UpdateByPlBullet();

            Destroy(transform.parent.gameObject);
            Debug.Log("적 AI를 총으로 파괴");
        }

        if(other.gameObject.tag == "Bomb")
        {
            //총으로 적 1마리 없앨 때마다 제한시간 5초 증가     
            timer.UpdateByPlBomb();

            Destroy(transform.parent.gameObject);
            Debug.Log("적 AI를 폭탄으로 파괴");
        }
    }
}
