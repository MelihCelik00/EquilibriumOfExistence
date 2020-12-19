using UnityEngine;
using UnityEngine.UI;

namespace Eoe.Managers
{
    public class BalanceManager : MonoBehaviour
    {
        [SerializeField] private Slider _equilibriumSlider;
        [SerializeField] private GameObject _player;

        public void ChangeBalance(float value)
        {
            //Debug.Log("SLIDEEEERRRRR\n"+"Value: "+value);
            _equilibriumSlider.value = value;
        }
    
    
    }
}
