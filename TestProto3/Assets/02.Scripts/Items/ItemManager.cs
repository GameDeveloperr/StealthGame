using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public enum ItemType
    { Normal_Bullet, Smoke_Bullet, Whorf_Bullet }

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        items.Add(new Item(0, 2, 4, 10));//normalBullet
        items.Add(new Item(0, 2, 4, 0));//SmokeBullet
        items.Add(new Item(0, 0, 0, 1));//WhorfBullet
    }

    public void DestroyBullet(Transform tr, Item item)
    {
        Destroy(tr.gameObject);
        GameManager.Instance.m_cCamera.Target = GameManager.Instance.m_cShooterCtrl.transform;
        GameManager.Instance.m_cCamera.ShootMode = false;
        item.hitCount = 4;
    }
}
