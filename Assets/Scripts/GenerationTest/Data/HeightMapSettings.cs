using UnityEngine;

[CreateAssetMenu()]
public class HeightMapSettings : UpdateableData
{
    public MapNoiseSettings noiseSettings;

    public bool useFalloff;

    public float heightMulti;
    public AnimationCurve heightCurve;

    public float minHeight
    {
        get
        {
            return heightMulti * heightCurve.Evaluate(0);
        }
    }

    public float maxHeight
    {
        get
        {
            return heightMulti * heightCurve.Evaluate(1);
        }
    }

#if UNITY_EDITOR

    protected override void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }

        base.OnValidate();
    }
    
    #endif
}
