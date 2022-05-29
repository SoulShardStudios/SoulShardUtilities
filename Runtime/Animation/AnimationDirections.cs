using UnityEngine;

namespace SoulShard.Animations
{
    [System.Serializable]
    /// <summary>
    /// The names of animations for a four or eight direction animation.
    /// Also allows for easy lookup of these direction based animations with a lookup function.
    /// </summary>
    public struct AnimDirections
    {
        public string name;

        public string down;
        public string up;
        public string left;
        public string right;

        public string downLeft;
        public string upLeft;
        public string downRight;
        public string upRight;

        /// <summary>
        /// Applies a name to all animation components.
        /// EX: new AnimDirections().ApplyNameToAll("Walk").down == "WalkDown"
        /// e.t.c. for all other directions.
        /// </summary>
        /// <param name="name">The name to apply to all animation components</param>
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

        /// <summary>
        /// Clears all animation names.
        /// </summary>
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

        /// <summary>
        /// Gets the name for a given direction but only for the cardinals.
        /// </summary>
        /// <param name="direction">The sirection to get the name of.</param>
        /// <returns>The name of the direction.</returns>
        public string NameForDirCardinal(Vector2 direction)
        {
            ApplyNameToAll(name);
            if (direction == Vector2.zero)
                return down;
            else
                return direction.y == 0
                  ? direction.x > 0
                      ? right
                      : left
                  : direction.y > 0
                      ? up
                      : down;
        }

        /// <summary>
        /// Gets the name for a given direction for all cardinals and diagonals.
        /// </summary>
        /// <param name="direction">The direction to get the name of.</param>
        /// <returns>The name of the direction.</returns>
        public string NameForDir(Vector2 direction)
        {
            ApplyNameToAll(name);
            if (direction == Vector2.zero)
                return down;
            if (direction == Vector2.up)
                return up;
            if (direction == Vector2.down)
                return down;
            if (direction == Vector2.left)
                return left;
            if (direction == Vector2.right)
                return right;

            return direction.y > 0
              ? direction.x > 0
                  ? upRight
                  : upLeft
              : direction.x > 0
                  ? downRight
                  : downLeft;
        }
    }
}
