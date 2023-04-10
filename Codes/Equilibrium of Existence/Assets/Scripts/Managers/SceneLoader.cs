using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using  UnityEngine.EventSystems;

namespace Managers
{
    public class SceneLoader : MonoBehaviour
    {
        private int _currentSceneIndex;
        public GameObject howToPlayPanel;
        public GameObject creditsPanel;
        private PauseMenu pauseControl;

        private void Start()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            pauseControl = GetComponent<PauseMenu>();
        }

        public void SceneLoaderFunc(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void HowToPlayPanel() {
            
            howToPlayPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);

        }

        public void BackToMenu()
        {
            if (!(howToPlayPanel is null)) howToPlayPanel.SetActive(false);
            creditsPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);

        }

        public void CreditsPanel()
        {
            creditsPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);

        }

        public void RestartGame(int sceneIndex)
        {
            PauseMenu.gameIsPaused = false;
            pauseControl.Resume();
            SceneManager.LoadScene(sceneIndex);
            EventSystem.current.SetSelectedGameObject(null);
            


        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
