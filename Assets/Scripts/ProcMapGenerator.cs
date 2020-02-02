using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Map;

public class ProcMapGenerator
{
    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    int seed;
    //int[,,] map;
    MapSpace[,,] mapSpaces;

    Vector3 position;

    public ProcMapGenerator(int seed, int y, int z, int x) {
        this.seed = seed;
        Random.InitState(seed);

        //map = new int[y, z, x];
        mapSpaces = new MapSpace[x,y,z];

        position = new Vector3(x/2, 0, z/2);
        //map[(int)position.y, (int)position.z, (int)position.x] = 1;
        mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType=Floor.dirt;

        int pathCount = 20;

        for(int i=0; i<pathCount; i++)
        {
            drawPath();
        }

    }

    public void drawPath()
    {
        Direction dir = (Direction)((int)(Random.value * 4));
        //Debug.Log("Dir: " + dir);
        int distance = (int)(Random.value * 10);
        Vector3 move = getMovementVector(dir);

        for (int i = 0; i < distance; i++)
        {
            //map[(int)position.y, (int)position.z, (int)position.x] = 1;
            mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType = Floor.dirt;
            position += move;
        }

        createRoom(position);
    }

    public Vector3 getMovementVector(Direction dir)
    {
        switch (dir)
        {
            case Direction.down:
                return new Vector3(0,0,-1);
            case Direction.left:
                return new Vector3(-1, 0, 0);
            case Direction.right:
                return new Vector3(1, 0, 0);
            case Direction.up:
                return new Vector3(0, 0, 1);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    public void createRoom(Vector3 position)
    {
        int width = (int)(Random.value * 5);
        int height = (int)(Random.value * 5);

        //Debug.Log("Width: " + width + " Height: " + height);

        int zStart = (int)position.z - height / 2;
        int xStart = (int)position.x - width / 2;

        for (int z=zStart; z<position.z+height/2; z++)
        {
            for (int x = xStart; x < position.x + width / 2; x++)
            {
                //Debug.Log(x+" "+position.y+" "+z);
                if (z >= 0 && z < mapSpaces.GetLength(2) && (int)position.y >= 0 && (int)position.y < mapSpaces.GetLength(1) && x >= 0 && x < mapSpaces.GetLength(0))
                {
                    Debug.Log("Adding room tile");
                    //map[(int)position.y,y,x]=1;
                    mapSpaces[x, (int)position.y, z].floorType = Floor.dirt;
                }
            }
        }
    }

   

    public MapSpace[,,] getMapSpaces()
    {
        //return map;
        return mapSpaces;
    }
}
