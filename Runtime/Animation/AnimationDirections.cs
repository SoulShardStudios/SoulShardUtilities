namespace SoulShard.Animations
{
    [System.Serializable]
    /// <summary>
    /// The names of animations for a four or eight direction animation.
    /// </summary>
    public struct AnimDirections
    {

        // Cardinals.
        public string down;
        public string up;
        public string left;
        public string right;

        // Diagonals.
        public string downLeft;
        public string upLeft;
        public string downRight;
        public string upRight;
        public void ApplyNameToAll(string name)
        {
            if (name == "")
                return;
            if (down != "")
                return;

            // Cardinals.
            down = name + "Down";
            up = name + "Up";
            left = name + "Left";
            right = name + "Right";

            // Diagonals.
            downLeft = name + "DownLeft";
            downRight = name + "DownRight";
            upLeft = name + "UpLeft";
            upRight = name + "UpRight";
        }
    }
}