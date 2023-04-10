using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers {
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        public GameObject pauseMenuUI;
        public GameObject howToPlayPanel;
        public GameObject creditsPanel;

        CursorLock cursorLock;
        


        // Update is called once per frame
         void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            gameIsPaused = false;
            Time.timeScale = 1f;
            AudioController.instance.PlayAudio(AudioNames.AudioName.Core_Game_music3D, true, 0.0f);
            howToPlayPanel.SetActive(false);
            creditsPanel.SetActive(false);
            CursorLock.LockCursor();
        }

        // ReSharper disable Unity.PerformanceAnalysis
         void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
            AudioController.instance.PauseAudio(AudioNames.AudioName.Core_Game_music3D, false, 0.0f);
            CursorLock.UnlockCursor();
        }

    }
}
