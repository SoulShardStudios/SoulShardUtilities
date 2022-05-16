using UnityEngine;

namespace SoulShard.Utils
{
    using Math;

    /// <summary>
    /// A component that manages a 1:1 translation of a rectInt to a world position, with a pixels per unit modifier.
    /// </summary>
    [ExecuteInEditMode]
    public class WorldRectInt : MonoBehaviour
    {
        /// <summary>
        /// the pixels per unit of this rectInt
        /// </summary>
        public int pixelsPerUnit = 1;

        /// <summary>
        /// the virtual rect that gets translated to its world position
        /// </summary>
        public RectInt bounds;

        /// <summary>
        /// the in world bounds of the rect
        /// </summary>
        /// <returns>the scaled, translated, and managed rect.</returns>
        public virtual Rect GetWorldBounds()
        {
            RectInt Bounds = GetTranslatedBounds();
            Vector2 min = (Vector2)Bounds.position / pixelsPerUnit;
            Vector2 max = (Vector2)Bounds.size / pixelsPerUnit;
            Rect translatedBounds = new Rect(min, max);
            return translatedBounds;
        }

        /// <summary>
        /// gets the bounds translated by the world position
        /// </summary>
        /// <returns>the translated rectint</sreturns>
        public virtual RectInt GetTranslatedBounds()
        {
            Vector2 worldIntPos =
                (Vector2)transform.position
                - VectorMath.ModVector((Vector2)transform.position, 1f / pixelsPerUnit);
            Vector2Int transformTranslation = VectorMath.RoundVector(worldIntPos * pixelsPerUnit);
            RectInt newbounds = bounds;
            newbounds.position += transformTranslation;
            return newbounds;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            GizmosUtility.DrawRect(GetWorldBounds());
        }
    }
}
