﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public class ItemSpawn : MonoBehaviourPun
{
    public GameObject[] itemList;   //하트, 기본총알, 샷건총알, 폭탄 
    public static ItemSpawn instance;
    public Queue<GameObject> i_queue = new Queue<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            instance = this;
            CreateQueue();
        }
        catch
        {
            Debug.Log("SgItemSpawn.Start Error");
        }
    }

    private void CreateQueue()
    {
        try
        {
            //리스트 내 모든 아이템들을 1개씩 큐에 저장(비활성화)
            for (int i = 0; i < itemList.Length; i++)
            {
                //이 코드가 들어있는 빈 오브젝트가 큐라는 저장공간이 됨.
                GameObject t_object = Instantiate(itemList[i], this.gameObject.transform);
                i_queue.Enqueue(t_object);
                t_object.SetActive(false);
            }

            StartCoroutine(SpawnItem());
        }
        catch
        {
            Debug.Log("SgItemSpawn.CreateQueue Error");
        }

    }

    //아이템을 먹으면 해당 아이템을 다시 큐에 저장(비활성화)
    public void InsertQueue(GameObject p_object)
    {
        try
        {
            i_queue.Enqueue(p_object);      //Enqueue : 오브젝트를 큐에 저장
            p_object.SetActive(false);

            StartCoroutine(SpawnItem());
        }
        catch
        {
            Debug.Log("SgItemSpawn.InsertQueue Error");
        }
    }

    //저장된 아이템을 큐에서 꺼내오기(활성화)
    public GameObject GetQueue()
    {
        try
        {
            GameObject t_object = i_queue.Dequeue();    //Dequeue : 오브젝트를 큐에서 꺼내기
            t_object.SetActive(true);

            return t_object;
        }
        catch
        {
            Debug.Log("SgItemSpawn.GetQueue Error");
            return null;
        }
    }


    IEnumerator SpawnItem()
    {
        while (true)
        {
            if (i_queue.Count != 0)
            {
                GameObject t_object = GetQueue();
                Vector3 point = GetRandomPoint();     //아이템 스폰지점은 ai와 동일하게 navmesh 범위 안
                t_object.transform.position = point;

                NavMeshAgent agent = t_object.GetComponent<NavMeshAgent>();
                agent.Warp(t_object.transform.position);  //NavMeshAgent가 오브젝트랑 떨어져있지 않도록, 자동으로 오브젝트 위치로 워프시킴
                agent.updateUpAxis = false;     //NavMeshAgent가 적용된 오브젝트를 옆으로 누울 수 있게 함
            }
            yield return new WaitForSeconds(1f);
        }
    }

    //Navmesh 범위 내에서 스폰할 랜덤 위치값 가져오기
    public Vector3 GetRandomPoint()
    {
        try
        {
            Vector3 RandomPosition = Random.insideUnitSphere * 15f;
            NavMeshHit hit;

            NavMesh.SamplePosition(transform.position + RandomPosition, out hit, 20f, NavMesh.AllAreas);

            //타일 정중앙에 정확히 스폰하기 위해 위치값 조정
            Vector3 spawnPoint =
                new Vector3((float)(hit.position.x - hit.position.x % 0.375), hit.position.y, (float)(hit.position.z - hit.position.z % 0.375));

            return spawnPoint;
        }
        catch
        {
            Debug.Log("SgItemSpawn.GetRandomPoint Error");
            return new Vector3();
        }
    }
}
