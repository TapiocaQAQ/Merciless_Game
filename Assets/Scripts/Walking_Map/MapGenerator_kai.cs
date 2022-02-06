using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator_kai : MonoBehaviour
{
    public Noise.NormalizeMode normalizeMode;
    const int mapSize = 10;
    const int mapChunkSize = 64;
    [Range(0,6)]
    public int meshSimplify;

    [Range(1,16)]
    public int textureDetail;

    public float noiseScale;
    public int octaves;

    [Range(0,1)]
    public float persistance;
    public float lacunarity;


    public int seed;
    public Vector2 offset;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;
    public TerrainType[] regions; 

    private MeshData[,] meshList;
    private Texture2D[,] textureList;
    private ArrayList objList = new ArrayList(); 

    
    void Start(){

    }
        
    public void GenerateMap(){
        meshList = new MeshData[mapSize, mapSize];
        textureList = new Texture2D[mapSize, mapSize];
        int colourMapWidth = (mapChunkSize)*textureDetail;
        float[,] noiseMap = Noise.GenerateNoiseMap(mapSize*mapChunkSize+1, mapSize*mapChunkSize+1, seed, noiseScale, octaves, persistance, lacunarity, offset, normalizeMode);
        float[,] colourNoiseMap = Noise.GenerateNoiseMap(mapSize*colourMapWidth, mapSize*colourMapWidth, seed, noiseScale * textureDetail, octaves, persistance, lacunarity, offset, normalizeMode);
        GameObject map = new GameObject("map");

        for(int y = 0; y<mapSize ; y++) for(int x = 0 ; x<mapSize ; x++){

            // generate all mesh chunks
            meshList[x, y] = MeshGenerator.GenerateTerrainMesh(
                clipNoiseMap(noiseMap, x*mapChunkSize, y*mapChunkSize, mapChunkSize+1),
                meshHeightMultiplier, meshHeightCurve, meshSimplify
            );

            // generate all mesh textures
            Color[] colorMap = new Color[colourMapWidth * colourMapWidth];
            for(int colorY = 0; colorY<colourMapWidth; colorY++) for(int colorX = 0; colorX<colourMapWidth; colorX++){
                float currentHeight = colourNoiseMap[x*colourMapWidth + colorX, y*colourMapWidth + colorY];
                for(int i = 0; i<regions.Length; i++){
                    if(currentHeight <= regions[i].height){
                        colorMap[colorY * colourMapWidth + colorX] = regions[i].colour;
                        break;
                    }
                }
            }
            textureList[x, y] = TextureGenerator.TextureFromColourMap(colorMap, colourMapWidth, colourMapWidth);

            // Create all map chunks
            GameObject mapChunk = new GameObject("mapChunk[ "+x+" , "+(mapSize-y)+" ]");
            mapChunk.transform.SetParent(map.transform);
            mapChunk.transform.position = new Vector3(x*mapChunkSize, 0, (mapSize-y)*mapChunkSize);
            MeshFilter meshFilter = mapChunk.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = meshList[x, y].CreateMesh();
            MeshCollider meshCollider = mapChunk.AddComponent<MeshCollider>();
            MeshRenderer meshRenderer = mapChunk.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            meshRenderer.sharedMaterial.mainTexture = textureList[x, y];
        }
    }

    private float[,] clipNoiseMap(float[,] noiseMap, int startX, int startY, int len){
        float[,] newMap = new float[len, len];
        for(int x=0; x < len; x++) for(int y=0; y < len; y++){
            newMap[x, y] = noiseMap[startX+x, startY+y];
        }
        return newMap;
    }

}