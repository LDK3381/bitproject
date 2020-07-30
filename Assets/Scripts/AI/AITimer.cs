using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AITimer : MonoBehaviour
{
    [Header("타이머 설정")]
    [SerializeField] Text txt_Timer = null;
    [SerializeField] float startTime = 90f;     //시작은 1:30
    [SerializeField] float currentTime = 0f;    //현재 시각
    [SerializeField] float wastedTime = 0f;    //소모된 시간

    private bool flag = true;
    StatusManager status;

    void Start()
    {
        currentTime = startTime;
        status = FindObjectOfType<StatusManager>();
    }

    void Update()
    {
        string minutes = ((int)currentTime / 60).ToString();
        string seconds = ((int)currentTime % 60).ToString();   //초 단위는 소수점 2자리까지만 표시
        txt_Timer.text = minutes + ":" + seconds;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; //1초씩 감소(타임어택)
        }

        if (currentTime <= 0 && flag == true)   //타이머가 다 지나면 게임종료
        {
            currentTime = 0;
            status.SgPlayerDead();
            flag = false;
        }
    }
}
