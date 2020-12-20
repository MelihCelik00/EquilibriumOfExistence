using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject _balanceSlider;

        private void Start()
        {
            DisableAndEnable();
        }

        private void DisableAndEnable()
        {
            StartCoroutine(Wait(2));
        }


        private IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
            
            _balanceSlider.SetActive(false);
            
            yield return new WaitForSeconds(time*2);
            
            _balanceSlider.SetActive(true);

            DisableAndEnable();
        }

    }
}
