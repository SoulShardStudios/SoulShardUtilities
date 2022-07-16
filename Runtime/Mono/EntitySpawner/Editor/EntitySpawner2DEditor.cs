using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntitySpawner2D))]
public class EntitySpawner2DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var spawner = (EntitySpawner2D)target;
        if (GUILayout.Button("Spawn Entities"))
            spawner.SpawnEntities();
        base.OnInspectorGUI();
    }
}