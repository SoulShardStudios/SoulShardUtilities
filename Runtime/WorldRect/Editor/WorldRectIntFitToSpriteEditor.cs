using UnityEditor;
using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// this is simply to allow you to refresh the bounds of the rect
    /// </summary>
    [CustomEditor(typeof(WorldRectIntFitToSprite))]
    public class WorldRectIntFitToSpriteEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            WorldRectIntFitToSprite rect = (WorldRectIntFitToSprite)target;
            if (GUILayout.Button("Refresh"))
                rect.Refresh();
            base.OnInspectorGUI();
        }
    }
}
