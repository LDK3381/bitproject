using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEvent : MonoBehaviour
{

    private float Timer = 0f;        //대기시간
    private float BlastTimer = 1f;   //폭발시간

    public GameObject bomb;
    public GameObject explosionEffect;
    public float explosion_force = 1.0f;
    public float explosion_radius = 2.0f;           //폭발 반경

    WallEvent wall;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        NearbyBlast();
    }

    //폭탄이 xx와 충돌 시,
   private void NearbyBlast()
    {
        Timer += Time.deltaTime;

        //폭탄 던지면 1초 후에 폭발하도록  
        if (Timer > BlastTimer)
        {
            //폭탄의 일정 범위 내의 오브젝트들을 폭파시키기
            Collider[] collidersToDestroy = Physics.OverlapSphere(bomb.transform.position, explosion_radius);

            foreach (Collider nearbyObject in collidersToDestroy)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

                //폭탄에 피해받는 대상(플레이어, 부서지는 벽, 타일?)
                if (rb != null)
                {
                    rb.AddExplosionForce(explosion_force, bomb.transform.position, explosion_radius, 0.2f, ForceMode.Impulse);
                }
            }
        }
    }

    //폭발 이펙트 구현

}
