using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayer : MonoBehaviour
{ 
    public GameObject HPUI;     //HP UI
    public GameObject Choice;   //캐릭터 선택 UI
    public GameObject[] Point;  //캐릭터 스폰 위치

    public Text timeText = null;
    private float time;
    [SerializeField] string[] nickName = null;

    private void Awake()
    {
        time = 10f;
    }

    private void Start()
    {
        HPUI.SetActive(false);
    }

    void Update()
    {
        if (time > 0)
            time -= Time.deltaTime;

        timeText.text = Mathf.Ceil(time).ToString();
        if (time == 0f)
            OnCreate(nickName[Random.Range(0, 4)]);
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
        try
        {
            if(time == 0)
            {
                PhotonNetwork.Instantiate(Nickname, new Vector3(0.375f, 0.6f, 0.375f), Quaternion.identity);
                Choice.SetActive(false);
                HPUI.SetActive(true);
                StartCoroutine("DestroyBullet");
            }
        }
        catch
        {
            Debug.Log("반복");
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

}
