using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSceneWithDelaay()
    {
        Invoke("LoadScene", 10f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(8);
    }
}
