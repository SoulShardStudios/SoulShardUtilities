using NUnit.Framework;
using SoulShard.Utils;
using SoulShard.Math;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GenerationUtilityTests
{
    static readonly StringDirections[] _tiles = new StringDirections[5]
    {
        new StringDirections("_", "_", "_", "_"),
        new StringDirections("+", "+", "+", "_"),
        new StringDirections("_", "+", "+", "+"),
        new StringDirections("+", "_", "+", "+"),
        new StringDirections("+", "+", "_", "+")
    };

    [Test]
    public void TestGenerateAdjacencyRules()
    {
        var res = GenerationUtility.GenerateAdjacencyRules<string, StringDirections>(_tiles);
        for (int i = 0; i < 5; i++)
            foreach (var v in VectorConstants.CardianlsVi())
                foreach (var t in res[_tiles[i]][v])
                    Assert.AreEqual(_tiles[i].GetCardinalDir(v), t.GetCardinalDir(-v));
    }

    [Test]
    public void TestWaveFunctionCollapse()
    {
        // just make sure this simple test case doesn't crash. since this algo is random
        // we cannot deteministically test this without real world exposure.
        var collapse = new Dictionary<Vector2Int, List<StringDirections>>();
        collapse[Vector2Int.zero] = new List<StringDirections>() { _tiles[0] };
        foreach (var v in VectorConstants.CardianlsVi())
            collapse[v] = new List<StringDirections>(_tiles);

        while (!collapse.All((room) => room.Value.Count == 1))
        {
            collapse = GenerationUtility.WaveFunctionCollapse<string, StringDirections>(
                collapse,
                GenerationUtility.GenerateAdjacencyRules<string, StringDirections>(_tiles),
                _tiles,
                new RectInt(-5, -5, 10, 10)
            );
        }
    }
}
