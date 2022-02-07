// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;

// [CustomEditor(typeof(MapGenerator_kai)),CanEditMultipleObjects]
// public class MapGenerator_kaiEditor : Editor {
//     public override void OnInspectorGUI() {

//         //base.OnInspectorGUI();
//         MapGenerator_kai mapGen = (MapGenerator_kai)target;
        
//         if(DrawDefaultInspector()){
//             if(mapGen.autoUpdate){
//                 mapGen.GenerateMap();
//             }
//         }

//         if(GUILayout.Button("Generate")){
//             mapGen.GenerateMap();
//         }
        
//     }
// }
