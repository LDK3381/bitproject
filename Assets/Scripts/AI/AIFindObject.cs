using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFindObject : MonoBehaviour
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
        childEffect.SetActive(false);
        gameObject.GetComponent<AIController>().AIStart();
    }
}
