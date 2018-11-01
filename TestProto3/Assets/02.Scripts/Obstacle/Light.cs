using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour {

    public int number;
	// Use this for initialization
	void Start () {
        number = GetComponentInParent<Obstacle>().number;
	}
	

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("PLAYER"))
        {
            //진입 불가
            GameManager.Instance.m_cShooterCtrl.Feel.gameObject.SetActive(true);
            Invoke("OffFeel", 1.0f);
        }
    }
    public void OffFeel()
    {
        GameManager.Instance.m_cShooterCtrl.Feel.gameObject.SetActive(false);
    }
}
