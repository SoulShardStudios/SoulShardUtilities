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
    }
}
