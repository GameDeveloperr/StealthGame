using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterInven : MonoBehaviour {

    List<Item>m_cInven = new List<Item>();


    private void Start()
    {
        m_cInven.Add(GameManager.Instance.m_cItemManager.items[(int)ItemManager.ItemType.Normal_Bullet]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            switch(collision.gameObject.name)
            {
                case "Item_Smoke":
                    setInven(GameManager.Instance.m_cItemManager.items[(int)ItemManager.ItemType.Smoke_Bullet]);
                    Destroy(collision.gameObject);
                    Debug.Log(collision.gameObject.name);
                    GameManager.Instance.m_cItemManager.items[1].haveCount++;
                    GameManager.Instance.m_cUIManager.GetComponent<ButtonCtrl>().texts[1].text = "연막 탄" + " \n보유 :" + GameManager.Instance.m_cItemManager.items[1].haveCount.ToString();
                    break;
                case "Item_Whorf":
                    setInven(GameManager.Instance.m_cItemManager.items[(int)ItemManager.ItemType.Whorf_Bullet]);
                    Destroy(collision.gameObject);
                    Debug.Log(collision.gameObject.name);
                    GameManager.Instance.m_cItemManager.items[2].haveCount++;
                    break;
                default:
                    break;
            }
        }
    }

    public void setInven(Item item)
    {
        m_cInven.Add(item);
    }

    public Item getInven(ItemManager.ItemType item)
    {
        return m_cInven[(int)item];
    }
}
