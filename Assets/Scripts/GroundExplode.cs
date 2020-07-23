using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundExplode : MonoBehaviour
{
    private Animation anima = null;

    void Start()
    {
        anima = GetComponent<Animation>();
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            Destroy(gameObject, 2);     //폭탄이 놓인 곳의 발판을 2초 후에 소멸

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BombCheck")
        {
            anima.Play("GroundCount");
        }
    }
}
