using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _equilibriumSlider;
    [SerializeField] private GameObject _player;

    private void Update()
    {
        _equilibriumSlider.value = _player.transform.position.x;
    }
}
