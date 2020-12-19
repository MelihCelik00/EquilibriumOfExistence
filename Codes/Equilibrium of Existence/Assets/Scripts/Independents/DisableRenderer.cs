using UnityEngine;

namespace Eoe.Independents
{
    // Editörde görünmesi gereken ama oyunda gözükmemesi gereken objelerin scripti
    public class DisableRenderer : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}