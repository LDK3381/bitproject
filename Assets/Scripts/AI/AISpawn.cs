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


    void Start()
    {
        InvokeRepeating("SpawnAI", 1f, 1f);
    }

    void Update()
    {
        EnemyCountCheck();  //수시로 생성된 ai 수 관리
    }

    //AI 스폰 함수
    public void SpawnAI()
    {
        Vector3 point = GetRandomPoint();
        GameObject spawnedEnemy = Instantiate(enemy, point, enemy.transform.rotation);
        enemyCount++;
    }

    //Navmesh 범위 내에서 스폰할 랜덤 위치값 가져오기
    public Vector3 GetRandomPoint()
    {
        Vector3 RandomPosition = Random.insideUnitSphere * 10f;
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + RandomPosition, out hit, 15f, NavMesh.AllAreas);

        //타일 정중앙에 정확히 스폰하기 위해 위치값 조정
        Vector3 spawnPoint =
            new Vector3((float)(hit.position.x - hit.position.x % 0.375), hit.position.y, (float)(hit.position.z - hit.position.z % 0.375));

        return spawnPoint;
    }

    //생성된 AI 수 관리
    public void EnemyCountCheck()
    {
        //현재 필드에 최대 10마리 전부 있다면, 스폰 중지
        if (enemyCount >= maxCount)
        {
            CancelInvoke("SpawnAI");
        }
    }

    //AI 수 증가 이벤트
    public int IncreaseCount()
    {
        enemyCount++;
        return enemyCount;
    }

    //AI 수 감소 이벤트
    public int DecreaseCount()
    {
        enemyCount--;
        Start();        //수 줄어들면 다시 최대 10마리 채우기
        return enemyCount;
    }
}

