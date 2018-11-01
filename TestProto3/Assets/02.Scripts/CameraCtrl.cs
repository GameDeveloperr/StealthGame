using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

    public Transform Target;

    public bool ShootMode = false;

    public float dist = 7.4f;
    public float height = 31.2f;
    public float shootheight = 20.0f;

    public float ClickTime = 1;

    void Start()
    {
        Target = GameObject.Find("Shooter").transform;
    }

    void FixedUpdate()
    {
        if(!ShootMode)
        {
            FollowTarget();
        }
        else
        {
            ShootCamera();
        }
    }

    void FollowTarget()
    {
        transform.position = Target.position - (Vector3.forward * dist) + (Vector3.up * height);
        transform.LookAt(Target);
    }
    void ShootCamera()
    {
        ClickTime += Time.deltaTime;
        if(ClickTime > 2.0f)
        {
            ClickTime = 2.0f;
        }
        transform.position = Target.position + (Vector3.up * shootheight * ClickTime);
    }
}
