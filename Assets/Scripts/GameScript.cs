using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
  
    public GameWorld gameWorld;


    // Use this for initialization
    void Start () {
        gameWorld.setMap(new Map());//for now map contains hard coded info. future will be spat out of a map generator
        gameWorld.buildWorld();
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
