using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {

    public int number;
    public Transform[] holes;
    public float speed = 5.0f;
    private float StartHeight;

    private void Start()
    {
        number = GetComponent<Obstacle>().number;
        //0번 홀이 아래 // 1번 홀이 위
        StartHeight = holes[1].position.y;
    }

    public void ChangePos(Transform tr1, Transform tr2)
    {
        if(tr1.position.y > tr2.position.y)
        {
            StartCoroutine(Up(tr2));
            StartCoroutine(Down(tr1));
        }
        else
        {
            StartCoroutine(Up(tr1));
            StartCoroutine(Down(tr2));
        }
    }
    IEnumerator Up(Transform tr)
    {
        bool run = true;
        while(run)
        {
            tr.Translate(Vector3.up * Time.deltaTime * speed);
            if(tr.position.y >= StartHeight)
            {
                tr.position = new Vector3(tr.position.x, StartHeight, tr.position.z);
                run = false;
            }
            yield return null;
        }
    }
    IEnumerator Down(Transform tr)
    {
        bool run = true;
        while(run)
        {
            tr.Translate(Vector3.down * Time.deltaTime * speed);
            if(tr.position.y <= 9.0f)
            {
                tr.position = new Vector3(tr.position.x, 9.0f, tr.position.z);
                run = false;
            }
            yield return null;
        }
    }
}
