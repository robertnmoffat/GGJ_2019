using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonObject {

    int xCoordinate = 0;
    int yCoordinate = 0;
    int zCoordinate = 0;
    public DungeonEntity gameObjectScript = null;

    public DungeonObject(int x, int y, int z)
    {
        xCoordinate = x;
        yCoordinate = y;
        zCoordinate = z;

    }
}
