using UnityEngine;

namespace Independents
{
    public class SpriteRandomization : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        private SpriteRenderer _sprRenderer;

        public void Start()
        {
            _sprRenderer = GetComponent<SpriteRenderer>();
            RandomizeSprites();
        }

        private void RandomizeSprites()
        {
            var rnd = Random.Range(0, sprites.Length);
            _sprRenderer.sprite = sprites[rnd];
        }
    }
}