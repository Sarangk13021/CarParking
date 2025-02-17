using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel;
    public GameObject InstructionPanel;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("obs"))
        {
            Time.timeScale = 0f;
            Panel.SetActive(true);
            InstructionPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    

    public void restart()
    {
        
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Panel.SetActive(false);
        InstructionPanel.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
