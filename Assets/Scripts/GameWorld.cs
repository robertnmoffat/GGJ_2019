using UnityEngine;
using System.Collections;
using static Map;

public class GameWorld : MonoBehaviour
{
    public enum WallOrientation { front, back, left, right };

    public GameObject goblinPrefab;

    public GameObject stoneWall;
    public GameObject stoneDoor;
    public GameObject barredWall;

    public GameObject paintingObj;
    public GameObject paintingObj2;

    public GameObject goblinObj;

    public GameObject dirtFloor;
    public GameObject swampFloor;
    public GameObject stairs;
    public Map map;

    public GameObject dungeonEntityPrefab;
    public PlayerScript player;

    // Use this for initialization
    void Start()
    {
        //map.writeMapToFile();
        ///string mapString = map.convertMapToString();
        //map.createMapFromString(mapString);
        
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

    //build all the game objects from new mapSpaces array
    public void buildWorldFromMapSpaces() {
        for (int y = 0; y < map.getYLength(); y++)
        {
            for (int z = 0; z < map.getZLength(); z++)
            {
                for (int x = 0; x < map.getXLength(); x++)
                {
                    Interactible curInteractible = map.GetInteractible(new Vector3(x, y, z));
                    if (curInteractible != null && curInteractible.gameObjectScript == null)
                    {
                        instantiateInteractible(curInteractible, new Vector3(x,y,z));
                    }

                    MapSpace space = map.getMapSpace(new Vector3(x, y, z));
                    
                    instantiateFloor(space.floorType, new Vector3(x, y, z));
                    instantiateWall(space.leftWall, new Vector3(x, y, z), WallOrientation.left);
                    instantiateWall(space.rightWall, new Vector3(x, y, z), WallOrientation.right);
                    instantiateWall(space.frontWall, new Vector3(x, y, z), WallOrientation.front);
                    instantiateWall(space.backWall, new Vector3(x, y, z), WallOrientation.back);

                }
            }
        }
    }
        
    public Interactible instantiateInteractible(Interactible interactible, Vector3 position) {
        int x = (int)position.x;
        int y = (int)position.y;
        int z = (int)position.z;

        if (interactible is Item)
        {
            //Interactible is of the item class
            Item curItem = (Item)interactible;
            GameObject itemObject = Instantiate(dungeonEntityPrefab, new Vector3(x, y + 0.5f, (z) - 1), Quaternion.Euler(0, 0, 0));
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

    public void instantiateWall(Wall wallType, Vector3 position, WallOrientation wallOrientation) {
        if (wallType == Wall.empty)
            return;

        int x = (int)position.x;
        int y = (int)position.y;
        int z = (int)position.z;

        float pictureSpawnChance = 0.01f;

        GameObject wallObject = getWallObjectByType(wallType);
        GameObject wall;

        GameObject curPainting;
        if ((int)(Random.value * 2) == 1)
            curPainting = paintingObj;
        else
            curPainting = paintingObj2;


        if (wallObject != null)
        {
            switch (wallOrientation) {
                case WallOrientation.front:
                    wall = Instantiate(wallObject, new Vector3(x, y + 0.5f, z + 0.5f), Quaternion.Euler(0, 0, 0));
                    if(Random.value<pictureSpawnChance)
                    Instantiate(curPainting, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                    break;
                case WallOrientation.back:
                    wall = Instantiate(wallObject, new Vector3(x, y + 0.5f, z - 0.5f), Quaternion.Euler(0, 0, 0));
                    if (Random.value < pictureSpawnChance)
                        Instantiate(curPainting, new Vector3(x, y, z), Quaternion.Euler(0, 180, 0));
                    break;
                case WallOrientation.left:
                    wall = Instantiate(wallObject, new Vector3(x - 0.5f, y + 0.5f, z), Quaternion.Euler(0, 90, 0));
                    if (Random.value < pictureSpawnChance)
                        Instantiate(curPainting, new Vector3(x, y, z), Quaternion.Euler(0, -90, 0));
                    break;
                case WallOrientation.right:
                    wall = Instantiate(wallObject, new Vector3(x + 0.5f, y + 0.5f, z), Quaternion.Euler(0, 90, 0));
                    if (Random.value < pictureSpawnChance)
                        Instantiate(curPainting, new Vector3(x, y, z), Quaternion.Euler(0, 90, 0));
                    break;
                default:
                    wall = Instantiate(wallObject, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                    break;
            }
            

            StoneDoor sd = wall.GetComponent<StoneDoor>();
            if (sd != null) {
                sd.setPlayer(player);
            }
        }
    }

    public void instantiateFloor(Floor floorType, Vector3 position) {
        if (floorType == Floor.empty)
            return;

        int x = (int)position.x;
        int y = (int)position.y;
        int z = (int)position.z;

        float goblinChance = 0.01f;

        if(Random.value<goblinChance)
            Instantiate(goblinObj, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));

        GameObject floorObject = getFloorObjectByType(floorType);
        GameObject floor;//if we want to utilize the floors later
        if (floorObject != null)
        {
            if(floorType==Floor.stairsEast)
                floor = (GameObject)Instantiate(floorObject, new Vector3(x, y, z), Quaternion.Euler(0,90, 0));
            else if(floorType==Floor.stairsNorth)
                floor = (GameObject)Instantiate(floorObject, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            else if(floorType==Floor.stairsSouth)
                floor = (GameObject)Instantiate(floorObject, new Vector3(x, y, z), Quaternion.Euler(0, 180, 0));
            else if(floorType==Floor.stairsWest)
                floor = (GameObject)Instantiate(floorObject, new Vector3(x, y, z), Quaternion.Euler(0, 270, 0));
            else
            floor = (GameObject)Instantiate(floorObject, new Vector3(x, y, z), Quaternion.Euler(0, 180, 0));
        }
    }

    //returns object matching enum of floor map integer
    public GameObject getFloorObjectByType(Floor floorType) {
        switch (floorType) {
            case Map.Floor.empty:
                return null;
            case Map.Floor.dirt:
                return dirtFloor;
            case Map.Floor.swamp:
                return swampFloor;
            case Map.Floor.stairsEast:
                return stairs;
            case Map.Floor.stairsNorth:
                return stairs;
            case Map.Floor.stairsSouth:
                return stairs;
            case Map.Floor.stairsWest:
                return stairs;
        }

        return null;
    }

    //returns object matching enum of wall map integer
    public GameObject getWallObjectByType(Wall wallType) {
        switch (wallType) {
            case Map.Wall.stoneBlock:
                return stoneWall;
            case Map.Wall.stoneDoor:
                return stoneDoor;
            case Map.Wall.barredWall:
                return barredWall;
        }
        return null;
    }

    public GameObject instantiateFloor(Map.Floor floorType, Vector3 position, Quaternion rotation)
    {
        GameObject tileObject;

        switch (floorType)
        {
            case Map.Floor.dirt:
                tileObject = dirtFloor;
                break;
            case Map.Floor.swamp:
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

    public Map getMap() {
        return map;
    }
}
