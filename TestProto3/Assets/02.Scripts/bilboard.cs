using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bilboard : MonoBehaviour {
	
	void Update () {
        gameObject.transform.LookAt(GameManager.Instance.m_cCamera.gameObject.transform);
	}
}
