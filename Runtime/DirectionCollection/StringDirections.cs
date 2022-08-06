using System;
using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// A map of directions to strings.
    /// </summary>
    [Serializable]
    public class StringDirections : DirectionalMap<string>
    {
        public string name;
        bool _applied;

        #region Constructors
        public StringDirections() => SetAllToVal("");

        public StringDirections(string all) : base(all) => ApplyNameToAll(all);

        public StringDirections(string down, string left, string right, string up)
            : base(down, left, right, up) => SetCardinals(down, left, right, up);

        public StringDirections(
            string down,
            string left,
            string right,
            string up,
            string downLeft,
            string downRight,
            string upLeft,
            string upRight
        ) : base(down, left, right, up, downLeft, downRight, upLeft, upRight) =>
            SetAll(down, left, right, up, downLeft, downRight, upLeft, upRight);
        #endregion
        public void ApplyNameToAll(string name)
        {
            if (_applied)
                return;
            if (name == "")
                return;
            if (down != "" && down != null)
                return;
            this.name = name;

            down = name + "Down";
            up = name + "Up";
            left = name + "Left";
            right = name + "Right";

            downLeft = name + "DownLeft";
            downRight = name + "DownRight";
            upLeft = name + "UpLeft";
            upRight = name + "UpRight";
            _applied = true;
        }

        public void Clear()
        {
            name = "";

            down = "";
            up = "";
            right = "";
            left = "";

            downLeft = "";
            downRight = "";
            upLeft = "";
            upRight = "";
        }

        public override string GetCardinalDir(Vector2 dir)
        {
            ApplyNameToAll(name);
            return base.GetCardinalDir(dir);
        }

        public override string GetDir(Vector2 dir)
        {
            ApplyNameToAll(name);
            return base.GetDir(dir);
        }

        public override string ToString() =>
            $"down:{down},up:{up},left:{left},right:{right},downLeft:{downLeft},downRight:{downRight},upLeft:{upLeft},upRight:{upRight}";
    }
}
