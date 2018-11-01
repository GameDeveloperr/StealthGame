using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour {

    public Transform FirePos;
    public Transform Bullet;

    public void CreatBullet()
    {
        Instantiate(Bullet, FirePos.position, FirePos.rotation);
    }
}
