using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HOMEUISCRIPT : MonoBehaviour
{
    public GameObject[] cars;
    private int current;

    public GameObject mainPage;
    public GameObject garagePage;
    public GameObject settingsPage;
    public GameObject morePage;
    public GameObject backArrow;
    
    void Start()
    {
        mainPage.SetActive(true);
        garagePage.SetActive(false);
        settingsPage.SetActive(false);
        morePage.SetActive(false);
        
        Time.timeScale = 1f;
        current = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (current == 0)
        {
            backArrow.SetActive(true);
        }
        else
        {
            backArrow.SetActive(false);
        }

    }
    public void next()
    {
        mainPage.SetActive(false);
        garagePage.SetActive(false);
        settingsPage.SetActive(false);
        morePage.SetActive(false);
    }
    public void level()
    {
        mainPage.SetActive(false);
        garagePage.SetActive(false);
        settingsPage.SetActive(false);
        morePage.SetActive(false);
    }
    public void garage()
    {
        mainPage.SetActive(false);
        garagePage.SetActive(true);
        settingsPage.SetActive(false);
        morePage.SetActive(false);
    }
    public void settings()
    {
        mainPage.SetActive(true);
        garagePage.SetActive(false);
        settingsPage.SetActive(true);
        morePage.SetActive(false);
    }
    public void more()
    {
        mainPage.SetActive(true);
        garagePage.SetActive(false);
        settingsPage.SetActive(false);
        morePage.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void homee()
    {
        mainPage.SetActive(true);
        garagePage.SetActive(false);
        settingsPage.SetActive(false);
        morePage.SetActive(false);
    }

    public void NextCar()//start
    {
        foreach (GameObject i in cars)
        {
            i.SetActive(false);
        }
        current++;
        if (current == cars.Length)
        {
            current = 0;
        }
        cars[current].SetActive(true);
    }
    public void privCar()
    {
        foreach (GameObject i in cars)
        {
            i.SetActive(false);
        }
        current--;
        if (current == -1)
        {
            current = cars.Length - 1;
        }
        cars[current].SetActive(true);
    }
    public void ResetToFirstCar()
    {
        foreach (GameObject i in cars)
        {
            i.SetActive(false);
        }
        current = 0;
        cars[current].SetActive(true);
    }//end
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    
}
