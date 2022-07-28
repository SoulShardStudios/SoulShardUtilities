using UnityEngine;
using UnityEditor;

/// <summary>
/// Fast automation for randomly spawning entities over an area in a natural way.
/// </summary>
public class EntitySpawner2D : MonoBehaviour
{
    /// <summary>
    /// The properties for each stage of spawning.
    /// </summary>
    [System.Serializable]
    public class Props
    {
        /// <summary>
        /// The collection of entities that could be spawned (Selected randomly)
        /// </summary>
        public GameObject[] entities;

        /// <summary>
        /// The number of attempts to spawn an entity that should be performed.
        /// </summary>
        public Vector2 spawnAmount;

        /// <summary>
        /// The minimum distance away from other entities inside of the transform parent that new entities must be.
        /// </summary>
        public float minimumDistance;
    }

    /// <summary>
    /// The list of properties of each stage of spawning.
    /// Goes from the first element to the last and spawns using those parameters in order.
    /// </summary>
    [SerializeField]
    Props[] _props;

    /// <summary>
    /// The transform parent to spawn the entites to.
    /// </summary>
    [SerializeField]
    Transform _spawnParent;

    /// <summary>
    /// The polygon in which to spawn the entities
    /// </summary>
    public Vector2[] spawnArea;

    /// <summary>
    /// Should the script spawn entities on enable?
    /// </summary>
    [SerializeField]
    bool _spawnOnEnable;

    void OnEnable()
    {
        if (_spawnOnEnable)
            SpawnAllProps();
    }

    bool PointIsValid(Vector2 point, float minDist)
    {
        for (int t = 0; t < _spawnParent.childCount; t++)
            if (Vector2.Distance(_spawnParent.GetChild(t).position, point) < minDist)
                return false;
        return true;
    }

    /// <summary>
    /// With the parameters on this monobehavior, spawn some entities.
    /// </summary>
    public void SpawnAllProps()
    {
        foreach (Props p in _props)
            SpawnEntities(p);
    }

    void SpawnEntities(Props props)
    {
        if (props.entities.Length == 0)
        {
            Debug.LogError("No entities were provided to the spawner.");
            return;
        }
        Mesh mesh = ConvexPointsToMesh2D(spawnArea, 0);
        for (int i = 0; i < Mathf.Ceil(Random.Range(props.spawnAmount.x, props.spawnAmount.y)); i++)
        {
            var pos = GetRandomPointOnMesh(mesh);
            if (!PointIsValid(pos, props.minimumDistance))
                continue;
            GameObject entity = props.entities[Random.Range(0, props.entities.Length)];
            GameObject g = (GameObject)PrefabUtility.InstantiatePrefab(
                entity,
                _spawnParent.transform
            );
            if (g == null)
                g = Instantiate(entity, _spawnParent.transform);
            g.transform.position = pos;
        }
    }

    public static Mesh ConvexPointsToMesh2D(Vector2[] points, float z)
    {
        var mesh = new Mesh();
        var pts = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
            pts[i] = new Vector3(points[i].x, points[i].y, z);

        mesh.vertices = pts;
        var triCount = points.Length - 2;
        int[] tris = new int[triCount * 3];
        for (int i = 0; i < triCount; i++)
        {
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = i + 2;
        }

        mesh.triangles = tris;
        return mesh;
    }

    public static Vector3 GetRandomPointOnTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        float r = Random.value;
        float s = Random.value;

        if (r + s >= 1)
        {
            r = 1 - r;
            s = 1 - s;
        }

        Vector3 pointOnMesh = a + r * (b - a) + s * (c - a);
        return pointOnMesh;
    }

    public static Vector3 GetRandomPointOnMesh(Mesh mesh)
    {
        int tris = (mesh.triangles.Length / 3) - 1;
        int triIndex = Mathf.RoundToInt(tris * Random.value);

        Vector3 a = mesh.vertices[mesh.triangles[triIndex * 3]];
        Vector3 b = mesh.vertices[mesh.triangles[triIndex * 3 + 1]];
        Vector3 c = mesh.vertices[mesh.triangles[triIndex * 3 + 2]];

        return GetRandomPointOnTriangle(a, b, c);
    }
}
