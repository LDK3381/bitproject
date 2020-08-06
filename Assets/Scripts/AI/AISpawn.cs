using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawn : MonoBehaviour
{
    [Header("적 AI")]
    [SerializeField] GameObject enemy = null;   //AI 프리팹
    [SerializeField] int enemyCount = 0;        //현재 ai 생성 수
    [SerializeField] int maxCount = 10;         //최대 ai 생성 제한 수

    public static AISpawn instance;
    public Queue<GameObject> e_queue = new Queue<GameObject>();

    void Start()
    {
        try
        {
            instance = this;
            CreateQueue();
        }
        catch
        {
            Debug.Log("AISpawn.Start Error");
        }
    }

    void Update()
    {
        EnemyCountCheck();
        StartCoroutine("SpawnAI");
    }

    #region AI 큐 활용
    //큐 생성
    private void CreateQueue()
    {
        try
        {
            for (int i = 0; i < maxCount; i++)
            {
                //instance.e_queue.Enqueue(CreateEnemy());  //Enqueue : 큐에 저장
                //큐라는 저장공간은 이 스크립트가 들어간 빈 오브젝트가 됨.
                GameObject e_object = Instantiate(enemy, this.gameObject.transform);
                e_queue.Enqueue(e_object);
                e_object.SetActive(false);
            }
        }
        catch
        {
            Debug.Log("AISpawn.CreateQueue Error");
        }
    }

    //적 ai 생성
    private GameObject CreateEnemy()
    {
        try
        {
            //큐라는 저장공간은 이 스크립트가 들어간 빈 오브젝트가 됨.
            GameObject e_object = Instantiate(enemy, this.gameObject.transform);
            e_object.SetActive(false);
            return e_object;
        }
        catch
        {
            Debug.Log("AISpawn.CreateEnemy Error");
            return null;
        }
    }

    //적이 파괴되었을 때 큐에 저장(비활성화)
    public void InsertQueue(GameObject e_object)
    {
        try
        {
            e_queue.Enqueue(e_object);
            e_object.SetActive(false);
        }
        catch
        {
            Debug.Log("AISpawn.InsertQueue Error");
        }
    }

    //적을 생성시키려 할 때 큐에서 꺼내기(활성화)
    public GameObject GetQueue()
    {
        try
        {
            //큐에 오브젝트가 담겨있으면 꺼내기
            if (instance.e_queue.Count > 0)
            {
                GameObject e_object = e_queue.Dequeue();    //Dequeue : 큐에서 꺼내기
                e_object.SetActive(true);

                return e_object;
            }
            //큐에 더 이상 꺼낼 오브젝트가 없다면? 새로 추가하기
            else
            {
                GameObject new_object = instance.CreateEnemy();
                new_object.gameObject.SetActive(true);
                return new_object;
            }
        }
        catch
        {
            Debug.Log("AISpawn.CreateEnemy Error");
            return null;
        }
    }

    //적 AI 사망 시, (큐 안에서 해당 적 오브젝트를 비활성화 처리)
    public void AIDie(GameObject killedEnemy)
    {
        try
        {
            killedEnemy.SetActive(false);
            instance.InsertQueue(killedEnemy);
        }
        catch
        {
            Debug.Log("AISpawn.AIDie Error");
        }
    }
    #endregion

    #region AI 스폰
    //AI 스폰 함수
    IEnumerator SpawnAI()
    {
        if (e_queue.Count != 0)
        {
            Vector3 point = GetRandomPoint();
            GameObject spawnedEnemy = GetQueue();
            spawnedEnemy.transform.position = point;

            //스폰된 적은 일단 마법진 사라질 때까지 잠시 행동 봉인
            spawnedEnemy.GetComponent<AIController>().enabled = false;
            spawnedEnemy.GetComponent<AITurretController>().enabled = false;

            //그 후 동작 실행
            yield return new WaitForSeconds(1f);
            spawnedEnemy.GetComponent<AIController>().enabled = true;
            spawnedEnemy.GetComponent<AITurretController>().enabled = true;

            enemyCount++;
        }
    }

    //Navmesh 범위 내에서 스폰할 랜덤 위치값 가져오기
    public Vector3 GetRandomPoint()
    {
        try
        {
            Vector3 RandomPosition = Random.insideUnitSphere * 10f;
            NavMeshHit hit;

            NavMesh.SamplePosition(transform.position + RandomPosition, out hit, 15f, NavMesh.AllAreas);

            //타일 정중앙에 정확히 스폰하기 위해 위치값 조정
            Vector3 spawnPoint =
                new Vector3((float)(hit.position.x - hit.position.x % 0.375), hit.position.y, (float)(hit.position.z - hit.position.z % 0.375));

            return spawnPoint;
        }
        catch
        {
            Debug.Log("AISpawn.GetRandomPoint Error");
            return new Vector3();
        }
    }

    #endregion
    
    #region AI 객체 수 관리
    //생성된 AI 수 관리
    public void EnemyCountCheck()
    {
        try
        {
            //현재 필드에 최대 10마리 전부 있다면, 스폰 중지
            if (enemyCount >= maxCount)
            {
                StopCoroutine("SpawnAI");
            }
        }
        catch
        {
            Debug.Log("AISpawn.EnemyCountCheck Error");
        }
    }

    //AI 수 증가 이벤트
    public int IncreaseCount()
    {
        try
        {
            enemyCount++;
            return enemyCount;
        }
        catch
        {
            Debug.Log("AISpawn.IncreaseCount Error");
            return 0;
        }
    }

    //AI 수 감소 이벤트
    public int DecreaseCount()
    {
        try
        {
            enemyCount--;
            return enemyCount;
        }
        catch
        {
            Debug.Log("AISpawn.DecreaseCount Error");
            return 0;
        }
    }
    #endregion
   
}

