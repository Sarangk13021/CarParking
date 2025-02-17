using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LEVELUIscript : MonoBehaviour
{
    // Start is called before the first frame update
    public void Level1()
    {
        SceneManager.LoadScene(2);
    }
    public void Level2()
    {
        SceneManager.LoadScene(3);
    }
    public void Level3()
    {
        SceneManager.LoadScene(4);
    }
    public void Level4()
    {
        SceneManager.LoadScene(5);
    }
    public void Level5()
    {
        SceneManager.LoadScene(6);
    }
    public void Level6()
    {
        SceneManager.LoadScene(7);
    }
    public void Level7()
    {
        SceneManager.LoadScene(8);
    }
    public void Level8()
    {
        SceneManager.LoadScene(9);
    }
    public void Level9()
    {
        SceneManager.LoadScene(10);
    }
    public void Level10()
    {
        SceneManager.LoadScene(11);
    }
    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
