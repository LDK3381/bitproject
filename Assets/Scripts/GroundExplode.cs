using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundExplode : MonoBehaviour
{
    private Animation anima = null;
    public GameObject cliffCollider = null;
    public GameObject enemy = null;

    void Start()
    {
        anima = GetComponent<Animation>();
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            StartCoroutine(CreateCliffCollider());
            Destroy(gameObject, 2.1f);     //폭탄이 놓인 곳의 발판을 2.1초 후에 소멸
        }
    }

    //파괴될 발판에 콜라이더 생성(AI가 그 발판 구간을 못지나가게 제한)
    IEnumerator CreateCliffCollider()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(cliffCollider,
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z)
            , gameObject.transform.rotation);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BombCheck")
        {
            anima.Play("GroundCount");  //폭탄 범위 빨간색 표시하는 애니메이션 작동
        }
    }
}
