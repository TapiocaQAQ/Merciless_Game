// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TempMapGenerator : MonoBehaviour
// {
//     public enum DrawMode {NoiseMap, ColourMap, Mesh};
//     public DrawMode drawMode;

//     public const int mapChunkSize = 241;
//     [Range(1,16)]
//     public int textureDetail;

//     [Range(0,6)]
//     public int levelOfDetail;

//     public float noiseScale;


//     public int octaves;

//     [Range(0,1)]
//     public float persistance;
//     public float lacunarity;


//     public int seed;
//     public Vector2 offset;
//     public float meshHeightMultiplier;
//     public AnimationCurve meshHeightCurve;

//     public bool autoUpdate;
//     public TerrainType[] regions; 
    
// public void TempGenerateMap(){
    
//     float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, 
//                                                     persistance, lacunarity, offset);
//     int colourMapWidth = mapChunkSize*textureDetail;
//     float[,] colourNoiseMap = Noise.GenerateNoiseMap(colourMapWidth, colourMapWidth, seed, noiseScale * textureDetail, octaves, 
//                                                     persistance, lacunarity, offset);
//     Color[] colourMap = new Color[colourMapWidth * colourMapWidth];
//     for(int y = 0; y<colourMapWidth ; y++){
//         for(int x = 0 ; x<colourMapWidth ; x++){
//             float currentHeight = colourNoiseMap[x,y];
//             for(int i = 0; i<regions.Length; i++){
//                 if(currentHeight <= regions[i].height){
//                     colourMap[y * colourMapWidth + x] = regions[i].colour;
//                     break;
//                 }
//             }
//         }
//     }

//     MapDisplay display = FindObjectOfType<MapDisplay> ();
//     if(drawMode == DrawMode.NoiseMap){
//         display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
//     }else if(drawMode == DrawMode.ColourMap){
//         display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, colourMapWidth, colourMapWidth));
//     }else if(drawMode == DrawMode.Mesh){
//         display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), 
//                         TextureGenerator.TextureFromColourMap(colourMap, colourMapWidth, colourMapWidth));
//     }
// }

// void OnValidate(){

//     if(lacunarity < 1){
//         lacunarity = 1;
//     }
//     if(octaves < 0){
//         octaves = 0;
//     }
// }

// }
// [System.Serializable]
// public struct TerrainType{
// public string name;
// public float height;
// public Color colour;

// }
