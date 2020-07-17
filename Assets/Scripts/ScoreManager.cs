using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int currentScore;                   // 현재 점수
    public static int extraScore;       // 아이템 점수
    float maxDistance;  // 플레이어가 이동한 최대 거리
    float originPosZ;   // 플레이어의 최초 위치의 Z값
    float originPosX;   // 플레이어의 최초 위치의 x값

    [SerializeField] Text txt_Score = null;

    void Update()
    {
        currentScore = extraScore;
        //점수 증가 표시
        txt_Score.text = currentScore.ToString();
    }
}
