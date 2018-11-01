using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agro : MonoBehaviour {

    public EnemyCtrl enemy;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BULLET"))
        {
            Debug.Log("맞음");

            enemy.m_eState = EnemyCtrl.EnemyState.Trace;
            enemy.target = gameObject.transform;
        }
    }



}
