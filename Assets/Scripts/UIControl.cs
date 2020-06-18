using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject mainpanel;
    [SerializeField] private GameObject signpanel;
    [SerializeField] private GameObject playpanel;
    [SerializeField] private GameObject gamemodepanel;
    [SerializeField] private GameObject noticeepanel;

    public void OnMain()
    {
        Debug.Log("메인화면");
        signpanel.SetActive(false);
        mainpanel.SetActive(true);
    }
    public void OnQuit()
    {
        Debug.Log("종료");
        Application.Quit();
    }
    public void OnSign()
    {
        Debug.Log("회원가입");
        mainpanel.SetActive(false);
        signpanel.SetActive(true);
    }
    public void OnPlay()
    {
        Debug.Log("플레이");
        mainpanel.SetActive(false);
        playpanel.SetActive(true);
    }
    public void OnGameMode()
    {
        Debug.Log("게임 모드");
        mainpanel.SetActive(false);
        gamemodepanel.SetActive(true);
    }
    public void dfsdfsd()
    {
        noticeepanel.SetActive(true);
        StartCoroutine("timer");
    }
    public void OnBack()
    {
        signpanel.SetActive(false);
        playpanel.SetActive(false);
        gamemodepanel.SetActive(false);
        noticeepanel.SetActive(false);
        mainpanel.SetActive(true);
    }


    IEnumerator timer()
    {
        yield return new WaitForSeconds(1.5f);
        noticeepanel.SetActive(false);
    }
}
