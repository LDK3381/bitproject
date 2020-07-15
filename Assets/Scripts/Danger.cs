using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    [SerializeField] Enemy m_enemy = null;      //위험지역으로 달려갈 Enemy

    //위험지역에 들어오는 것을 체크
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_enemy.SetTarget(other.transform);
        }
    }

    //위험지역에서 빠져나가는 것을 체크
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_enemy.RemoveTarget();
        }
    }

}
