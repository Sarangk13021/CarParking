using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PAUSEscript : MonoBehaviour
{
    public GameObject WinScreenPanel;
    public GameObject FailScreenPanel;
    public GameObject PauseMenuPanel;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
   
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void HomeButton()
    {
        SceneManager.LoadScene(0);

    }
    public void ResumeButton()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LevelBack()
    {
        SceneManager.LoadScene(1);
    }
}
