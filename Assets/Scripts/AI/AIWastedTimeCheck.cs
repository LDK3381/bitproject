using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIWastedTimeCheck : MonoBehaviour
{
    AITimer timer;

    [Header("패배 시 발생 이벤트")]
    [SerializeField] GameObject losePanel = null;   //패배 화면
    [SerializeField] Text txt_wastedTime;   //버틴 시간 텍스트


    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<AITimer>();
    }

    // Update is called once per frame
    void Update()
    {
        AIGameOver();
    }

    //게임오버 시 패배 화면에 버틴시간 표시
    public void AIGameOver()
    {
        if (losePanel.activeSelf == true)
            txt_wastedTime.text = "버틴 시간 : " + timer.GetWastedTime().ToString("f0") + "초";
    }
}
