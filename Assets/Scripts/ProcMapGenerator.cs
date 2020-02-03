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
        right,
        none
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
        Random.seed = seed;

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
            if (!positionsInBoundsOfSpaces(position))
                return;

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
                if (mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType!=Floor.stairOpening)
                    mapSpaces[(int)position.x, (int)position.y, (int)position.z].floorType = Floor.dirt;    
            }
            position += move;
        }

        if(Random.value*5==1)
            createRoom(position, Floor.dirt);
        else
            createRoom(position, Floor.swamp);

    }

    //checking if a vector is within bounds of the mapSpaces array
    public bool positionsInBoundsOfSpaces(Vector3 position) {
        int x = (int)position.x;
        int y = (int)position.y;
        int z = (int)position.z;

        if (x < 0 || x >= mapSpaces.GetLength(0))
            return false;
        if (y < 0 || y >= mapSpaces.GetLength(1))
            return false;
        if (z < 0 || z >= mapSpaces.GetLength(2))
            return false;

        return true;
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

    public void createRoom(Vector3 position, Floor floorType)
    {
        int width = (int)(Mathf.Abs(Random.value-Random.value) * distanceScale/2);
        int height = (int)(Mathf.Abs(Random.value - Random.value) * distanceScale/2);

        //Debug.Log("Width: " + width + " Height: " + height);

        int zStart = (int)position.z - height / 2;
        int xStart = (int)position.x - width / 2;

        Rect area = new Rect(xStart, zStart, position.x + width - xStart, position.z + height / 2-zStart);

        for (int z=zStart; z<position.z+height/2; z++)
        {          
            for (int x = xStart; x < position.x + width / 2; x++)
            {
                Direction cageDir = positionOnEdge(new Vector3(x, position.y, z), area);
                spawnCageChance(new Vector3(x, (int)position.y, z), cageDir);

                //Debug.Log(x+" "+position.y+" "+z);
                if (z >= 0 && z < mapSpaces.GetLength(2) && (int)position.y >= 0 && (int)position.y < mapSpaces.GetLength(1) && x >= 0 && x < mapSpaces.GetLength(0))
                {
                    //Debug.Log("Adding room tile");
                    //map[(int)position.y,y,x]=1;
                    mapSpaces[x, (int)position.y, z].floorType = floorType;
                }
            }
        }
    }

    //checks if a vector is on the edge of a rectangular area
    public Direction positionOnEdge(Vector3 position, Rect area) {
        int x = (int)position.x;
        int y = (int)position.y;
        int z = (int)position.z;

        if (x == area.xMin)
            return Direction.left;
        if (z == area.yMin)
            return Direction.up;
        if (x == area.xMax)
            return Direction.right;
        if (z == area.yMax)
            return Direction.down;

        return Direction.none;
    }

    public void spawnCageChance(Vector3 position, Direction dir) {
        float closetSpawnChance = 0.1f;
        if (Random.value < closetSpawnChance)
        {
            int x = (int)position.x;
            int y = (int)position.y;
            int z = (int)position.z;

            if (!positionsInBoundsOfSpaces(position))
                return;
            

            switch (dir)
            {
                case Direction.down:
                    mapSpaces[x, (int)position.y, z].frontWall = Wall.barredWall;
                    mapSpaces[x, (int)position.y, z-1].floorType = Floor.dirt;
                    break;
                case Direction.up:
                    mapSpaces[x, (int)position.y, z].backWall = Wall.barredWall;
                    mapSpaces[x, (int)position.y, z + 1].floorType = Floor.dirt;
                    break;
                case Direction.left:
                    mapSpaces[x, (int)position.y, z].leftWall = Wall.barredWall;
                    mapSpaces[x-1, (int)position.y, z].floorType = Floor.dirt;
                    break;
                case Direction.right:
                    mapSpaces[x, (int)position.y, z].rightWall = Wall.barredWall;
                    mapSpaces[x + 1, (int)position.y, z].floorType = Floor.dirt;
                    break;

            }
            
        }
    }
    

    public void surroundFloorsWithWalls() {

        float paintingChance = 0.1f;

        for (int y = 0; y < mapSpaces.GetLength(1); y++)
        {
            for (int z = 0; z < mapSpaces.GetLength(2); z++)
            {
                for (int x = 0; x < mapSpaces.GetLength(0); x++)
                {
                    if (mapSpaces[x, y, z].floorType != Floor.empty)
                    {
                        if (!positionsInBoundsOfSpaces(new Vector3(x+1, y, z+1))||
                            !positionsInBoundsOfSpaces(new Vector3(x-1, y, z-1)))
                            continue;

                        if (mapSpaces[x - 1, y, z].floorType == Floor.empty&&mapSpaces[x - 1, y, z].leftWall==Wall.empty)
                        {
                            mapSpaces[x, y, z].leftWall = Wall.stoneBlock;
                            
                        }

                        if (mapSpaces[x + 1, y, z].floorType == Floor.empty && mapSpaces[x + 1, y, z].leftWall == Wall.empty)
                        {
                            mapSpaces[x, y, z].rightWall = Wall.stoneBlock;
                        }

                        if (mapSpaces[x, y, z - 1].floorType == Floor.empty && mapSpaces[x, y, z-1].leftWall == Wall.empty)
                        {
                            mapSpaces[x, y, z].backWall = Wall.stoneBlock;
                        }

                        if (mapSpaces[x, y, z + 1].floorType == Floor.empty && mapSpaces[x, y, z+1].leftWall == Wall.empty)
                        {
                            mapSpaces[x, y, z].frontWall = Wall.stoneBlock;
                        }
                                             

                    }
                }
            }
        }

        //create ceilings
        for (int y = mapSpaces.GetLength(1)-1; y >=0 ; y--)
        {
            for (int z = 0; z < mapSpaces.GetLength(2); z++)
            {
                for (int x = 0; x < mapSpaces.GetLength(0); x++)
                {
                    if (y + 1 < mapSpaces.GetLength(1) &&
                            (mapSpaces[x, y, z].floorType == Floor.dirt|| mapSpaces[x, y, z].floorType == Floor.stairOpening|| mapSpaces[x, y, z].floorType ==Floor.swamp) &&
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
