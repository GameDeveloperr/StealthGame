using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterCtrl : MonoBehaviour {

    // 플레이어 이동 시에만 사용하는 클래스 // 전투, 효과 등은 다른 클래스에서
    private Vector2 MousePos = Vector2.zero;
    private Vector2 StartPos = Vector2.zero;
    //회전 컨트롤
    private Vector2 Dir = Vector2.zero;
    private float dist = 0;
    private float Degree = 0;
    // 궤적
    private LineRenderer Line;
    public Transform LineColl;
    //Shoot 변수
    public bool MouseDown = false;
    public bool Shooting = false;

    public GunCtrl MyGun;
    //public Image ClickImg;
    //private bool Clear = false;
    private bool Dead = false;

    //벽material 저장공간 전환할것
    Material Temp = null;
    // 플레이어 캔버스
    public Transform Feel;


    private void Start()
    {
        MyGun = GetComponentInChildren<GunCtrl>();
        Line = GetComponentInChildren<LineRenderer>();
        Line.enabled = false;
        Feel.gameObject.SetActive(false);
    }
    private void Update()
    {
      
        if (Shooting)
        {
            ShootingSling();
        }
        if (MouseDown)
        {
            RotatePlayer();
        }
              
    }
    void SetFirst()
    {
        Degree = 0;
        Dir = Vector2.zero;
        dist = 0;
    }
    void ShootingSling()
    {
        // Debug.Log("플레이 준비");
        if (Input.GetMouseButtonDown(0) && !MouseDown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.collider.CompareTag("PLAYER"))
                {
                    
                    SetFirst();
                    StartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    MouseDown = true;
                    GameManager.Instance.m_cCamera.ShootMode = true;
                    //궤적
                    StartCoroutine(DrawLine());
                }
            }
        }
        else if (Input.GetMouseButton(0) && MouseDown)
        {
            MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            dist = Vector3.Distance(StartPos, MousePos);
        }
        else if (Input.GetMouseButtonUp(0) && MouseDown)
        {
            MyGun.CreatBullet();
            //ClickImg.enabled = false;
            MouseDown = false;
            Shooting = false;
        }
    }
    void RotatePlayer()
    {
        if (dist > 1.0f)
        {
            Dir = MousePos - StartPos;
            Degree = (Mathf.Atan2(Dir.x, Dir.y) * Mathf.Rad2Deg + 360f) % 360;

            transform.rotation = Quaternion.Euler(0, Degree + 180f, 0);
        }
    }

    IEnumerator DrawLine()
    {
        Line.enabled = true;
        Vector3 incomingVec;
        Vector3 reflecVec;

        Line.positionCount = 3;

        while (MouseDown)
        {
            Line.SetPosition(0, Line.gameObject.transform.position);
            RaycastHit hitpoint;
            if (Physics.SphereCast(Line.gameObject.transform.position, Line.gameObject.transform.localScale.x / 2, Line.gameObject.transform.forward, out hitpoint, 100.0f))
            {
                    LineColl.position = hitpoint.point;
                    incomingVec = LineColl.position - Line.gameObject.transform.position;
                    reflecVec = Vector3.Reflect(incomingVec, hitpoint.normal);
                    LineColl.rotation = Quaternion.LookRotation(reflecVec);
                    Line.SetPosition(1, LineColl.position);

                    RaycastHit hitpoint_2;
                    if (Physics.SphereCast(LineColl.position, Line.gameObject.transform.localScale.x / 2, LineColl.forward, out hitpoint_2, 100.0f))
                    {
                        Line.SetPosition(2, hitpoint_2.point);
                    }
            }
            yield return null;
        }
        Line.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WALL") || other.CompareTag("STREETLIGHT"))
        {
            Debug.Log(other.name);

            gameObject.transform.Translate(-Vector3.forward * Time.deltaTime * 15);
        }

        if (other.CompareTag("QUEST")) {
            Destroy(other.gameObject);
            GameManager.Instance.AddQuest();
        }
        else if (other.CompareTag("EXIT"))
        {
            GameManager.Instance.Clear();
        }

        //if (other.CompareTag("ENEMY"))
        //{
        //    Die();
        //}

        //벽투명하게 하는것
        if (other.CompareTag("WALL"))
        {
            Material[] material = other.GetComponent<MeshRenderer>().materials;
            Temp = material[0];
            other.GetComponent<MeshRenderer>().material = material[1];
            material[0] = material[1];
            material[1] = Temp;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WALL"))
        {
            Material[] material = other.GetComponent<MeshRenderer>().materials;
            other.GetComponent<MeshRenderer>().material = Temp;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        ////Player가 벽을 통과하지 못하게
        if (collision.collider.CompareTag("WALL"))
        {
            this.gameObject.transform.Translate(-Vector3.forward * Time.deltaTime * 15);
        }

        //적과 충돌시  통과 X &  사망
        if (collision.collider.CompareTag("ENEMY")){
            this.gameObject.transform.Translate(-Vector3.forward * Time.deltaTime * 15);
            Die();
        }
    }

    public void Die()
    {
        Dead = true;
    }

    public bool Cheack()
    {
        return Dead;
    }

}
