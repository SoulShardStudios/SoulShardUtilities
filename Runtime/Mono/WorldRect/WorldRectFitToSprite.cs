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

        void Reset() => OnEnable();

        void OnEnable() => Refresh();

        /// <summary>
        /// refrshes the bounds of the rect, and checks for a new sprite.
        /// </summary>
        public void Refresh()
        {
            _renderer = GetComponent<SpriteRenderer>();
            bounds = new Rect(Vector2.zero, _renderer.sprite.rect.size);
        }

        /// <summary>
        /// the in world bounds of the rect
        /// </summary>
        /// <returns>the scaled, translated, and managed rect.</returns>
        public override Rect GetTranslatedBounds()
        {
            Rect translatedBounds = bounds;
            Vector2 absscale = new Vector2(
                Mathf.Abs(transform.localScale.x),
                Mathf.Abs(transform.localScale.y)
            );
            translatedBounds.position +=
                (Vector2)transform.position
                - (_renderer.sprite.pivot / _renderer.sprite.pixelsPerUnit * absscale);
            translatedBounds.size =
                translatedBounds.size / _renderer.sprite.pixelsPerUnit * absscale;
            return translatedBounds;
        }
    }
}
