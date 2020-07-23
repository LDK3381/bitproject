using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{ 
    public GameObject HPUI;     //HP UI
    public GameObject Choice;   //캐릭터 선택 UI
    public GameObject[] Point;  //캐릭터 스폰 위치
    public Text WaitText;
    public Button PlayerButton;
    

    private void Start()
    {
        Debug.Log(PhotonNetwork.PlayerList.Length);
        HPUI.SetActive(false);
        PlayerButton.interactable = false;
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

    public void OnCreate(string Nickname)
    {
        PhotonNetwork.Instantiate(Nickname, new Vector3(0.375f, 0.6f, 0.375f), Quaternion.identity);
        Choice.SetActive(false);
        HPUI.SetActive(true);
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            GO.GetComponent<PhotonView>().RPC("DestroyRPC", RpcTarget.All);
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(10.0f);
    }
}
