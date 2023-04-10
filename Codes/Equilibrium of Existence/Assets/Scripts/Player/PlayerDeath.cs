using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using UnityEngine.UI;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        int currentLevelIndex;
        float currentBalanceCounter;
        public GameObject deathPanel;
        public bool isDead = false;
        

        PlayerBalance playerBalance;
        void Start()
        {
            currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
          
        }

         private void Update()
         {
             //ProcessDeath();
         }

         public void ProcessDeath()
        {
            Debug.Log("processing");
            isDead = true;
            Time.timeScale = 0f;
            deathPanel.SetActive(true);
            AudioController.instance.PlayAudio(AudioNames.AudioName.Death_SFX, false, 0f);
            StartCoroutine(deathCoroutine());              
            
        }
        public IEnumerator deathCoroutine()
        {
              
            yield return new WaitForSecondsRealtime(3);
            Time.timeScale = 1f;
            LevelManager.ResetLevel();
            deathPanel.SetActive(false);
            isDead = false;
            
        }
       
        public void ResetLevel()
        {
           /* Debug.Log("Reset level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);*/
        }
        
       
        
    }

    
}