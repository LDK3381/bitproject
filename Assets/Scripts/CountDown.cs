using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public GameObject numImg1 = null;
    public GameObject numImg2 = null;
    public GameObject numImg3 = null;
    public GameObject startImg = null;

    private int timer = 0;

    void Start()
    {
        //시작할 때 카운트 초기화
        timer = 0;

        numImg1.SetActive(false);
        numImg2.SetActive(false);
        numImg3.SetActive(false);
        startImg.SetActive(false);
    }

    void Update()
    {
        //게임 시작시 정지
        if(timer == 0)
        {
            Time.timeScale = 0.0f;
        }
        //타이머가 90보다 작거나 같으면 증가
        if(timer <= 90)
        {
            timer++;
            NumImg3();
            NumImg2();
            NumImg1();
            StartImg();
        }
    }
    private void NumImg3()
    {
        if (timer < 30)
            numImg3.SetActive(true);
    }
    private void NumImg2()
    {
        if (timer > 30)
        {
            numImg3.SetActive(false);
            numImg2.SetActive(true);
        }
    }
    private void NumImg1()
    {
        if (timer > 60)
        {
            numImg2.SetActive(false);
            numImg1.SetActive(true);
        }
    }
    private void StartImg()
    {
        if (timer > 90)
        {
            numImg1.SetActive(false);
            startImg.SetActive(true);
            StartCoroutine(this.LoadingEnd());
            Time.timeScale = 1.0f;  //시작
        }
    }

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        startImg.SetActive(false);
    }
}
