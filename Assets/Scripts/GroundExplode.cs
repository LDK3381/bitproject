﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundExplode : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bomb")
            Destroy(gameObject, 2);     //폭탄이 놓인 곳의 발판을 2초 후에 소멸
    }
}
