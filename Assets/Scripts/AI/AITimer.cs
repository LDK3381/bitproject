using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AITimer : MonoBehaviour
{
    [Header("타이머 설정")]
    [SerializeField] Text txt_Timer = null;
    [SerializeField] float startTime = 90f;         //시작은 1:30
    [SerializeField] float currentTime = 0f;        //현재 시간
    [SerializeField] float wastedTime = 0;          //버틴 시간
    [SerializeField] GameObject losePanel = null;   //패배 화면

    private bool flag = true;
    private float minutes, seconds;

    StatusManager status;

    void Start()
    {
        currentTime = startTime;
        status = FindObjectOfType<StatusManager>();
    }

    void Update()
    {
        try
        {
            UpdateCurrentTime(currentTime);

            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;      //제한시간 1초씩 감소
                wastedTime += Time.deltaTime;       //버틴 시간 1초씩 증가
            }

            if (currentTime <= 0 && flag == true)   //타이머가 다 지나면 게임종료
            {
                currentTime = 0;
                status.SgPlayerDead();
                flag = false;
            }
        }
        catch
        {
            Debug.Log("AITimer.Update Error");
        }
    }

    //현재 제한시간 정보 갱신
    public void UpdateCurrentTime(float curTime)
    {
        try
        {
            minutes = Mathf.FloorToInt(curTime / 60);
            seconds = Mathf.FloorToInt(curTime % 60);
            txt_Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        catch
        {
            Debug.Log("AITimer.UpdateCurrentTime Error");
        }
    }

    #region 특정 이벤트로 인한 제한시간 변화
    //총으로 ai 파괴할때 
    public void UpdateByPlBullet()
    {
        try
        {
            currentTime += 5f;
            UpdateCurrentTime(currentTime);
        }
        catch
        {
            Debug.Log("AITimer.UpdateByPlBullet Error");
        }
    }

    //폭탄으로 ai 파괴할 때 
    public void UpdateByPlBomb()
    {
        try
        {
            currentTime += 10f;
            UpdateCurrentTime(currentTime);
        }
        catch
        {
            Debug.Log("AITimer.UpdateByPlBomb Error");
        }
    }

    //적의 총으로 데미지 입을 때 
    public void UpdateByAIBullet()
    {
        try
        {
            currentTime -= 5f;
            UpdateCurrentTime(currentTime);
        }
        catch
        {
            Debug.Log("AITimer.UpdateByAIBullet Error");
        }
    }

    //적의 폭탄으로 데미지 입을 때 
    public void UpdateByAIBomb()
    {
        try
        {
            currentTime -= 10f;
            UpdateCurrentTime(currentTime);
        }
        catch
        {
            Debug.Log("AITimer.UpdateByAIBomb Error");
        }
    }
    #endregion

    #region 외부에다 쓸 시간값 가져오는 함수들
    //외부에다 쓸 버틴 시간값
    public float GetWastedTime()
    {
        try
        {
            if (losePanel.activeSelf == true)
                return wastedTime;

            return wastedTime;
        }
        catch
        {
            Debug.Log("AITimer.GetWastedTime Error");
            return 0f;
        }
    }

    //외부에다 쓸 현재 시간값
    public float GetCurrentTime()
    {
        try
        {
            return currentTime;
        }
        catch
        {
            Debug.Log("AITimer.GetCurrentTime Error");
            return 0f;
        }
    }
    #endregion

}
