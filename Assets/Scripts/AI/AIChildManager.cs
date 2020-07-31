using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChildManager : MonoBehaviour
{
    [Header("AI의 자식들 관리")]
    [SerializeField] GameObject childAI = null;
    [SerializeField] GameObject childEffect = null;

    // Start is called before the first frame update
    void Start()
    {
        childAI.SetActive(false);   //적 생성할 때 이펙트만 보이게 하기
        StartCoroutine("ShowAI");
    }

    IEnumerator ShowAI()
    {
        //2초 후에 ai 본체 등장
        yield return new WaitForSeconds(2f);
        childAI.SetActive(true);

        //마법진 소멸은 AI 등장 후 1초 뒤에
        yield return new WaitForSeconds(1f);
        childEffect.SetActive(false);

        //마법진에서 완전히 소환되고 1초 후에 동작 실행
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<AIController>().AIStart();
    }
}
