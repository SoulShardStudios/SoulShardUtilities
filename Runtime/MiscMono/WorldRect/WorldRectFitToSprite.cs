using UnityEngine;
using SoulShard.Math;
namespace SoulShard.Utils
{
    /// <summary>
    /// a world rect fit to the sprite on its associated object.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class WorldRectFitToSprite : WorldRect
    {
        SpriteRenderer _renderer;
        void OnEnable()
        {
            _renderer = GetComponent<SpriteRenderer>();
            Refresh();
        }
        /// <summary>
        /// call this method when the sprite is changed so the rect can update.
        /// </summary>
        public void Refresh() => bounds = new Rect(Vector2.zero, _renderer.sprite.rect.size);
        void Reset() => OnEnable();
        /// <summary>
        /// the in world bounds of the rect
        /// </summary>
        /// <returns>the scaled, translated, and managed rect.</returns>
        public override Rect GetTranslatedBounds()
        {
            Rect translatedBounds = bounds;
            Vector2 absscale = VectorMath.AbsVector(transform.localScale);
            translatedBounds.position += (Vector2)transform.position - (_renderer.sprite.pivot / _renderer.sprite.pixelsPerUnit * absscale);
            translatedBounds.size = translatedBounds.size / _renderer.sprite.pixelsPerUnit * absscale;
            return translatedBounds;
        }
    }
}