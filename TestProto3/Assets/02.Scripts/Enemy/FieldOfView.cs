using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Transform Player;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshReosolution;

    public MeshFilter viewMeshFiter;
    Mesh viewMesh;

    EnemyCtrl enemy;

    //Enemy 시야 변경
    public Material[] material;
    GameObject m_Gviewmesh;

    public float firsttime = 0f;
    public float settime = 3f;
    public bool TargetCheck = false;

    private void Start()
    {
        enemy = this.gameObject.GetComponent<EnemyCtrl>();
        Player = GameManager.Instance.m_cShooterCtrl.transform;
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFiter.mesh = viewMesh;
        m_Gviewmesh = viewMeshFiter.gameObject;
        StartCoroutine("FindTargetWithDelay", .3f);
    }

    IEnumerator FindTargetWithDelay()
    {
        while (true)
        {
            FindVisibleTarget();
            yield return null;
        }
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {

            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshReosolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);

        }
    }

    public Vector3 DirFromAngle(float angleInDerees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDerees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDerees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDerees * Mathf.Deg2Rad));
    }

    private void Update()
    {
        if (visibleTargets.Contains(Player))
        {
            firsttime += Time.deltaTime;
            if (firsttime >= 3f)
            {
                GameManager.Instance.m_cShooterCtrl.Die();
            }
            m_Gviewmesh.GetComponent<Renderer>().material = material[1];
            enemy.m_eState = EnemyCtrl.EnemyState.Trace;
            TargetCheck = true;
        }
        else
        {
            if (firsttime >= 3f)
            {
                firsttime = 0;
            }

            if (TargetCheck)
            {
                TargetCheck = false;
                Invoke("StateChange", 3.0f);
            }

        }
    }

    void StateChange()
    {
        enemy.m_eState = EnemyCtrl.EnemyState.Idle;
        m_Gviewmesh.GetComponent<Renderer>().material = material[0];
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}
