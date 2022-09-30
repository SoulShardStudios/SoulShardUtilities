using UnityEditor;
using UnityEngine;
namespace SoulShard.Utils
{
    [CustomEditor(typeof(EntitySpawner2D)), CanEditMultipleObjects]
    public class EntitySpawner2DEditor : UnityEditor.Editor
    {
        public void OnSceneGUI()
        {
            EditorGUI.BeginChangeCheck();
            var spawner = (EntitySpawner2D)target;
            var len = spawner.spawnArea.Length;
            if (spawner.spawnArea == null)
                return;

            var v3 = new Vector3[len];
            for (int i = 0; i < len; i++)
                v3[i] = (Vector3)spawner.spawnArea[i];
            Handles.color = new Color(1, 1, 1, 0.1f);
            Handles.DrawAAConvexPolygon(v3);
            var v2 = new Vector2[len];
            for (int i = 0; i < len; i++)
            {
                v2[i] = Handles.PositionHandle(
                        spawner.spawnArea[i],
                        Quaternion.identity
                    );
            }
            EditorGUI.EndChangeCheck();
            Undo.RecordObject(spawner, "edit spawnArea positions");
            spawner.spawnArea = v2;
        }

        public override void OnInspectorGUI()
        {
            var spawner = (EntitySpawner2D)target;
            if (GUILayout.Button("Spawn Entities"))
                spawner.SpawnAllProps();
            base.OnInspectorGUI();
        }
    }
}
