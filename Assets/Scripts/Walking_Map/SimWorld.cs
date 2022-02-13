using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimWorld : MonoBehaviour
{
    const int MapSize = 20;
    public static MapChunk[,] map = new MapChunk[MapSize, MapSize];

    public class MapChunk{
        public Terrain terrain;
        public Settlment settlment;
        public List<Group> groups;
    }
    public enum Terrain{
        water,
        plain,
        forest,
        mountain
    }
    public class Settlment{
        int faction;
        List<Building> buildings;
        List<Pop> pops;
        List<Item> items;
        public Settlment(int Faction){
            faction = Faction;
        }
    }
    public class Group{
        string faction;
        List<Pop> pops;
        List<Item> items;
        Vector2Int home;
        string target;
    }
    public class Building{
        string name;
        Vector3 position;
        Quaternion quaternion;
    }
    public class Pop{
        string name;
        Vector3 position;
        Quaternion quaternion;
    }
    public class Item{
        string name;
        int count;
    }

    void Start()
    {
        for(int x=0; x<MapSize; x++) for(int y=0; y<MapSize; y++){
            if(x==0 || x==MapSize-1 || y==0 || y==MapSize-1) map[x,y].terrain = Terrain.water;
            else map[x,y].terrain = Terrain.plain;
            if(x==MapSize/2 && y==MapSize/2) map[x,y].settlment = new Settlment(1);
        }
    }
}
