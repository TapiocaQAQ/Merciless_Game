using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : ScriptableObject
{
    public bool useFlatShading;
    public bool useFalloff;

	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve;
}
