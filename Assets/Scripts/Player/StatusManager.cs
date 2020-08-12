using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   // UI 사용에 필요한 네임스페이스
using Photon.Pun;

public class StatusManager : MonoBehaviourPun
{
    [SerializeField] int maxHp = 3;         // 최대 체력
    public int currentHp;                      // 현재 체력

    [SerializeField] Image[] img_HpArray = null;   // 체력 UI
    [SerializeField] Image[] img_BigHpArray = null;   // 체력 UI

    bool isInvincibleMode = false;  // 무적상태 확인

    [SerializeField] GameObject obj = null;   // 사망시 비석오브젝트

    [SerializeField] float blinkSpeed = 0f;  // 플레이어 깜밖임 속도
    [SerializeField] int blinkCount = 0;    // 플레이어 깜밖임 횟수
    [SerializeField] MeshRenderer mesh_PlayerGraphics = null;

    [SerializeField] GameObject playerPosition = null;  //패배나 승리 시 플레이어 위치 확인
    [SerializeField] SgPauseManager sealKey = null;     //패배나 승리 시 플레이어 움직임 제한
    public MtSealKey mtSealKey;
    [SerializeField] GameObject losePanel = null;   //패배 패널 불러오기 위해 필요

    void Start()
    {
        #region 체력을 최대 체력으로
        currentHp = maxHp;
        HpUpdate();
        #endregion
    }

    // HP 상태를 업데이트
    void HpUpdate()
    {
        #region 시작체력 개수와 체력이 감소했을 떄
        for (int i = 0; i < img_HpArray.Length; i++)
        {
            if (i < currentHp)
            {
                img_HpArray[i].gameObject.SetActive(true);
                img_BigHpArray[i].gameObject.SetActive(true);
            }
            else
            {
                img_HpArray[i].gameObject.SetActive(false);
                img_BigHpArray[i].gameObject.SetActive(false);
            }
        }
        #endregion
    }

    // HP 증가
    public void IncreaseHp(int _num)
    {
        #region 체력을 획득
        if (currentHp == maxHp)
            return;

        currentHp += _num;


        if (currentHp > maxHp)
            currentHp = maxHp;

        HpUpdate();
        #endregion
    }

    #region Single
    // HP 감소
    public void SgDecreaseHp(int _num)
    {
        if (!isInvincibleMode)
        {
            currentHp -= _num;
            HpUpdate();

            if (currentHp <= 0)
            {
                SgPlayerDead();
                return;
            }

            SoundManager.instance.PlaySE("Hurt");
            StartCoroutine(BlinkCoroutine());
        }
    }

    // 플레이어 사망
    public void SgPlayerDead()
    {
        mtSealKey = null;
        gameObject.SetActive(false);

        //키보드, 마우스 잠금
        sealKey.SealKey();

        Instantiate(obj,
            new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y + 2, playerPosition.transform.position.z),
            Quaternion.Euler(0, 90, 0));

        //losepanel 실행
        losePanel.SetActive(true);
    }
    #endregion

    #region Multi
    // HP 감소
    public void MtDecreaseHp(int _num)
    {
        if (!isInvincibleMode)
        {
            currentHp -= _num;
            HpUpdate();

            if (currentHp <= 0)
            {
                MtPlayerDead();
                return;
            }

            SoundManager.instance.PlaySE("Hurt");
            StartCoroutine(BlinkCoroutine());
        }
    }

    // 플레이어 사망
    public void MtPlayerDead()
    {
        mtSealKey.SealKey();
        sealKey = null;
        losePanel = null;
        gameObject.SetActive(false);

        Instantiate(obj,
            new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y + 2, playerPosition.transform.position.z),
            Quaternion.Euler(0, 90, 0));
    }
    #endregion

    // 깜밖임 함수
    IEnumerator BlinkCoroutine()
    {
        isInvincibleMode = true;

        for (int i = 0; i < blinkCount * 2; i++)
        {
            mesh_PlayerGraphics.enabled = !mesh_PlayerGraphics.enabled; // 깜밖임이 꺼졌다 켜졌다 반복 (짝수배로)
            yield return new WaitForSeconds(blinkSpeed);    // 코루틴은 반드시 대기 문법이 존재해야 함
        }

        isInvincibleMode = false;
    }

    //플레이어가 맵 밖으로 빠지면 익사 판정
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadZone")
        {
            Debug.Log("플레이어가 익사했습니다.");
            CheckInRoom();
        }
    }

    private void CheckInRoom()
    {
        if (PhotonNetwork.InRoom)
            MtPlayerDead();
        else
            SgPlayerDead();
    }
}
