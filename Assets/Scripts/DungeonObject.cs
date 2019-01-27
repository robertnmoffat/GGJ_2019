using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonObject : MonoBehaviour {

    int xCoordinate = 0;
    int yCoordinate = 0;
    int zCoordinate = 0;

	// Use this for initialization
	void Start () {


        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public DungeonObject(int x, int y, int z)
    {
        xCoordinate = x;
        yCoordinate = y;
        zCoordinate = z;

    }
}
