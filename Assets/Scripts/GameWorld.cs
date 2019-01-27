using UnityEngine;
using System.Collections;

public class GameWorld : MonoBehaviour
{
    public GameObject stoneWall;
    public GameObject dirtFloor;
    public GameObject swampFloor;
    public GameObject stairs;
    public Map map;

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
                    //Instantiate(dirtFloor, new Vector3(x, 0, y), transform.rotation);
                    if ((currentTileNumber = map.getTileType(y, z, x)) > 0)
                    {
                        if (z % 2 == 0)
                        {
                            //only walls
                            Instantiate(stoneWall, new Vector3(x / 2, y + 0.5f, (-z / 2) + 0.5f), Quaternion.Euler(0, 0, 0));
                            //Instantiate(stoneWall, new Vector3(x / 2, y + 0.5f, (-z / 2) + 0.5f), Quaternion.Euler(0, 180, 0));
                        }
                        else
                        {

                            if (x % 2 == 0)
                            {
                                //walls
                                Instantiate(stoneWall, new Vector3((x / 2) - 0.5f, y + 0.5f, -z / 2), Quaternion.Euler(0, 90, 0));
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

    public void instantiateFloor(int tileNumber, int y, int z, int x) {
        GameObject floorObject = getFloorTypeByInt(tileNumber);
        GameObject floor;//if we want to utilize the floors later
        if (floorObject!=null)
            floor = (GameObject)Instantiate(floorObject, new Vector3(x / 2, y, -z / 2), Quaternion.Euler(0, 180, 0));
    }

    //returns object matching enum of map integer
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
