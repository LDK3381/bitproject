using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   // UI 사용에 필요한 네임스페이스
using Photon.Pun;

public class StatusManager : MonoBehaviourPun
{
    [SerializeField] int maxHp = 0;         // 최대 체력
    int currentHp = 0;                      // 현재 체력

    [SerializeField] Image[] img_HpArray = null;   // 체력 UI

    bool isInvincibleMode = false;  // 무적상태 확인

    [SerializeField] GameObject obj;          // 사망시 비석오브젝트

    [SerializeField] float blinkSpeed = 0f;  // 플레이어 깜밖임 속도
    [SerializeField] int blinkCount = 0;    // 플레이어 깜밖임 횟수

    [SerializeField] MeshRenderer mesh_PlayerGraphics = null;

    [SerializeField] GameObject playerPosition;

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
                img_HpArray[i].gameObject.SetActive(true);
            else
                img_HpArray[i].gameObject.SetActive(false);
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

    // HP 감소
    public void DecreaseHp(int _num)
    {
        if (!isInvincibleMode)
        {
            currentHp -= _num;
            HpUpdate();

            if (currentHp <= 0)
            {
                PlayerDead();
                return;
            }

            SoundManager.instance.PlaySE("Hurt");
            StartCoroutine(BlinkCoroutine());
        }
    }

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

    // 플레이어 사망
    void PlayerDead()
    {
        gameObject.SetActive(false);
        Instantiate(obj, 
            new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y + 1, playerPosition.transform.position.z),
            Quaternion.identity);
    }
}
