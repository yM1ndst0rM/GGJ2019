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

    public void LoadSceneWithDelay()
    {
        Invoke("LoadGameOverScene", 7f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(8);
        GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>().Stop();
    }
}
