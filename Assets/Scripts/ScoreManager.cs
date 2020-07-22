using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int currentScore;                   // 현재 점수
    public static int extraScore;       // 아이템 점수

    [SerializeField] Text txt_Score = null;

    void Update()
    {
        currentScore = extraScore;
        //점수 증가 표시
        txt_Score.text = currentScore.ToString();
    }
}
