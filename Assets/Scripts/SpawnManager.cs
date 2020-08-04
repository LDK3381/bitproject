using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviour
{ 
    public GameObject   inCanvace;    //게임내 켐퍼스
    public GameObject   Choice;       //캐릭터 선택 UI
    public GameObject[] Point;        //캐릭터 스폰 위치
    public MtCount      mtCount;      //캐릭터 선택 제한 시간
    public GameObject   WinPanel;     //승리 판넬
    public GameObject   LosePanel;    //패배 판넬

    private string nick;            //선택한 캐릭터 이름
    private GameObject FixedPosition;  //고정된 위치
    private bool isCheck;           //시간 내에 캐릭터를 선택하였는가?

    private void Start()
    {
        try
        {
            isCheck = false;
            inCanvace.SetActive(false);
            WinPanel.SetActive(false);
            LosePanel.SetActive(false);
        }
        catch
        {
            Debug.Log("SpawnManager.Start Error");
        }
    }

    private void Update()
    {
        NotChoiceCreate();
    }

    #region 플레이어 생성

    private void NotChoiceCreate()  //버튼 선택 안하면.
    {
        try
        {
            if (mtCount.flag == false && isCheck == false)
            {
                RandomCreate();
                Destroy(gameObject);    //무한 스폰 방지
            }
        }
        catch
        {
            Debug.Log("SpawnManager.NotChoiceCreate Error");
        }
    }

    public void ChoiceCreate(string Nickname)   //버튼 선택시.
    {
        try
        {
            nick = Nickname;
            FixedPosition = SpawnPointCheck(Nickname);
            isCheck = true;
            StartCoroutine("CreatePlayer");
        }
        catch
        {
            Debug.Log("SpawnManager.ChoiceCreate Error");
        }
    }

    //스폰 위치 체크
    GameObject SpawnPointCheck(string Nickname)
    {
        try
        {
            foreach (var b in Point)
            {
                if (b.CompareTag(Nickname))
                {
                    return b;
                }
            }
            return null;
        }
        catch
        {
            Debug.Log("SpawnManager.SpawnPointCheck Error");
            return null;
        }
    }

    IEnumerator CreatePlayer()
    {
        yield return new WaitForSeconds(mtCount.timeCost);  //남은시간
        OnCreate(nick, FixedPosition);
        Destroy(gameObject);
    }

    public void RandomCreate()
    {
        try
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    nick = "Kitchen";
                    FixedPosition = Point[0];
                    break;
                case 1:
                    nick = "Gurow";
                    FixedPosition = Point[1];
                    break;
                case 2:
                    nick = "PapaGu";
                    FixedPosition = Point[2];
                    break;
                case 3:
                    nick = "RainGuw";
                    FixedPosition = Point[3];
                    break;
                default:
                    break;
            }
            OnCreate(nick, FixedPosition);
        }
        catch
        {
            Debug.Log("SpawnManager.RandomCreate Error");
        }
    }

    public void OnCreate(string Nickname, GameObject point)
    {
        try
        {
            PhotonNetwork.Instantiate(Nickname, point.transform.position, Quaternion.identity);
            Choice.SetActive(false);
            inCanvace.SetActive(true);
            StartCoroutine("DestroyBullet");
        }
        catch
        {
            Debug.Log("SpawnManager.OnCreate Error");
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            GO.GetComponent<PhotonView>().RPC("DestroyRPC", RpcTarget.All);
        }
    }
    #endregion

    #region Win & Lose
    private void WinCheck()
    {
        try
        {
            int num = gameObject.GetComponent<StatusManager>().DeadPlayerCount;

            if (num == 1)
                WinPanel.SetActive(true);
        }
        catch
        {
            Debug.Log("SpawnManager.WinCheck Error");
        }
    }
    #endregion
}
