using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using static Item;

public class Map : MonoBehaviour {
    public struct MapSpace
    {
        public Floor floorType;
        public Wall frontWall;
        public Wall backWall;
        public Wall leftWall;
        public Wall rightWall;
    }

    public enum Floor { empty = 0, dirt = 1, swamp = 2, stairsNorth = 3,stairsSouth, stairsWest, stairsEast, stairOpening};
    public enum Wall { empty = 0, stoneBlock = 1 , stoneDoor = 2, barredWall = 3};

    public Interactible[,,] interactibleMap;

    public Sprite swordGraphic;

    public GameObject[,,] levelGameObjects;

    MapSpace[,,] mapSpaces;

    //super unintuitive level layout
    int[,,] levelTiles = {
                        {   {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0 },
                            {1, 1, 3, 1, 3, 1, 1, 0, 0, 0, 1, 2, 0, 2, 0, 2, 1, 0 },
                            {1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 2, 0, 2, 0, 2, 1, 0 },
                            {1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 2, 1, 0, 1, 0, 2, 0, 2, 0, 2, 1, 0 },
                            {1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
                            {1, 1, 1, 3, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }},

                        {   {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0 },
                            {1, 0, 0, 1, 2, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0 },
                            {1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0 },
                            {1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0 },
                            {0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
                            {1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }},

                        {   {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0 },
                            {1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
                            {1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            {0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }}};



    // Use this for initialization
    void Start () {
        // Debug.Log(levelTiles.GetLength(0));//2 y
        // Debug.Log(levelTiles.GetLength(1));//10 z
        // Debug.Log(levelTiles.GetLength(2));//9 x
        //Debug.Log(convertMapToString());

        
    }

    public void setMap(int[,,] map)
    {
        levelTiles = map;
    }

    public void setMap(MapSpace[,,] map)
    {
        mapSpaces = map;
    }


    public void setMapSpaces(MapSpace[,,] map)
    {
        mapSpaces = map;
    }

    public MapSpace getMapSpace(Vector3 position) {
        int x = (int)position.x;
        int y = (int)position.y;
        int z = (int)position.z;
        return mapSpaces[x,y,z];
    }

    public void initializeInteractibleMap() {
        interactibleMap = new Interactible[levelTiles.GetLength(0), levelTiles.GetLength(1), levelTiles.GetLength(2)];
        Item swordItem = new Item(levelTiles.GetLength(0)/2, 0, levelTiles.GetLength(2)/2, swordGraphic, ItemType.sword);
        //interactibleMap[0, 4, 1] = swordItem;//yzx
        interactibleMap[levelTiles.GetLength(0)/2, 0, levelTiles.GetLength(2)/2] = swordItem;
    }

    public Interactible GetInteractible(Vector3 position) {
        if (!WithinBoundsOfInteractableMap(position))
            return null;

        int y = (int)position.y;
        int z = (int)position.z;
        int x = (int)position.x;

        if (y < interactibleMap.GetLength(0) &&
            z < interactibleMap.GetLength(1) &&
            x < interactibleMap.GetLength(2)) {
            return interactibleMap[y, z, x];
        }
        return null;
    }

    public void setInteractible(Vector3 position, Interactible interactible)
    {
        if (!WithinBoundsOfInteractableMap(position))
            return;

        int y = (int)position.y;
        int z = (int)position.z;
        int x = (int)position.x;

        if (y < interactibleMap.GetLength(0) &&
            z < interactibleMap.GetLength(1) &&
            x < interactibleMap.GetLength(2))
        {
            interactibleMap[y, z, x] = interactible;
        }
    }

    public bool WithinBoundsOfInteractableMap(Vector3 position) {
        int x = (int)position.x;
        int y = (int)position.y;
        int z = (int)position.z;

        return (y < interactibleMap.GetLength(0) &&
            z < interactibleMap.GetLength(1) &&
            x < interactibleMap.GetLength(2));
        }

    // Update is called once per frame
    void Update () {
	
	}

    public int getTileType(int y, int z, int x) {
        return levelTiles[y,z,x];
    }
    

    public int getXLength() {
        return mapSpaces.GetLength(0);
    }

    public int getYLength() {
        return mapSpaces.GetLength(1);
    }

    public int getZLength() {
        return mapSpaces.GetLength(2);
    }
}
