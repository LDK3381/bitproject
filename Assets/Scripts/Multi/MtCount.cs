using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MtCount : MonoBehaviour
{
    public Text timeCount = null;
    public float timeCost = 10.0f;
    public bool flag = false;

    void Update()
    {
        CountDown();
    }
    public void CountDown()
    {
        if (timeCost > 0)
        {
            timeCost -= Time.deltaTime;
            flag = true;
        }
        if (timeCost <= 0)
        {
            timeCost = 0f;
            flag = false;
        }
        timeCount.text = "남은시간 : " + timeCost.ToString("N0");
    }
}
