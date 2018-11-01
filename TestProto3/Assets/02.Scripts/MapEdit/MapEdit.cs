using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdit : MonoBehaviour{

    public MAPSTYLE curMapStyle;
    public MapInfo m_cMapInfo;

    public void GenerateMap()
    {
        Instantiate(m_cMapInfo.MapStyles[(int)curMapStyle], Vector3.zero, Quaternion.identity);
    }
}
