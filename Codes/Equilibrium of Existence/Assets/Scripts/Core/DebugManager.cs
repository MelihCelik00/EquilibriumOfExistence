﻿using System.Collections;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace Core
{
    public class DebugManager : Singleton<DebugManager>
    {
        public bool invincible = false;
        [SerializeField] private KeyCode editorModeKey = KeyCode.F1;
        [SerializeField] private KeyCode invincibilityKey = KeyCode.F2;
        [SerializeField] private KeyCode restartLevelKey = KeyCode.F4;
        [SerializeField] private KeyCode restartGameKey = KeyCode.F5;
        [SerializeField] private KeyCode exitKey = KeyCode.F12;
        [SerializeField] private KeyCode nextLevelKey = KeyCode.KeypadPlus;
        [SerializeField] private KeyCode previousLevelKey = KeyCode.KeypadMinus;
        
        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            // Exit Game
            if (Input.GetKeyDown(exitKey))
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            // Editör modunu açma / kapama
            if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(editorModeKey)) 
                Settings.Gameplay.IsDebugMode = !Settings.Gameplay.IsDebugMode;

            if (!Settings.Gameplay.IsDebugMode) return;

            // Toggle invincibility
            if (Input.GetKeyDown(invincibilityKey))
            {
                invincible = !invincible;
            }

            // Bölümü yeniden başlat
            if (Input.GetKeyDown(restartLevelKey) 
                || Settings.Gameplay.IsMiddleMouseReset && Input.GetMouseButtonDown(2))
            {
                LevelManager.ResetLevel();
            }

            // Oyunu yeniden başlat
            if (Input.GetKeyDown(restartGameKey)) LevelManager.LoadLevel(0);
            
            // Sonraki bölüme geç
            if (Input.GetKeyDown(nextLevelKey))
            {
                LevelManager.LoadLevel(LevelManager.CurrentSceneIndex + 1);
            }

            // Önceki bölüme geç
            if (Input.GetKeyDown(previousLevelKey))
            {
                LevelManager.LoadLevel(LevelManager.CurrentSceneIndex - 1);
            }
        }
    }
}