using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel = null;       //메인화면
    [SerializeField] private GameObject playPanel = null;       //튜토리얼,싱글,멀티플레이
    [SerializeField] private GameObject sgMapPanel = null;      //싱글 맵 선택
    [SerializeField] private GameObject optionPanel = null;     //환경설정
    [SerializeField] private GameObject keyErrorPanel = null;    

    private void Start()
    {
        try
        {
            Debug.Log("메인화면");
            optionPanel.SetActive(false);
            playPanel.SetActive(false);
            keyErrorPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        catch
        {
            Debug.Log("UIManager.Start Error");
        }
    }

    #region 메인화면 기본옵션들
    //게임시작
    public void OnPlay()
    {
        try
        {
            Debug.Log("게임시작화면");
            mainPanel.SetActive(false);
            playPanel.SetActive(true);
        }
        catch
        {
            Debug.Log("UIManager.OnPlay Error");
        }
    }
    public void OnSinglePlay()
    {
        try
        {
            Debug.Log("싱글플레이 시작");
            playPanel.SetActive(false);
            sgMapPanel.SetActive(true);
        }
        catch
        {
            Debug.Log("UIManager.OnSinglePlay Error");
        }
    }
    //환경설정
    public void OnOption()
    {
        try
        {
            Debug.Log("환경설정");
            mainPanel.SetActive(false);
            optionPanel.SetActive(true);
        }
        catch
        {
            Debug.Log("UIManager.OnOption Error");
        }
    }
    //게임종료
    public void OnQuit()
    {
        try
        {
            Debug.Log("종료");
            Application.Quit();
        }
        catch
        {
            Debug.Log("UIManager.OnQuit");
        }
    }
    //뒤로가기
    public void OnBack()
    {
        try
        {
            playPanel.SetActive(false);
            sgMapPanel.SetActive(false);
            optionPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        catch
        {
            Debug.Log("UIManager.OnBack");
        }
    }
    #endregion

    #region 게임플레이 옵션들
    public void OnTutorial()
    {
        try
        {
            Debug.Log("튜토리얼 시작");
            playPanel.SetActive(false);
            SceneManager.LoadScene("PlayerScene");
        }
        catch
        {
            Debug.Log("UIManager.OnTutorial Error");
        }
    }
    public void OnSgMap1()
    {
        try
        {
            Debug.Log("SgMap1 시작");
            playPanel.SetActive(false);
            SceneManager.LoadScene("SgMap1");
        }
        catch
        {
            Debug.Log("UIManager.OnSgMap1 Error");
        }
    }
    public void OnSgMap2()
    {
        try
        {
            Debug.Log("SgMap2 시작");
            playPanel.SetActive(false);
            SceneManager.LoadScene("SgMap2");
        }
        catch
        {
            Debug.Log("UIManager.OnSgMap2 Error");
        }
    }
    public void OnMultiPlay()
    {
        try
        {
            Debug.Log("멀티플레이 시작");
            playPanel.SetActive(false);
            SceneManager.LoadScene("MultiUI");
        }
        catch
        {
            Debug.Log("UIManager.OnMultiPlay Error");
        }
    }
    #endregion

}
