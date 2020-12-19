using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;

    
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = 0;
    }

    public void SceneLoaderFunc(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void MainMenuLoader()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadNextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
