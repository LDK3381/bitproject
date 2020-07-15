using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel = null;       //메인화면
    [SerializeField] private GameObject playPanel = null;       //튜토리얼,싱글,멀티플레이 
    [SerializeField] private GameObject optionPanel = null;     //환경설정

    private void Start()
    {
        Debug.Log("메인화면");
        optionPanel.SetActive(false);
        playPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    #region 메인화면 기본옵션들
    //게임시작
    public void OnPlay()
    {
        Debug.Log("게임시작화면");
        mainPanel.SetActive(false);
        playPanel.SetActive(true);
    }
    //환경설정
    public void OnOption()
    {
        Debug.Log("환경설정");
        mainPanel.SetActive(false);
        optionPanel.SetActive(true);
    }
    //게임종료
    public void OnQuit()
    {
        Debug.Log("종료");
        Application.Quit();
    }
    //뒤로가기
    public void OnBack()
    {
        playPanel.SetActive(false);
        optionPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    #endregion

    #region 게임플레이 옵션들
    public void OnTutorial()
    {
        Debug.Log("튜토리얼 시작");
        playPanel.SetActive(false);
        SceneManager.LoadScene("PlayerScene");

    }
    public void OnSinglePlay()
    {
        Debug.Log("싱글플레이 시작");
        playPanel.SetActive(false);
        SceneManager.LoadScene("PlayerScene");
    }
    public void OnMultiPlay()
    {
        Debug.Log("멀티플레이 시작");
        playPanel.SetActive(false);
        SceneManager.LoadScene("MultiUI");
    }
    #endregion

}
