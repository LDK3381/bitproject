using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{ 
    public GameObject HPUI;     //HP UI
    public GameObject Choice;   //캐릭터 선택 UI
    public GameObject[] Point;  //캐릭터 스폰 위치
    public MtCount mtCount;     //캐릭터 선택 제한 시간
    private string nick;        //선택한 캐릭터 이름
    private bool isCheck;       //시간 내에 캐릭터를 선택하였는가?

    private void Start()
    {
        isCheck = false;
        HPUI.SetActive(false);
    }

    private void Update()
    {
        NotChoiceCreate();
    }

    //스폰 위치 체크
    Transform SpawnPointCheck()
    {
        while (true)
        {
            int idx = Random.Range(0, 4);

            if (!Point[idx].CompareTag("CheckPosition"))
            {
                Point[idx].SetActive(false);
                Point[idx].gameObject.tag = "CheckPosition";
                return Point[idx].transform;
            }
        }
    }

    private void NotChoiceCreate()  //버튼 선택 안하면.
    {
        if (mtCount.flag == false && isCheck == false)
        {
            RandomCreate();
            Destroy(gameObject);    //무한 스폰 방지
        }
    }

    public void ChoiceCreate(string Nickname)   //버튼 선택시.
    {
        nick = Nickname;
        isCheck = true;
        StartCoroutine("CreatePlayer");
    }
    IEnumerator CreatePlayer()
    {
        yield return new WaitForSeconds(mtCount.timeCost);  //남은시간
        OnCreate(nick);
        Destroy(gameObject);
    }

    public void RandomCreate()
    {
        switch (Random.Range(0, 4))
        {
            case 0: nick = "Kitchen"; break;
            case 1: nick = "Kitchen"; break;
            case 2: nick = "Kitchen"; break;
            case 3: nick = "Kitchen"; break;
            default:
                break;
        }
        OnCreate(nick);
    }

    public void OnCreate(string Nickname)
    {
        PhotonNetwork.Instantiate(Nickname, new Vector3(0.375f, 0.6f, 0.375f), Quaternion.identity);
        Choice.SetActive(false);
        HPUI.SetActive(true);
        StartCoroutine("DestroyBullet");
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            GO.GetComponent<PhotonView>().RPC("DestroyRPC", RpcTarget.All);
        }
    }
}
