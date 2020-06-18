using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEvent : MonoBehaviour
{
    [SerializeField] ParticleSystem ps_MuzzleFlash;     //적용할 파티클 효과

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //폭탄이 플레이어와 충돌 시,
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            explode();
        }
    }


    //폭탄이 폭발
    public void explode()
    {
        gameObject.SetActive(false);    //폭탄 소멸
        ps_MuzzleFlash.Play();
    }
}
