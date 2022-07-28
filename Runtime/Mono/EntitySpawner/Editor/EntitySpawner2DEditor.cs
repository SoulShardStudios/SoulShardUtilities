using UnityEditor;
using UnityEngine;

namespace SoulShard.Utils
{
    [CustomEditor(typeof(EntitySpawner2D))]
    public class EntitySpawner2DEditor : Editor
    {
        Vector2? prevpos = null;

        public void OnSceneGUI()
        {
            var spawner = target as EntitySpawner2D;
            var v3 = new Vector3[spawner.spawnArea.Length];
            for (int i = 0; i < spawner.spawnArea.Length; i++)
                v3[i] = (Vector3)spawner.spawnArea[i];
            Handles.color = new Color(1, 1, 1, 0.1f);
            Handles.DrawAAConvexPolygon(v3);
            for (int i = 0; i < spawner.spawnArea.Length; i++)
                spawner.spawnArea[i] = Handles.PositionHandle(
                    spawner.spawnArea[i]
                        + (
                            prevpos.HasValue
                                ? ((Vector2)spawner.transform.position - prevpos.Value)
                                : Vector2.zero
                        ),
                    Quaternion.identity
                );
            prevpos = spawner.transform.position;
        }

        public override void OnInspectorGUI()
        {
            var spawner = target as EntitySpawner2D;
            if (GUILayout.Button("Spawn Entities"))
                spawner.SpawnAllProps();
            base.OnInspectorGUI();
        }
    }
}
