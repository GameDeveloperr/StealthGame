using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {

    //Shoot 변수
    private float MoveSpeed = 0f;
    //Player Move
    private Vector3 BeforePos = Vector3.zero;

    public ItemManager.ItemType itemType;

    Item item;

    private float m_fTime = 0;
    //Smke Effect
    public Transform SmokeEffect;

    private void Start()
    {
        MoveSpeed = 20.0f;
        BeforePos = this.gameObject.transform.position;
        item = GameManager.Instance.m_cItemManager.items[(int)itemType];
        SelectEffect();
    }
    private void Update()
    {
        float distanceThisFrame = MoveSpeed * Time.deltaTime;
   
        transform.Translate(transform.forward * distanceThisFrame, Space.World);

        m_fTime += Time.deltaTime;
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        item.Effect(collision);
    }

    public void NormalBullet(Collision collision)
    {
        Vector3 incomingVec = transform.position - BeforePos; // 입사각 => 충돌지점 - 출발지점
        Vector3 normalVec = collision.contacts[0].normal; // 법선벡터
        Vector3 reflecVec = Vector3.Reflect(incomingVec, normalVec); //반사각

        transform.rotation = Quaternion.LookRotation(reflecVec);

        BeforePos = transform.position; // 이전 위치 갱신

        item.hitCount--;

        if(m_fTime >= item.time)
        {
            Destroy(this.gameObject);
            GameManager.Instance.m_cItemManager.DestroyBullet(this.transform, item);
            GameManager.Instance.m_cCamera.ClickTime = 1;
        }
        if (collision.collider.CompareTag("ENEMY"))
        {
            
            EnemyCtrl enemy = collision.collider.GetComponent<EnemyCtrl>();
            enemy.m_eState = EnemyCtrl.EnemyState.Stern;

            Destroy(transform.gameObject); 
            GameManager.Instance.m_cItemManager.DestroyBullet(this.transform, item);
            GameManager.Instance.m_cCamera.ClickTime = 1;
        }

        if (item.hitCount == 0)
        {
            Destroy(transform.gameObject);    //총알로 사용할 경우 4번째 충돌시 파괴
            GameManager.Instance.m_cItemManager.DestroyBullet(this.transform, item);
            GameManager.Instance.m_cCamera.ClickTime = 1;
        }


    }
    public void SmokeBullet(Collision collision)
    {
        Vector3 incomingVec = transform.position - BeforePos; // 입사각 => 충돌지점 - 출발지점
        Vector3 normalVec = collision.contacts[0].normal; // 법선벡터
        Vector3 reflecVec = Vector3.Reflect(incomingVec, normalVec); //반사각

        transform.rotation = Quaternion.LookRotation(reflecVec);

        BeforePos = transform.position; // 이전 위치 갱신

        item.hitCount--;

        if (m_fTime >= item.time)
        {
            Vector3 Pos = new Vector3(transform.position.x, 0.1f, transform.position.z);
            Instantiate(SmokeEffect, Pos, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.Instance.m_cItemManager.DestroyBullet(this.transform, item);
            GameManager.Instance.m_cCamera.ClickTime = 1;
        }

        if (item.hitCount == 0)
        {
            Vector3 Pos = new Vector3(transform.position.x, 0.1f, transform.position.z);
            Instantiate(SmokeEffect, Pos, Quaternion.identity);
            Destroy(this.gameObject);   //총알로 사용할 경우 4번째 충돌시 파괴
            GameManager.Instance.m_cItemManager.DestroyBullet(this.transform, item);
            GameManager.Instance.m_cCamera.ClickTime = 1;
        }
    }
    
    public void WhorfBullet(Collision collsision)
    {
        GameManager.Instance.m_cShooterCtrl.transform.position = transform.position;
        //GameManager.Instance.m_cCamera.Target = GameManager.Instance.m_cShooterCtrl.transform;
        GameManager.Instance.m_cCamera.ShootMode = false;
        GameManager.Instance.m_cShooterCtrl.Shooting = false;
        Destroy(gameObject);
        GameManager.Instance.m_cCamera.ClickTime = 1;
    }
    public void SelectEffect()
    {
        switch(itemType)
        {
            case ItemManager.ItemType.Normal_Bullet:
                NewMethod(NormalBullet);
                break;
            case ItemManager.ItemType.Smoke_Bullet:
                NewMethod(SmokeBullet);
                break;
            case ItemManager.ItemType.Whorf_Bullet:
                NewMethod(WhorfBullet);
                break;
            default:
                break;
        }
    }

    private void NewMethod(System.Action<Collision> effect)
    {
        item.Effect = effect;
    }
}
