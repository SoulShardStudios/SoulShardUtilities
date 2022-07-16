using UnityEngine;
using UnityEditor;
using SoulShard.Utils;
/// <summary>
/// Fast automation for randomly spawning entities over an area in a natural way.
/// </summary>
public class EntitySpawner2D : MonoBehaviour
{
    /// <summary>
    /// The transform parent to spawn the entites to.
    /// </summary>
    [SerializeField]
    Transform _spawnParent;
    /// <summary>
    /// The collection of entities that could be spawned (Selected randomly)
    /// </summary>
    [SerializeField]
    GameObject[] _entities;
    /// <summary>
    /// The size of the area to spawn entities in.
    /// </summary>
    [SerializeField]
    Vector2 _spawnSize;
    /// <summary>
    /// The number of attempts to spawn an entity that should be performed.
    /// </summary>
    [SerializeField]
    Vector2 _spawnAmount;
    /// <summary>
    /// The minimum distance away from other entities inside of the transform parent that new entities must be.
    /// </summary>
    [SerializeField]
    float _minimumDistance;
    /// <summary>
    /// With the parameters on this monobehavior, spawn some entities.
    /// </summary>
    public void SpawnEntities()
    {
        if (_entities.Length == 0)
        {
            Debug.LogError("No entities were provided to the spawner.");
            return;
        }
        for (int i = 0; i < Mathf.Ceil(Random.Range(_spawnAmount.x, _spawnAmount.y)); i++)
        {
            var pos = _randomPos;
            bool distanced = true;
            for (int t = 0; t < _spawnParent.childCount; t++)
            {
                if (Vector2.Distance(_spawnParent.GetChild(t).position, pos) < _minimumDistance)
                {
                    distanced = false;
                    break;
                }
            }
            if (!distanced)
                continue;
            GameObject entity = _entities[Random.Range(0, _entities.Length)];
            GameObject g = (GameObject)PrefabUtility.InstantiatePrefab(entity, _spawnParent.transform);
            if (g == null)
                g = Instantiate(entity, _spawnParent.transform);
            g.transform.position = pos;
        }
    }
    void OnDrawGizmos() => GizmosUtility.DrawRect(new Rect(transform.position, _spawnSize));
    Vector2 _randomPos => transform.position + new Vector3(Random.Range(0, _spawnSize.x), Random.Range(0, _spawnSize.y));
}