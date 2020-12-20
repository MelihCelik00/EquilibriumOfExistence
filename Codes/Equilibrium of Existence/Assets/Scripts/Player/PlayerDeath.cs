using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        int currentLevelIndex;
        float currentBalanceCounter;
        [SerializeField] float timeLimit = 3;

        PlayerBalance playerBalance;
        float maxBalance;
        float balance;
         void Start()
        {
            currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            playerBalance = GetComponent<PlayerBalance>();
            maxBalance = playerBalance.maxBalance;
            balance = playerBalance.balance;

        }
        public void ProcessDeath()
        {
            do
            {

                
                if (currentBalanceCounter >= timeLimit)
                {
                    currentBalanceCounter = 0;
                    ResetLevel();
                    break;
                }

            } while (balance >= maxBalance || balance <= -maxBalance);
  
        }
        

        void ResetLevel()
        {
            SceneManager.LoadScene(currentLevelIndex);
        }

        private void Update()
        {
           
        }
        /*IEnumerator CountBalance()
        {
            currentBalanceCounter += Time.deltaTime;
           // yield return WaitForSeconds(timeLimit);
        }
        */
      
    
    }

    
}