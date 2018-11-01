using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainButtonCtrl : MonoBehaviour {

    public GameObject Startmenu;
    public GameObject Settingmenu;

    private void Start()
    {
        Startmenu.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    public void MainStart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void Setting()
    {
        Startmenu.SetActive(false);
        Settingmenu.SetActive(true);


    }

    public void Done()
    {
        Startmenu.SetActive(true);
        Settingmenu.SetActive(false);
    }
}
