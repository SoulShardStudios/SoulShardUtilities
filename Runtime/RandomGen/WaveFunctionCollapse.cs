using System.Collections.Generic;
using UnityEngine;
using SoulShard.Math;
using System.Linq;

namespace SoulShard.Utils
{
    /// <summary>
    /// Contains various helper methods for random generation
    /// </summary>
    public static class GenerationUtility
    {
        static Vector2Int[] _cardinals = VectorConstants.CardianlsVi();

        /// <summary>
        /// An implementation of the wave function collapse algorithm.
        /// Feed the result of this algorithm into itself untill all of
        /// the returned lists in the dictionary are of length 1.
        ///
        /// You can use a while loop to do this, although if there is an "impossible tile"
        /// that cannot be filled by the algorithm you will crash the program.
        /// </summary>
        /// <typeparam name="_eq">The type that implements IEquateable</typeparam>
        /// <typeparam name="_cardEq">The type that implements ICardinalComparer,
        /// this is how the algorithm determines if two tiles can fit together.</typeparam>
        /// <param name="toCollapse">The dictionary of tiles to apply this algorithm to.</param>
        /// <param name="adjacencyRules">The rules for how tiles can fit together.</param>
        /// <param name="allTiles">A collection of all of the tiles.</param>
        /// <param name="bounds">The bounds of the algorithm (can only run on finite spaces)</param>
        /// <returns>A new dictionary with the updated superpositions of every room.</returns>
        public static Dictionary<Vector2Int, List<_cardEq>> WaveFunctionCollapse<_eq, _cardEq>(
            Dictionary<Vector2Int, List<_cardEq>> toCollapse,
            Dictionary<_cardEq, Dictionary<Vector2Int, List<_cardEq>>> adjacencyRules,
            _cardEq[] allTiles,
            RectInt bounds
        )
            where _eq : System.IEquatable<_eq>
            where _cardEq : ICardinalComparer<_eq>
        {
            var least = int.MaxValue;
            var lowest = Vector2Int.zero;
            foreach (var kvp in toCollapse)
            {
                if (kvp.Value.Count < least && kvp.Value.Count > 1)
                {
                    least = kvp.Value.Count;
                    lowest = kvp.Key;
                }
            }
            if (least != int.MaxValue)
                toCollapse[lowest] = new List<_cardEq>()
                {
                    toCollapse[lowest][Random.Range(0, toCollapse[lowest].Count)]
                };

            var @new = new Dictionary<Vector2Int, List<_cardEq>>(toCollapse);

            void PropegateConstraints(
                HashSet<_cardEq> possible,
                Vector2Int other,
                Vector2Int opposite
            )
            {
                if (!bounds.Contains(other))
                    return;

                if (!@new.ContainsKey(other))
                    @new[other] = new List<_cardEq>(allTiles);

                var valid = new HashSet<_cardEq>();
                foreach (var r in @new[other])
                    valid.UnionWith(adjacencyRules[r][opposite]);
                possible.RemoveWhere((r) => !valid.Contains(r));
            }

            foreach (var kvp in toCollapse)
            {
                if (kvp.Value.Count <= 1)
                    continue;
                HashSet<_cardEq> possible = new HashSet<_cardEq>(allTiles);
                foreach (var v in _cardinals)
                    PropegateConstraints(possible, v + kvp.Key, -v);
                @new[kvp.Key] = possible.ToList();
            }
            toCollapse = @new;
            return toCollapse;
        }

        /// <summary>
        /// Generates adjacency rules for the wave function collapse algorithm automatically.
        /// </summary>
        /// <typeparam name="_eq">The type that implements IEquateable</typeparam>
        /// <typeparam name="_cardEq">The type that implements ICardinalComparer,
        /// this is how the algorithm determines if two tiles can fit together.</typeparam>
        /// <param name="allTiles">A collection of all of the tiles.</param>
        /// <returns>A dictionary mapping of which tiles can fit to each edge of each other tile.</returns>
        public static Dictionary<
            _cardEq,
            Dictionary<Vector2Int, List<_cardEq>>
        > GenerateAdjacencyRules<_eq, _cardEq>(_cardEq[] allTiles)
            where _eq : System.IEquatable<_eq>
            where _cardEq : ICardinalComparer<_eq>
        {
            var res = new Dictionary<_cardEq, Dictionary<Vector2Int, List<_cardEq>>>();
            foreach (var r in allTiles)
                res[r] = new Dictionary<Vector2Int, List<_cardEq>>();
            foreach (var r in allTiles)
                foreach (var v in _cardinals)
                    res[r][v] = allTiles
                        .Where((r2) => r.CompareCardinalDir(v, r2.GetCardinalDir(-v)))
                        .ToList();
            return res;
        }
    }
}
