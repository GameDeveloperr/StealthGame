using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    protected static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
                if (instance == null)
                {
                    Debug.LogError("Error Creating Intance.");
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public CameraCtrl m_cCamera;
    public UIManager m_cUIManager;
    public ShooterCtrl m_cShooterCtrl;
    public ItemManager m_cItemManager;
    public JoystickCtrl m_cJoystickCtrl;

    private int Quset = 0;
    

    public void AddQuest()
    {
        Quset++;

    }

    public void Clear()
    {
        if(Quset == 3)
        {
            //Time.timeScale = 0f;
            m_cUIManager.Canvas_Local.gameObject.SetActive(false);
            m_cUIManager.Clear_Panel.SetActive(true);

        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //GameOver Cheack
        if (m_cShooterCtrl.Cheack() == true)
        {
            m_cUIManager.Canvas_Local.gameObject.SetActive(false);
            m_cUIManager.GameOver_Panel.SetActive(true);
        }
    }

   
}


