using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Terrain_generator)),CanEditMultipleObjects]
public class Terrain_generator_editor : Editor
{
    public override void OnInspectorGUI() {

        base.OnInspectorGUI();
        Terrain_generator terrainGen = (Terrain_generator)target;

        if(GUILayout.Button("Generate")){
            terrainGen.GenerateTerrain();
        }
        
    }
}
