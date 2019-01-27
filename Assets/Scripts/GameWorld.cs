using UnityEngine;
using System.Collections;

public class GameWorld : MonoBehaviour
{
    public enum WallOrientation { front, side };

    public GameObject stoneWall;
    public GameObject stoneDoor;
    public GameObject barredWall;

    public GameObject dirtFloor;
    public GameObject swampFloor;
    public GameObject stairs;
    public Map map;

    public GameObject dungeonEntityPrefab;

    // Use this for initialization
    void Start()
    {
        map.writeMapToFile();
        string mapString = map.convertMapToString();
        map.createMapFromString(mapString);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Set current game map
    public void setMap(Map map)
    {
        this.map = map;
    }

    //build all the game objects from map of level
    public void buildWorld()
    {
        int currentTileNumber = 0;

        for (int y = 0; y < map.getYLength(); y++)
        {
            for (int z = 0; z < map.getZLength(); z++)
            {
                for (int x = 0; x < map.getXLength(); x++)
                {
                    Interactible curInteractible;
                    if ((curInteractible = map.GetInteractible(y, z / 2, x / 2)) != null&&curInteractible.gameObjectScript==null)
                    {
                        instantiateInteractible(curInteractible, y, z, x);
                    }

                    //Instantiate(dirtFloor, new Vector3(x, 0, y), transform.rotation);
                    if ((currentTileNumber = map.getTileType(y, z, x)) > 0)
                    {
                        if (z % 2 == 0)
                        {
                            //only walls

                            instantiateWall(currentTileNumber, y,z,x, WallOrientation.front);
                            //Instantiate(stoneWall, new Vector3(x / 2, y + 0.5f, (-z / 2) + 0.5f), Quaternion.Euler(0, 0, 0));
                            //Instantiate(stoneWall, new Vector3(x / 2, y + 0.5f, (-z / 2) + 0.5f), Quaternion.Euler(0, 180, 0));
                        }
                        else
                        {

                            if (x % 2 == 0)
                            {
                                //walls

                                instantiateWall(currentTileNumber, y, z, x, WallOrientation.side);
                                //Instantiate(stoneWall, new Vector3((x / 2) - 0.5f, y + 0.5f, -z / 2), Quaternion.Euler(0, 90, 0));
                                //Instantiate(stoneWall, new Vector3((x / 2) - 0.5f, y + 0.5f, -z / 2), Quaternion.Euler(0, -90, 0));
                            }
                            else
                            {
                                //floor                              
                                //GameObject floor = (GameObject)Instantiate(swampFloor, new Vector3(x / 2, y, -z / 2), Quaternion.Euler(90, 0, 0));
                                //Instantiate(dirtFloor, new Vector3(x / 2, y, -z / 2), Quaternion.Euler(-90, 0, 0));
                                instantiateFloor(currentTileNumber, y, z, x);

                                
                            }
                        }
                    }
                }
            }
        }
    }

    public Interactible instantiateInteractible(Interactible interactible, int y, int z, int x) {
        if (interactible is Item)
        {
            //Interactible is of the item class
            Item curItem = (Item)interactible;
            GameObject itemObject = Instantiate(dungeonEntityPrefab, new Vector3(x / 2, y + 0.5f, (-z / 2) - 1), Quaternion.Euler(0, 0, 0));
            DungeonEntity newDungeonEntity = itemObject.GetComponent<DungeonEntity>();
            curItem.gameObjectScript = newDungeonEntity;
            curItem.initGameObject();
                   
            
            return curItem;
        }
        else {
            //Interactible is just an interactible (lever or door, etc)

            return null;
        }
    }

    public void instantiateWall(int tileNumber, int y, int z, int x, WallOrientation wallOrientation) {
        GameObject wallObject = getWallTypeByInt(tileNumber);
        GameObject wall;
        if (wallObject != null)
        {
            if(wallOrientation==WallOrientation.front)
                wall = Instantiate(wallObject, new Vector3(x / 2, y + 0.5f, (-z / 2) + 0.5f), Quaternion.Euler(0, 0, 0));
            else
                wall = Instantiate(wallObject, new Vector3((x / 2) - 0.5f, y + 0.5f, -z / 2), Quaternion.Euler(0, 90, 0));
        }
    }

    public void instantiateFloor(int tileNumber, int y, int z, int x) {
        GameObject floorObject = getFloorTypeByInt(tileNumber);
        GameObject floor;//if we want to utilize the floors later
        if (floorObject!=null)
            floor = (GameObject)Instantiate(floorObject, new Vector3(x / 2, y, -z / 2), Quaternion.Euler(0, 180, 0));
    }

    //returns object matching enum of floor map integer
    public GameObject getFloorTypeByInt(int tileInt) {
        Map.Floors floorType = (Map.Floors)tileInt;//casting to Floor enum
        switch (floorType) {
            case Map.Floors.empty:
                return null;
            case Map.Floors.dirt:
                return dirtFloor;
            case Map.Floors.swamp:
                return swampFloor;
            case Map.Floors.stairs:
                return stairs;
        }

        return null;
    }

    //returns object matching enum of wall map integer
    public GameObject getWallTypeByInt(int tileInt) {
        Map.Walls wallType = (Map.Walls)tileInt;
        switch (wallType) {
            case Map.Walls.stoneBlock:
                return stoneWall;
            case Map.Walls.stoneDoor:
                return stoneDoor;
            case Map.Walls.barredWall:
                return barredWall;
        }
        return null;
    }

    public GameObject instantiateFloor(Map.Floors floorType, Vector3 position, Quaternion rotation)
    {
        GameObject tileObject;

        switch (floorType)
        {
            case Map.Floors.dirt:
                tileObject = dirtFloor;
                break;
            case Map.Floors.swamp:
                tileObject = swampFloor;
                break;
        }

        return (GameObject)Instantiate(swampFloor, position, rotation);
        //return Instantiate(dirtFloor, new Vector3(x / 2, y, -z / 2), Quaternion.Euler(-90, 0, 0));
    }

    public GameObject getStoneWall()
    {
        return stoneWall;
    }

    public GameObject getDirtFloor()
    {
        return dirtFloor;
    }
}
