using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SgTurret : MonoBehaviour
{
    [SerializeField] Transform tr_Gun_Body = null;

    [SerializeField] float tr_range = 0f;   //터렛의 사정거리

    [SerializeField] LayerMask tr_layerMask = 0;    //특정레이어 마스크 검출

    [SerializeField] float tr_spinSpeed = 0f;   //터렛 회전속도

    [SerializeField] float tr_fireRate = 0f;    //연사속도

    [SerializeField] float tr_fireSpeed = 0f;   //총알속도

    [SerializeField] GameObject tr_Bullet_Prefab = null;   //터렛 총알 프리팹

    [SerializeField] ParticleSystem tr_MuzzleFlash = null;  //터렛 발사 이펙트

    float tr_currentFireRate = 0f;  //연사속도 변수

    Transform tr_Target = null;     //공격할 대상 트렌스폼

    Transform me = null;    //나의 위치(포탑위치)

    //가까운 플레이어 탐색
    void SearchPlayer()
    {
        //OverlapSphere : 객체 주변의 Collider를 검출
        Collider[] tr_cols = Physics.OverlapSphere(transform.position, tr_range, tr_layerMask); //터렛 주변의 Collider 검출
        Transform tr_shortTarget = null;    //터렛과 가까운 트랜스폼

        //검출된 Collider 있을경우
        if (tr_cols.Length>0)
        {
            float tr_shortDistance = Mathf.Infinity; //짧은것을 비교하려면 가장 긴 녀석이 기준이 되야함

            //검출된 Collider만큼 반복
            foreach (Collider tr_colTarget in tr_cols)
            {
                //SqrMagnitude : 제곱 반환(실제거리 x 실제거리)
                //Distance : 루트 연산 후 반환(실제거리)
                //터렛의 거리와 Collider의 거리 계산
                float tr_distance = Vector3.SqrMagnitude(transform.position - tr_colTarget.transform.position);

                //거리비교를 위한 거리보다 작다면
                if (tr_shortDistance > tr_distance)
                {
                    //가장 가까운 대상으로 확인
                    tr_shortDistance = tr_distance;
                    //타겟에 Collider 적용
                    tr_shortTarget = tr_colTarget.transform;
                }
            }
        }

        tr_Target = tr_shortTarget; //타겟을 가까운 타겟으로 대입
    }
    
    void Start()
    {
        //연사속도 대입
        tr_currentFireRate = tr_fireRate;

        //플레이어 탐색 0.5초마다 실행
        InvokeRepeating("SearchPlayer", 0f, 0.5f);

        //나의 위치 시작할때 대입
        me = transform;
    }

    void Update()
    {
        //타겟이 없을경우(터렛 회전)
        if (tr_Target == null)
            tr_Gun_Body.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
        //타겟이 있을 경우
        else
        {
            //LookRotation : 특정 좌표를 바라보게 만드는 회전값을 리턴
            Quaternion tr_lookRotation = Quaternion.LookRotation(tr_Target.position - me.position);   //플레이어 위치 확인
            Debug.Log(tr_lookRotation);

            //RotateTowards : a지점에서 b지점까지 c스피드로 회전
            Vector3 tr_euler = Quaternion.RotateTowards
                (tr_Gun_Body.rotation,  //a
                tr_lookRotation,        //b
                tr_spinSpeed * Time.deltaTime).eulerAngles; //c

            //오일러 값에서 y축만 반영되게 수정 한 뒤 쿼티니온으로 변환
            tr_Gun_Body.rotation = Quaternion.Euler(0, tr_euler.y, 0);

            //터렛이 조준할 방향(y축)
            Quaternion tr_fireRotation = Quaternion.Euler(0, tr_lookRotation.eulerAngles.y, 0);

            //현재 터렛의 방향과 조준할 방향
            if (Quaternion.Angle(tr_Gun_Body.rotation, tr_fireRotation) < 5f)
            {
                tr_currentFireRate -= Time.deltaTime;

                if (tr_currentFireRate <= 0)
                {
                    tr_currentFireRate = tr_fireRate;

                    //총알 Instantiate(무한 생성)
                    var clone = Instantiate
                        (tr_Bullet_Prefab, tr_MuzzleFlash.transform.position, Quaternion.identity);

                    //총알 AddForce(발사)
                    clone.GetComponent<Rigidbody>().AddForce((tr_Target.position - me.position) * tr_fireSpeed);
                }
            }
        }
    }
}
