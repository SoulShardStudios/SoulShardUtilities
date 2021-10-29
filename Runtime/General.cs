using UnityEngine;
#pragma warning disable
namespace SoulShard.Utils
{
    public static class General
    {
        // gets the proper texture2d from a spritesheet or regular sprite
        public static Texture2D GetTextureFromSprite(Sprite Sprite)
        {
            if (Sprite != null)
            {
                var @return = new Texture2D((int)Sprite.textureRect.width, (int)Sprite.textureRect.height);
                var pixels = Sprite.texture.GetPixels((int)Sprite.textureRect.x, (int)Sprite.textureRect.y, (int)Sprite.textureRect.width, (int)Sprite.textureRect.height);
                @return.SetPixels(pixels);
                @return.Apply();
                @return.name = Sprite.name;
                return @return;
            }
            return null;
        }
        // gets the current image, and sets the pivot either to a specific position, or the height of the pixel closest to the bottom of the image
        public static Sprite SetPivot(Sprite S, Vector2 Pivot) { return Sprite.Create(S.texture, new Rect(0, 0, S.texture.width, S.texture.height), Pivot, 1); }
        public static Sprite SetPivotBottomMost(Sprite S)
        {
            Texture2D T = S.texture;
            Color[] colors = T.GetPixels();
            float YPos = 0;
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i].a != 0)
                {
                    YPos = (((float)i / (float)T.width) / (float)T.height);
                    break;
                }
            }
            return Sprite.Create(T, new Rect(0, 0, T.width, T.height), new Vector2(0.5f, YPos), 1);
        }
        // returns a new 2d array, initialized with a default value.
        public static T[,] GenerateNew2dArray<T>(int xLength, int yLength, T defaultValue) where T : new()
        {
            T[,] @return = new T[xLength, yLength];
            if (defaultValue == null)
                return null;
            for (int i = 0; i < xLength; i++)
            {
                for (int e = 0; e < yLength; e++)
                    @return[i, e] = defaultValue;
            }
            return @return;
        }

        // generates a new 2d array.
        public static T[,] GenerateNew2dArray<T>(int xLength, int yLength) => new T[xLength, yLength];
        // generates a new array with a default value
        public static T[] GenerateNewArray<T>(int length, T defaultValue)
        {
            T[] @return = new T[length];
            for (int i = 0; i < length; i++)
                @return[i] = defaultValue;
            return @return;
        }
        // gets a specific component from every gameobject in the list, and returns a list of the components.
        public static T[] GetComponentFromGameObjectList<T>(GameObject[] toGetComponentFrom) where T : MonoBehaviour
        {
            T[] @return = new T[toGetComponentFrom.Length];
            for (int i = 0; i < toGetComponentFrom.Length; i++)
                @return[i] = toGetComponentFrom[i].GetComponent<T>();
            return @return;
        }
        // takes in a boolean list and checks if all the values are the same
        public static bool? ListIsRepeatedValues<T>(T[] list) where T: System.IEquatable<T>
        {
            if (list.Length == 0)
                return null;
            T start = list[0];
            foreach (T t in list)
                if (!t.Equals(start))
                    return false;
            return true;
        }
        // creates a boundsint out of a start and end coordinate (ask @lubba64#5426 for why this is used)
        public static BoundsInt CreateBoundsInt(Vector3Int start, Vector3Int end)
        {
            Vector3Int realStart = new Vector3Int(start.x < end.x ? start.x : end.x, start.y < end.y ? start.y : end.y, 0);
            Vector3Int realEnd = new Vector3Int(start.x > end.x ? start.x : end.x, start.y > end.y ? start.y : end.y, 0);
            Vector3Int size = realEnd - realStart + new Vector3Int(0, 0, 1);
            return new BoundsInt(realStart, size);
        }
    }
}