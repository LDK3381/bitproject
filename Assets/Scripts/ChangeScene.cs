﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //씬 변환 함수
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("PlayerScene");
    }
}