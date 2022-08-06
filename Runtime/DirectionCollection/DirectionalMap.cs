using UnityEngine;
using System;
using SoulShard.Math;

namespace SoulShard.Utils
{
    /// <summary>
    /// A map of directions to values.
    /// </summary>
    /// <typeparam name="T">The type of values the direction is being mapped to.</typeparam>
    [Serializable]
    public class DirectionalMap<T> : IDirectionalComparer<T>, ICardinalComparer<T>
        where T : IEquatable<T>
    {
        #region Vars
        static Vector2[] _cardinals = VectorConstants.CardianlsVf();
        static Vector2[] _cardinalsAndDiagonals = VectorConstants.CardinalsAndDiagonalsVf();

        public T down;
        public T up;
        public T left;
        public T right;

        public T downLeft;
        public T upLeft;
        public T downRight;
        public T upRight;
        #endregion
        #region Constructors
        public DirectionalMap() { }

        public DirectionalMap(T all) => SetAllToVal(all);

        /// <summary>
        /// Sets all directions to the given value.
        /// </summary>
        /// <param name="val">The value to set all directions to.</param>
        public virtual void SetAllToVal(T val)
        {
            down = val;
            up = val;
            left = val;
            right = val;
            downLeft = val;
            upLeft = val;
            downRight = val;
            upRight = val;
        }

        public DirectionalMap(T down, T left, T right, T up) => SetCardinals(down, left, right, up);

        /// <summary>
        /// Sets all cardinal values.
        /// </summary>
        public virtual void SetCardinals(T down, T left, T right, T up)
        {
            this.down = down;
            this.up = up;
            this.left = left;
            this.right = right;
        }

        public DirectionalMap(
            T down,
            T left,
            T right,
            T up,
            T downLeft,
            T downRight,
            T upLeft,
            T upRight
        ) => SetAll(down, left, right, up, downLeft, downRight, upLeft, upRight);

        /// <summary>
        /// Sets all diagonal and cardinal values.
        /// </summary>
        public virtual void SetAll(
            T down,
            T left,
            T right,
            T up,
            T downLeft,
            T downRight,
            T upLeft,
            T upRight
        )
        {
            this.down = down;
            this.up = up;
            this.left = left;
            this.right = right;
            this.downLeft = downLeft;
            this.downRight = downRight;
            this.upLeft = upLeft;
            this.upRight = upRight;
        }
        #endregion
        #region CardinalFuncs
        public virtual T GetCardinalDir(Vector2 dir)
        {
            VectorMath.BlendVector2(_cardinals, ref dir);
            if (dir == Vector2.zero)
                return down;
            else
                return dir.y == 0
                  ? dir.x > 0
                      ? right
                      : left
                  : dir.y > 0
                      ? up
                      : down;
        }

        public virtual bool CompareCardinalDir(Vector2 dir, T value) =>
            GetCardinalDir(dir).Equals(value);
        #endregion
        #region DirectionalFuncs
        public virtual T GetDir(Vector2 dir)
        {
            VectorMath.BlendVector2(_cardinalsAndDiagonals, ref dir);
            if (dir == Vector2.zero)
                return down;
            if (dir == Vector2.up)
                return up;
            if (dir == Vector2.down)
                return down;
            if (dir == Vector2.left)
                return left;
            if (dir == Vector2.right)
                return right;

            return dir.y > 0
              ? dir.x > 0
                  ? upRight
                  : upLeft
              : dir.x > 0
                  ? downRight
                  : downLeft;
        }

        public virtual bool CompareDir(Vector2 dir, T value) => GetDir(dir).Equals(value);
        #endregion
    }
}
