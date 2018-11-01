using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)) ]

public class EnemyCtrl : MonoBehaviour {
    public enum EnemyState
    { Idle, Trace, Stern};
    
    //네비게이션 Enemy 컨트롤
    public NavMeshAgent nav;

    public Transform target;
    private int MovePoinIdx = 0;

    public EnemyState m_eState;
    //어그로 폭탄 주변에 도착여부 검사

    public Transform[] MovePoint;

    //노말 폭탄
    public float TraceTime = 3.0f;
    FieldOfView ViewScript;
    public float m_fTime = 0f;

    //플레이어 발견 이미지
    public Canvas Chash_img;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        ViewScript = this.gameObject.GetComponent<FieldOfView>();
        m_eState = EnemyState.Idle;
        MovePoinIdx = MovePoint.Length-1;
        target = MovePoint[0];
        StartCoroutine("Move");
        nav.SetDestination(target.position);
        Chash_img.enabled = false;
        //플레이어 발견 느낌표 이미지 안보이게
    }

    public IEnumerator Move()
    {
        while (true)
        {
            switch (m_eState)
            {
                case EnemyState.Idle:
                    target = MovePoint[MovePoinIdx];
                    float dist = Vector3.Distance(transform.position, target.position);
                    if (dist <= 2.0f)
                    {
                        GetNextWaypoint();
                    }
                    Chash_img.enabled = false;
                    break;
                case EnemyState.Trace:
                    target = ViewScript.Player;
                    Chash_img.enabled = true;
                    break;
                case EnemyState.Stern:
                    target = this.transform;
                    nav.SetDestination(target.position);
                    yield return new WaitForSeconds(3.0f);
                    GetNextWaypoint();
                    m_eState = EnemyState.Idle;
                    break;
            }
            nav.SetDestination(target.position);
            yield return null;
        }
    }

    public void GetNextWaypoint()
    {
        if (MovePoinIdx == MovePoint.Length - 1)
        {
            MovePoinIdx = -1;
        }
        MovePoinIdx++;
        target = MovePoint[MovePoinIdx];
    }
}
