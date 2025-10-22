using UnityEngine;

[CreateAssetMenu()]
public class MeshSettings : UpdateableData
{
    public const int numSupportedLODs = 5;
    public const int numSupportedChunkSizes = 9;
    public const int numSupportedFlatshadedChunkSizes = 3;

    public static readonly int[] supportedChunkSizes = { 48, 72, 96, 120, 144, 168, 192, 216, 240 };

    public float meshScale = 2f;

    public bool useFlatShading;

    [Range(0, numSupportedChunkSizes - 1)] public int chunkSizeIndex;
    [Range(0, numSupportedFlatshadedChunkSizes - 1)] public int flatshadedChunkSizeIndex;

    [Range(0, numSupportedLODs - 1)] public int previewLevelOfDetail;

    // Number of vertices per line (with mesh rendered at LOD of zero). Includes 2 extra vertices used for normals calculations
    public int numVerticesPerLine
    {
        get
        {
            return supportedChunkSizes[(useFlatShading)?flatshadedChunkSizeIndex:chunkSizeIndex] + 1;
        }
    }

    public float meshWorldSize
    {
        get
        {
            return (numVerticesPerLine - 3) * meshScale;
        }
    }
}
