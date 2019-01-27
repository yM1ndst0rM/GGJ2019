using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    public string[] MainScenes;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScenesAsync());
    }

    private IEnumerator LoadScenesAsync()
    {

        foreach (var s in MainScenes)
        {
            
            var asyncLoad = SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
