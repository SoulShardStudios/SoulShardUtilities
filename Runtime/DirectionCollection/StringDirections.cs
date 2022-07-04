using System;

namespace SoulShard.Utils
{
    /// <summary>
    /// A map of directions to strings.
    /// </summary>
    [Serializable]
    public class StringDirections : DirectionalMap<string>
    {
        public string name;

        public void ApplyNameToAll(string name)
        {
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
    }
}
