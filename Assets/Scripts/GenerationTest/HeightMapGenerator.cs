using Unity.Mathematics;
using UnityEngine;

public static class HeightMapGenerator
{
    static float[,] falloffMap;

    public static HeightMap GenerateHeightMap(int width, int height, HeightMapSettings settings, Vector2 sampleCenter)
    {
        float[,] values = Noise.NoiseMapGeneration(width, height, settings.noiseSettings, sampleCenter);

        AnimationCurve heightCurve_threadsafe = new AnimationCurve(settings.heightCurve.keys);

        if (settings.useFalloff)
        {
            if (falloffMap == null || falloffMap.GetLength(0) != width) falloffMap = FalloffGenerator.GenerateFalloffMap(width);
        }

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (settings.useFalloff) values[i, j] = Mathf.Clamp01(values[i, j] - falloffMap[i, j]);

                values[i,j] *= heightCurve_threadsafe.Evaluate(values[i,j]) * settings.heightMulti;

                if(values[i,j] > maxValue) maxValue = values[i,j];

                if(values[i,j] < minValue) minValue = values[i,j];
            }
        }

        return new HeightMap(values, minValue, maxValue);
    }
}

public struct HeightMap
{
    public readonly float[,] values;
    public readonly float minValue;
    public readonly float maxValue;

    public HeightMap(float[,] values, float minValue, float maxValue)
    {
        this.values = values;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }
}
