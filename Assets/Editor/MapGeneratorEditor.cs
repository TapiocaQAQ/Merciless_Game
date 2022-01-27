using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGenerator)),CanEditMultipleObjects]
public class MapGeneratorEditor : Editor {
    public override void OnInspectorGUI() {

        //base.OnInspectorGUI();
        MapGenerator mapGen = (MapGenerator)target;

        
        if(DrawDefaultInspector()){
            if(mapGen.autoUpdate){
                mapGen.DrawMapInEditor();
            }
        }

        if(GUILayout.Button("Generate")){
            mapGen.DrawMapInEditor();
        }
        
        
    }
}


