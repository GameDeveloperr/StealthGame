using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonCtrl : MonoBehaviour {

    public Transform[] bullet;

    public Button[] buttons;
    public Image[] Images;
    public Text[] texts;
    public float coolTime = 3.2f;
    public bool isClicked = false;
    private float LeftTime = 0.0f;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Images[i] = buttons[i].GetComponent<Image>();
        }
        for (int i = 0; i < 3; i++)
        {
            texts[i] = buttons[i].gameObject.GetComponentInChildren<Text>();
            texts[i].text += " \n보유 :" + GameManager.Instance.m_cItemManager.items[i].haveCount.ToString();
    }
    }
    private void Update()
    {
        if(isClicked)
        {
            if(LeftTime > 0)
            {
                LeftTime -= Time.deltaTime;
                if(LeftTime < 0)
                {
                    LeftTime = 0;
                    for(int i = 0; i < 3; i++)
                    {
                        buttons[i].enabled = true;
                        isClicked = false;
                    }
                }
                float ratio = 1.0f - (LeftTime / coolTime);
                for(int i = 0; i < 3; i++)
                {
                    Images[i].fillAmount = ratio;
                }
            }
        }
    }
    public void StartCoolTime()
    {
        LeftTime = coolTime;
        isClicked = true;
        for(int i = 0; i < 3; i++)
        {
            buttons[i].enabled = false;
        }
    }

    public void NormalBulletBtn()
    {
        if (GameManager.Instance.m_cItemManager.items[(int)ItemManager.ItemType.Normal_Bullet].haveCount > 0 && !GameManager.Instance.m_cShooterCtrl.Shooting)
        {
            GameManager.Instance.m_cShooterCtrl.MyGun.Bullet = bullet[(int)ItemManager.ItemType.Normal_Bullet]; //총알 타입 결정
            GameManager.Instance.m_cShooterCtrl.Shooting = true; //발사 모드
            GameManager.Instance.m_cItemManager.items[(int)ItemManager.ItemType.Normal_Bullet].haveCount -= 1; // 총알 갯수 감소
            texts[0].text = "기본 탄" + " \n보유 :" + GameManager.Instance.m_cItemManager.items[0].haveCount.ToString(); // Text
            StartCoolTime();
        } 
    }
    public void SmokeBulletBtn()
    {
        if (GameManager.Instance.m_cItemManager.items[1].haveCount > 0)
        {
            GameManager.Instance.m_cShooterCtrl.MyGun.Bullet = bullet[(int)ItemManager.ItemType.Smoke_Bullet];
            GameManager.Instance.m_cShooterCtrl.Shooting = true;
            GameManager.Instance.m_cItemManager.items[(int)ItemManager.ItemType.Smoke_Bullet].haveCount -= 1;
            texts[1].text = "연막 탄" + " \n보유 :" + GameManager.Instance.m_cItemManager.items[1].haveCount.ToString();
            StartCoolTime();
        }
    }
    public void WhorfBulletBtn()
    {
        if (GameManager.Instance.m_cItemManager.items[2].haveCount > 0)
        {
            GameManager.Instance.m_cShooterCtrl.MyGun.Bullet = bullet[(int)ItemManager.ItemType.Whorf_Bullet];
            GameManager.Instance.m_cShooterCtrl.Shooting = true;
            GameManager.Instance.m_cItemManager.items[(int)ItemManager.ItemType.Whorf_Bullet].haveCount -= 1;
            texts[2].text = "워프 탄" + " \n보유 :" + GameManager.Instance.m_cItemManager.items[2].haveCount.ToString();
            StartCoolTime();
        }
    }



    public void RePlay()
    {
        //Time.timeScale = 1.0f;
        SceneManager.LoadScene("PlayScene");
    }

    public void Exit()
    {
        Application.Quit();
    }



}
