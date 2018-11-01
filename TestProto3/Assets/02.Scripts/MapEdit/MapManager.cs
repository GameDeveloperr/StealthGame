//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(MapEdit))]
//public class MapManager : Editor {

//    public void OnSceneGUI()
//    {
//        MapEdit map = target as MapEdit;

//        Handles.BeginGUI();

//        if(GUI.Button(new Rect(10, 10, 100, 30), "TILE"))
//        {
//            map.curMapStyle = MAPSTYLE.TILE;
//            map.GenerateMap();
//        }
//        if (GUI.Button(new Rect(10, 50, 100, 30), "WALL"))
//        {
//            map.curMapStyle = MAPSTYLE.WALL;
//            map.GenerateMap();
//        }
//        if (GUI.Button(new Rect(10, 90, 100, 30), "LIGHT"))
//        {
//            map.curMapStyle = MAPSTYLE.LIGHT;
//            map.GenerateMap();
//        }
//        if (GUI.Button(new Rect(10, 130, 100, 30), "CONTROLLER"))
//        {
//            map.curMapStyle = MAPSTYLE.CONTROLLER;
//            map.GenerateMap();
//        }
//        if (GUI.Button(new Rect(10, 170, 100, 30), "HOLE"))
//        {
//            map.curMapStyle = MAPSTYLE.HOLE;
//            map.GenerateMap();
//        }
//    }
//}
