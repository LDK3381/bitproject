using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SgPlayerWin : MonoBehaviour
{
    [SerializeField] SgPauseManager sealKey = null;     //패배나 승리 시 플레이어 움직임 제한

    [SerializeField] GameObject winPanel = null;    //승리 패널 불러오기 위해 필요 

    [SerializeField] Text winScore = null;   //승리시 필요한 점수 텍스트

    void Update()
    {
        //싱글모드 플레이어 승리(점수100점 획득)
        SgWin();
    }

    // 플레이어 승리
    private void SgWin()
    {
        if (winScore.text == "100")
        {
            //키보드, 마우스 잠금
            sealKey.SealKey();

            //winPanel 실행
            winPanel.SetActive(true);
        }
    }
}
