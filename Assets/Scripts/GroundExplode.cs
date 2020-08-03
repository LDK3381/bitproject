using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundExplode : MonoBehaviour
{
    private Animation  anima         = null;
    public  GameObject cliffCollider = null;
    public  GameObject enemy         = null;

    void Start()
    {
        try
        {
            anima = GetComponent<Animation>();
        }
        catch
        {
            Debug.Log("GroundExplode.Start Error");
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        try
        {
            if (other.gameObject.tag == "Bomb")
            {
                StartCoroutine(CreateCliffCollider());
                Destroy(gameObject, 2.1f);     //폭탄이 놓인 곳의 발판을 2.1초 후에 소멸
            }
        }
        catch
        {
            Debug.Log("GroundExplode.OnCollisionEnter Error");
        }
    }

    //파괴될 발판에 콜라이더 생성(AI가 해당 발판 구간을 못지나가게 제한)
    IEnumerator CreateCliffCollider()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(cliffCollider,
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z)
            , gameObject.transform.rotation);
    }

    public void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.gameObject.tag == "BombCheck")
            {
                anima.Play("GroundCount");  //폭탄 범위 빨간색 표시하는 애니메이션 작동
            }
        }
        catch
        {
            Debug.Log("GroundExplode.OnTriggerEnter Error");
        }
    }
}
