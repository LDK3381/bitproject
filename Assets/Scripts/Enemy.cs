using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//RequireComponent : 요구되는 의존 컴포넌트를 자동으로 추가
[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    NavMeshAgent m_enemy = null;

    [SerializeField] Transform[] m_WayPoints = null;    //정찰위치를 담는 배열
    int m_count = 0;

    Transform m_target = null;

    //타겟 지정(기존 순찰 취소)
    public void SetTarget(Transform p_target)
    {
        //CancelInvoke() : 현재 MonoBehaviour상의 모든 invoke 호출을 취소
        CancelInvoke();
        m_target = p_target;
    }

    //타겟 해재 후 다시 순찰
    public void RemoveTarget()
    {
        m_target = null;
        //InvokeRepeating : time 초에 /methodName/메서드를 호출한 후, 매 /repeatRate/초 마다 반복적으로 호출합니다.
        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
    }

    //다음 정찰지역으로 이동
    void MoveToNextWayPoint()
    {
        //타겟이 범위내에 없을 떄 순찰
        if (m_target == null)
        {
            //속도가 0이 되면 다음 지역으로 순찰 시작
            if (m_enemy.velocity == Vector3.zero)
            {
                m_enemy.SetDestination(m_WayPoints[m_count++].position);

                //정찰위치를 다 탐색하고 처음지역으로 돌아감
                if (m_count >= m_WayPoints.Length)
                    m_count = 0;
            }
        }
    }

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        
        //2초마다 정찰을 반복
        InvokeRepeating("MoveToNextWayPoint", 0f, 1f);
    }

    void Update()
    {
        //타겟의 위치로 이동
        if (m_target != null)
        {
            m_enemy.SetDestination(m_target.position);
        }
    }
}
