using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain_generator : MonoBehaviour
{
    public Terrain terrain;
    TerrainData terrainData;
    float[,] heightsMap;
    int size;
    Vector2 playerSpawnCoord;

    // Start is called before the first frame update
    void Start()
    {
        float maxHeight = -1, minHeight = 1;

        // Generate new height map for the terrain 
        terrainData = terrain.terrainData;
        size = terrainData.heightmapResolution;
        heightsMap = terrainData.GetHeights(0, 0, size, size);
        playerSpawnCoord = WorldPos2TerrainCoord(terrain, new Vector3(Config.playerSpawnX, 0, Config.playerSpawnZ));

        for(int x=0; x<size; x++) for(int y=0; y<size; y++){
            // apply perlin noise to the entire map
            heightsMap[x, y] = Mathf.PerlinNoise(x*1.0f/size, y*1.0f/size) * 0.1f;
            heightsMap[x, y] += Mathf.PerlinNoise(x*10.0f/size, y*10.0f/size) * 0.05f;

            // flat out the terrain near player spawn point
            float dist2spawn = Vector2.Distance(new Vector2(x, y), playerSpawnCoord);
            if( dist2spawn<5){
                heightsMap[x, y] = 0.05f;
            }else if(dist2spawn<15){
                heightsMap[x, y] -= 0.05f;
                heightsMap[x, y] *= (dist2spawn-5)/10;
                heightsMap[x, y] += 0.05f;
            }

            if(heightsMap[x, y] > maxHeight) maxHeight = heightsMap[x, y];
            if(heightsMap[x, y] < minHeight) minHeight = heightsMap[x, y];
        }
        terrain.terrainData.SetHeights(0, 0, heightsMap);

        // Color the terrain 
        Color[,] colorMap= new Color[size, size];
        float sandHeight = minHeight + (maxHeight - minHeight)*0.3f;
        float treeHeight = minHeight + (maxHeight - minHeight)*0.6f;
        for(int x=0; x<size; x++) for(int y=0; y<size; y++){
            if(heightsMap[x, y] < sandHeight){
                colorMap[x, y] = new Color(1.0f, 1.0f, 0.3f);
            }else if(heightsMap[x, y] < treeHeight){
                colorMap[x, y] = new Color(0.3f, 1.0f, 0.3f);
            }else{
                colorMap[x, y] = new Color(0.3f, 0.3f, 0.3f);
            }
        }
        //terrainData.terrainLayers[0].diffuseTexture = generateTexture2D(colorMap);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // turn world position into terrain coordinate
    public static Vector2 WorldPos2TerrainCoord(Terrain terrain, Vector3 worldPosition){
        var terrainPosition = terrain.transform.position;
        TerrainData terrainData = terrain.terrainData;
        var terrainSize = terrainData.size;
        float relativeHitTerX = (worldPosition.x - terrainPosition.x) / terrainSize.x;
        float relativeHitTerZ = (worldPosition.z - terrainPosition.z) / terrainSize.z;

        float relativeTerCoordX = terrainData.heightmapResolution * relativeHitTerX;
        float relativeTerCoordZ = terrainData.heightmapResolution * relativeHitTerZ;

        int hitPointTerX = Mathf.FloorToInt(relativeTerCoordX);
        int hitPointTerZ = Mathf.FloorToInt(relativeTerCoordZ);

        // Yes, Z as X, X as Y
        return new Vector2(hitPointTerZ, hitPointTerX);
    }

    public static Texture2D generateTexture2D(Color[,] colorMap){
        Texture2D texture = new Texture2D(colorMap.GetLength(0), colorMap.GetLength(1));

        for(int x=0; x<colorMap.GetLength(0); x++) for(int y=0; y<colorMap.GetLength(1); y++){
            texture.SetPixel(x, y, colorMap[x, y]);
        }
        texture.Apply();

        return texture;
    }
}
