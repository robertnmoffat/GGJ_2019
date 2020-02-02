using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using static Item;

public class Map : MonoBehaviour {
    public enum Floors { empty = 0, dirt = 1, swamp = 2, stairs = 3};
    public enum Walls { empty = 0, stoneBlock = 1 , stoneDoor = 2, barredWall = 3};

    public Interactible[,,] interactibleMap;

    public Sprite swordGraphic;

    public GameObject[,,] levelGameObjects;

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
        Debug.Log(convertMapToString());

        
    }

    public void initializeInteractibleMap() {
        interactibleMap = new Interactible[levelTiles.GetLength(0), levelTiles.GetLength(1) / 2, levelTiles.GetLength(2) / 2];
        Item swordItem = new Item(1, 0, 4, swordGraphic, ItemType.sword);
        interactibleMap[0, 4, 1] = swordItem;//yzx

    }

    public Interactible GetInteractible(int y, int z, int x) {
        if (y < interactibleMap.GetLength(0) &&
            z < interactibleMap.GetLength(1) &&
            x < interactibleMap.GetLength(2)) {
            return interactibleMap[y, z, x];
        }
        return null;
    }

    public void setInteractible(int y, int z, int x, Interactible interactible)
    {
        if (y < interactibleMap.GetLength(0) &&
            z < interactibleMap.GetLength(1) &&
            x < interactibleMap.GetLength(2))
        {
            interactibleMap[y, z, x] = interactible;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    public int getTileType(int y, int z, int x) {
        return levelTiles[y,z,x];
    }

    public string convertMapToString() {
        StringBuilder builder = new StringBuilder();

        builder.Append("y:\n");
        builder.Append(levelTiles.GetLength(0) + "\n");
        builder.Append("z:\n");
        builder.Append(levelTiles.GetLength(1) + "\n");
        builder.Append("x:\n");
        builder.Append(levelTiles.GetLength(2) + "\n");

        for (int y = 0; y < getYLength(); y++)
        {
            builder.Append("\n");

            for (int z = 0; z < getZLength(); z++)
            {
                builder.Append("\n");

                for (int x = 0; x < getXLength(); x++)
                {
                    int curTile = levelTiles[y, z, x];

                    builder.Append(curTile);
                    builder.Append(',');
                }

                //builder.Append("");
            }

            //builder.Append("");            
        }

        return builder.ToString();
    }

    public void writeMapToFile() {
        string mapString = convertMapToString();
        System.IO.File.WriteAllText(@"map.txt", mapString);

        //StreamWriter writer = new StreamWriter(@"Map.txt", true);
        //writer.Write(mapString);
    }

    public string readMapStringFromFile() {
        //TODO:
        return null;
    }

    public int[,,] createMapFromString(string mapString) {
        string[] stringLines = Regex.Split(mapString, @"\r?\n|\r");

        int pos = 1;
        int y, z, x;
        if (!int.TryParse(stringLines[pos], NumberStyles.Number, CultureInfo.InvariantCulture, out y))
            Debug.Log("READ ERROR LINE "+pos);
        pos += 2;//move to z
        if (!int.TryParse(stringLines[pos], NumberStyles.Number, CultureInfo.InvariantCulture, out z))
            Debug.Log("READ ERROR LINE " + pos);
        pos += 2;//move to x
        if (!int.TryParse(stringLines[pos], NumberStyles.Number, CultureInfo.InvariantCulture, out x))
            Debug.Log("READ ERROR LINE " + pos);
        pos += 3;//move to first data line

        int[,,] mapArray = new int[y,z,x];

        string[] curLineNums = stringLines[pos++].Split(',');


        //string curString = "2 jkl 4 jkl 3";
        //int num = int.Parse(curString, CultureInfo.InvariantCulture);

        Debug.Log("y=" + y);
        Debug.Log("z=" + z);
        Debug.Log("x=" + x);

        

        // Debug.Log("y="+mapString[2]);
        //Debug.Log("z=" + mapString[2]);
        //Debug.Log("x=" + mapString[2]);

        return null;
    }

    public int getYLength() {
        return levelTiles.GetLength(0);
    }

    public int getZLength() {
        return levelTiles.GetLength(1);
    }

    public int getXLength() {
        return levelTiles.GetLength(2);
    }
}
