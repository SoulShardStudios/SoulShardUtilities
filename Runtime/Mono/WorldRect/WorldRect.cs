using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// A component that manages a 1:1 translation of a rect to a world position.
    /// </summary>
    [ExecuteInEditMode]
    public class WorldRect : MonoBehaviour
    {
        /// <summary>
        /// the virtual rect that gets translated to its world position
        /// </summary>
        public Rect bounds;

        /// <summary>
        /// the in world bounds of the rect
        /// </summary>
        /// <returns>the scaled, translated, and managed rect.</returns>
        public virtual Rect GetTranslatedBounds()
        {
            Rect translatedBounds = bounds;
            Vector2 absscale = Math.VectorMath.AbsVector(transform.localScale);
            translatedBounds.position +=
                (Vector2)transform.position - translatedBounds.size / 2 * absscale;
            translatedBounds.size *= absscale;
            return translatedBounds;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            GizmosUtility.DrawRect(GetTranslatedBounds());
        }
    }
}
