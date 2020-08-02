using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SgItemSpawn : MonoBehaviour
{
    public GameObject[] itemList;   //하트, 기본총알, 샷건총알, 폭탄 
    public static SgItemSpawn instance;
    public Queue<GameObject> i_queue = new Queue<GameObject>();

    AISpawn spawn;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        spawn = FindObjectOfType<AISpawn>();

        //리스트 내 모든 아이템들을 1개씩 큐에 저장(비활성화)
        for (int i = 0; i < itemList.Length; i++)
        {
            //이 코드가 들어있는 빈 오브젝트가 큐라는 저장공간이 됨.
            GameObject t_object = Instantiate(itemList[i], this.gameObject.transform);
            i_queue.Enqueue(t_object);
            t_object.SetActive(false);
            StartCoroutine(SpawnItem());
        }
    }

    //아이템을 먹으면 해당 아이템을 다시 큐에 저장(비활성화)
    public void InsertQueue(GameObject p_object)
    {
        i_queue.Enqueue(p_object);
        p_object.SetActive(false);

        StartCoroutine(SpawnItem());
    }

    //저장된 아이템을 큐에서 꺼내오기(활성화)
    public GameObject GetQueue()
    {
        GameObject t_object = i_queue.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }


    IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(1f);

        if (i_queue.Count != 0)
        {
            GameObject t_object = GetQueue();
            Vector3 point = spawn.GetRandomPoint();     //아이템 스폰지점은 ai와 동일하게 navmesh 범위 안
            t_object.transform.position = point;
        }
    } 
}
