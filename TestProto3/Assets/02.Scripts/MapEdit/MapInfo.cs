using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MAPSTYLE
{
    TILE = 0,
    WALL,
    LIGHT,
    CONTROLLER,
    HOLE,
    AGRO
}

public class MapInfo : MonoBehaviour {

    public Transform[] MapStyles;
}
