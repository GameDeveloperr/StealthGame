using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public int number;
    public Transform myLight = null;
    public Hole myHole = null;

    private void Start()
    {
        number = GetComponent<Obstacle>().number;
        StartCoroutine(CheckLight());
    }
    IEnumerator CheckLight()
    {
        yield return null;

        Light[] lights;
        lights = FindObjectsOfType<Light>();
        for (int i = 0; i < lights.Length; i++)
        {

            if (lights[i].number == number)
            {
                myLight = lights[i].transform;
            }
        }
        if(myLight == null)
        {
            CheckHole();
        }
    }
    public void CheckHole()
    {
        Hole[] holes;
        holes = FindObjectsOfType<Hole>();
        for (int i = 0; i < holes.Length; i++)
        {
            if (holes[i].number == number)
            {
                myHole = holes[i];
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BULLET"))
        {
            if (myLight != null)
            {
                Destroy(myLight.gameObject);
                myLight = null;
            }
            if(myHole != null)
            {
                myHole.ChangePos(myHole.holes[0], myHole.holes[1]);
            }
        }
    }
}
