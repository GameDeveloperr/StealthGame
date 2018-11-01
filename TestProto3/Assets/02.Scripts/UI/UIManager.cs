using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public List<Transform> bullet_btn = new List<Transform>();
    public Canvas Canvas_Local;
    public Canvas Canvas_world;
    public GameObject Stage_panel;
    public GameObject Clear_Panel;
    public GameObject GameOver_Panel;



    //bilboard를 위한 카메라 Target;
    public Transform Target;

    private void Start()
    {
        Stage_panel.SetActive(true);

        Target = Camera.main.gameObject.transform;
        Invoke("Stage", 2.0f);
    }

    void Stage() {
        Stage_panel.SetActive(false);
    }



}
