using UnityEngine;
namespace SoulShard.Utils
{
    using Math;
    /// <summary>
    /// fits a world rect int to a sprite
    /// </summary>
    [ExecuteInEditMode]
    public class WorldRectIntFitToSprite : WorldRectInt
    {
        SpriteRenderer _renderer;
        private void OnEnable() => Refresh();
        /// <summary>
        /// refrshes the bounds of the rect, and checks for a new sprite.
        /// </summary>
        public void Refresh()
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (_renderer == null)
                return;
            if (_renderer.sprite == null)
            {
                bounds = new RectInt(0, 0, 0, 0);
                return;
            }
            var rect = _renderer.sprite.rect;
            Vector2Int size = VectorMath.RoundVector(rect.size);
            Vector2Int position = VectorMath.RoundVector(_renderer.sprite.pivot);
            bounds = new RectInt(-position, size);
        }
    }
}