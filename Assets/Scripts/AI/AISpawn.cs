using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawn : MonoBehaviour
{
    [Header("적 AI")]
    [SerializeField] GameObject enemy = null;          //AI 프리팹

    AIFindObject find;

    public Wave[] waves;
    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;


    [System.Serializable]
    public class Wave
    {

        public int enemyCount = 0;      //생성시킬 AI 수`
        public float timeBetweenSpawns = 0f;    //스폰 시간 간격
    }

    // Start is called before the first frame update
    void Start()
    {
        find = GetComponent<AIFindObject>();
        NextWave();
        InvokeRepeating("SpawnAI", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            //SpawnEffect();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        print("Wave: " + currentWaveNumber);
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    //AI 스폰 함수
    public void SpawnAI()
    {
        Vector3 point = GetRandomPoint();      
        GameObject spawnedEnemy = Instantiate(enemy, point, enemy.transform.rotation);
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
}
