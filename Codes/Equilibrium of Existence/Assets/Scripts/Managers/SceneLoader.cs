using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoader : MonoBehaviour
    {
        private int _currentSceneIndex;

        private void Start()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void SceneLoaderFunc(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadNextScene()
        {
       
            SceneManager.LoadScene(_currentSceneIndex + 1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
