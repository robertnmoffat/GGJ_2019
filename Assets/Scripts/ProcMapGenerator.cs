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
    Vector3 startPos;

    int distanceScale = 25;
    float stairsChance = 0.01f;

    public ProcMapGenerator(int seed, int y, int z, int x) {
        this.seed = seed;
        Random.InitState(seed);

        //map = new int[y, z, x];
        mapSpaces = new MapSpace[x,y,z];

        startPos = new Vector3(x / 2, 0, z / 2);
        position = startPos;
        //map[(int)position.y, (int)position.z, (int)position.x] = 1;
        mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType=Floor.dirt;

        int pathCount = 30;

        for(int i=0; i<pathCount; i++)
        {
            drawPath();
        }

        surroundFloorsWithWalls();
    }

    public void drawPath()
    {
        Direction dir = (Direction)((int)(Random.value * 4));
        //Debug.Log("Dir: " + dir);
        int distance = (int)(Random.value * distanceScale);
        Vector3 move = getMovementVector(dir);

        for (int i = 0; i < distance; i++)
        {
            //map[(int)position.y, (int)position.z, (int)position.x] = 1;
            if (Random.value < stairsChance)
            {
                switch (dir)
                {
                    case Direction.up:
                        mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType = Floor.stairsNorth;
                        break;
                    case Direction.down:
                        mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType = Floor.stairsSouth;
                        break;
                    case Direction.left:
                        mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType = Floor.stairsWest;
                        break;
                    case Direction.right:
                        mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType = Floor.stairsEast;
                        break;
                }                
                mapSpaces[(int)position.x, (int)position.y+1, (int)position.z].floorType = Floor.stairOpening;
                mapSpaces[(int)(position.x-move.x), (int)position.y + 1, (int)(position.z-move.z)].floorType = Floor.stairOpening;
                position.y += 1;
            }
            else
            {
                mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType = Floor.dirt;
            }
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
        int width = (int)(Mathf.Abs(Random.value-Random.value) * distanceScale/2);
        int height = (int)(Mathf.Abs(Random.value - Random.value) * distanceScale/2);

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

    public void surroundFloorsWithWalls() {

        for (int y = 0; y < mapSpaces.GetLength(1); y++)
        {
            for (int z = 0; z < mapSpaces.GetLength(2); z++)
            {
                for (int x = 0; x < mapSpaces.GetLength(0); x++)
                {
                    if (mapSpaces[x, y, z].floorType != Floor.empty)
                    {                        
                        if (mapSpaces[x - 1, y, z].floorType == Floor.empty)
                            mapSpaces[x, y, z].leftWall = Wall.stoneBlock;

                        if (mapSpaces[x + 1, y, z].floorType == Floor.empty)
                            mapSpaces[x, y, z].rightWall = Wall.stoneBlock;

                        if (mapSpaces[x, y, z-1].floorType == Floor.empty)
                            mapSpaces[x, y, z].backWall = Wall.stoneBlock;

                        if (mapSpaces[x, y, z+1].floorType == Floor.empty)
                            mapSpaces[x, y, z].frontWall = Wall.stoneBlock;
                                             

                    }
                }
            }
        }

        for (int y = mapSpaces.GetLength(1)-1; y >=0 ; y--)
        {
            for (int z = 0; z < mapSpaces.GetLength(2); z++)
            {
                for (int x = 0; x < mapSpaces.GetLength(0); x++)
                {
                    if (y + 1 < mapSpaces.GetLength(1) &&
                            (mapSpaces[x, y, z].floorType == Floor.dirt|| mapSpaces[x, y, z].floorType == Floor.stairOpening) &&
                            mapSpaces[x, y + 1, z].floorType == Floor.empty)
                        mapSpaces[x, y + 1, z].floorType = Floor.dirt;
                }
            }
        }
    }

   

    public MapSpace[,,] getMapSpaces()
    {
        //return map;
        return mapSpaces;
    }

    public Vector3 getStartPos() {
        return startPos;
    }
}
