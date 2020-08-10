using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SgCountDown : MonoBehaviour
{
    [SerializeField] private GameObject numImg1     = null;
    [SerializeField] private GameObject numImg2     = null;
    [SerializeField] private GameObject numImg3     = null;
    [SerializeField] private GameObject startImg    = null;
    [SerializeField] private SgPauseManager sealKey = null;
    [SerializeField] private GameObject pauseBtn    = null;
    private int timer = 0;

    void Start()
    {
        try
        {
            //시작할 때 카운트 초기화
            timer = 0;
            sealKey.SealKey();
            numImg1.SetActive(false);
            numImg2.SetActive(false);
            numImg3.SetActive(false);
            pauseBtn.SetActive(false);
            startImg.SetActive(false);
        }
        catch
        {
            Debug.Log("CountDown.Start Error");
        }
    }

    void Update()
    {
        try
        {
            //게임 시작시 정지
            if (timer == 0)
            {
                Time.timeScale = 0.0f;
            }
            //타이머가 90보다 작거나 같으면 증가
            if (timer <= 90)
            {
                timer++;
                NumImg3();
                NumImg2();
                NumImg1();
                StartImg();
            }
        }
        catch
        {
            Debug.Log("CountDown.Update Error");
        }
    }
    private void NumImg3()
    {
        try
        {
            if (timer < 30)
                numImg3.SetActive(true);
        }
        catch
        {
            Debug.Log("CountDown.NumImg3 Error");
        }
    }
    private void NumImg2()
    {
        try
        {
            if (timer > 30)
            {
                numImg3.SetActive(false);
                numImg2.SetActive(true);
            }
        }
        catch
        {
            Debug.Log("CountDown.NumImg2 Error");
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
        try
        {
            if (timer > 90)
            {
                numImg1.SetActive(false);
                startImg.SetActive(true);
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f;  //시작
            }
        }
        catch
        {
            Debug.Log("CountDown.StartImg Error");
        }
    }

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        startImg.SetActive(false);
        pauseBtn.SetActive(true);
        sealKey.UnSealKey();
    }
}
