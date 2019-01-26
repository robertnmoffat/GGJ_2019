using UnityEngine;
using System.Collections;

public class GameWorld : MonoBehaviour
{
    public GameObject stoneWall;
    public GameObject dirtFloor;
    public GameObject swampFloor;
    public Map map;

    // Use this for initialization
    void Start()
    {
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
        for (int y = 0; y < 2; y++)
        {
            for (int z = 0; z < 9; z++)
            {
                for (int x = 0; x < 9; x++)
                {
                    //Instantiate(dirtFloor, new Vector3(x, 0, y), transform.rotation);
                    if (map.getTileType(y, z, x) == 1)
                    {
                        if (z % 2 == 0)
                        {
                            //only walls
                            Instantiate(stoneWall, new Vector3(x / 2, y + 0.5f, (-z / 2) + 0.5f), Quaternion.Euler(0, 0, 0));
                            Instantiate(stoneWall, new Vector3(x / 2, y + 0.5f, (-z / 2) + 0.5f), Quaternion.Euler(0, 180, 0));
                        }
                        else
                        {

                            if (x % 2 == 0)
                            {
                                //walls
                                Instantiate(stoneWall, new Vector3((x / 2) - 0.5f, y + 0.5f, -z / 2), Quaternion.Euler(0, 90, 0));
                                Instantiate(stoneWall, new Vector3((x / 2) - 0.5f, y + 0.5f, -z / 2), Quaternion.Euler(0, -90, 0));
                            }
                            else
                            {
                                //floor                              
                                GameObject floor = (GameObject)Instantiate(swampFloor, new Vector3(x / 2, y, -z / 2), Quaternion.Euler(90, 0, 0));
                                Instantiate(dirtFloor, new Vector3(x / 2, y, -z / 2), Quaternion.Euler(-90, 0, 0));
                            }
                        }

                    }
                }

            }
        }
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
