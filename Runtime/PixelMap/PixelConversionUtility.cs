using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using System.Collections.Generic;
using SoulShard.Utils;
using System.Linq;
/// <summary>
/// Converts a texture into pixel format (position,color)
/// its much less compressed, but it needs to be uncompressed
/// to interact with the rest of the pixelmap stuff.
/// </summary>
public static class PixelConversionUtility
{
    /// <summary>
    /// Converts the given texture2d to pixel format.
    /// </summary>
    /// <param name="tex2D">The texture2d to convert.</param>
    /// <returns>The pixel output.</returns>
    public static (Vector2Int[], Color[]) GetPixelsFromTexture2D(Texture2D tex2D) =>
        GetPixelsFromTexture2D(tex2D, new Color[] { Color.clear });

    /// <summary>
    /// Converts the given texture2d to pixel format.
    /// </summary>
    /// <param name="tex2D">The texture2d to convert.</param>
    /// <param name="clearColor">color that should be filtered out of the output, as its considered clear. </param>
    /// <returns>The pixel output.</returns>
    public static (Vector2Int[], Color[]) GetPixelsFromTexture2D(Texture2D tex2D, Color clearColor) =>
        GetPixelsFromTexture2D(tex2D, new Color[] { clearColor });

    /// <summary>
    /// Converts the given texture2d to pixel format.
    /// </summary>
    /// <param name="tex2D">The texture2d to convert.</param>
    /// <param name="clearColors">colors that should be filtered out of the output, as they are "clear". </param>
    /// <returns>The pixel output.</returns>
    public static (Vector2Int[], Color[]) GetPixelsFromTexture2D(Texture2D tex2D, Color[] clearColors)
    {
        NativeArray<Color32> n_tex = tex2D.GetRawTextureData<Color32>();
        List<Color> colors = new List<Color>(0);
        List<Vector2Int> positions = new List<Vector2Int>(0);
        for ( int i = 0; i < n_tex.Length; i++)
        {
            if (_ColorUtility.ConvertColorArrToColor32Arr(clearColors).Contains(n_tex[i]))
                continue;
            positions.Add(CollectionUtility.GetPosition(i, tex2D.width));
            colors.Add(n_tex[i]);
        }
        return (positions.ToArray(), colors.ToArray());
    }

    
}

// this was a jobs accelerated version that worked but was unstable, and you really don't need the extra performance
/*
public static (Vector2Int[], Color[]) GetPixelsFromTexture2D(Texture2D tex2D, Color clearColor)
{
    Debug.Log((tex2D.width, tex2D.height));
    NativeList<Vector2Int> n_positions = new NativeList<Vector2Int>(Allocator.TempJob);
    NativeList<Color> n_colors = new NativeList<Color>(Allocator.TempJob);
    NativeArray<Color32> n_tex = tex2D.GetRawTextureData<Color32>();
    Jobs.ConvertTexture2DToPixels job = new Jobs.ConvertTexture2DToPixels()
    {
        positions = n_positions,
        colors = n_colors,
        texture = n_tex,
        clearColor = clearColor,
        collectionXSize = tex2D.width
    };
    int size = tex2D.width * tex2D.height;
    JobHandle handle = job.Schedule(size,JobUtility.GetBatchAmount(size,10));
    handle.Complete();
    Vector2Int[] positions = n_positions.ToArray();
    Color[] colors = n_colors.ToArray();
    n_positions.Dispose();
    n_colors.Dispose();
    return (positions, colors);
}
static class Jobs
{
    //[BurstCompile]
    public struct ConvertTexture2DToPixels : IJobParallelFor
    {
        [NativeDisableParallelForRestriction]
        public NativeList<Vector2Int> positions;
        [NativeDisableParallelForRestriction]
        public NativeList<Color> colors;
        public NativeArray<Color32> texture;
        public Color32 clearColor;
        public int collectionXSize;
        public void Execute(int index)
        {
            if (ColorOperators.ColorEQ(texture[index], clearColor))
                return;
            positions.Add(CollectionUtility.GetPosition(index, collectionXSize));
            colors.Add(texture[index]);
        }
    }
}
*/
