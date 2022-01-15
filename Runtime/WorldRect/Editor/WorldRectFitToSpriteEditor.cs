using UnityEditor;
using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// this is simply to allow you to refresh the bounds of the rect
    /// </summary>
    [CustomEditor(typeof(WorldRectFitToSprite))]
    public class WorldRectFitToSpriteEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            WorldRectFitToSprite rect = (WorldRectFitToSprite)target;
            if (GUILayout.Button("Refresh"))
                rect.Refresh();
            base.OnInspectorGUI();
        }
    }
}
