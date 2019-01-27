using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntity : MonoBehaviour {
    public enum InteractiveType { item, interactible };

    public InteractiveType interactiveType;
    public Item itemScript;
    public Interactible interactibleScript;
    public Sprite sprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void removeSelf() {
        Destroy(this.gameObject);
    }
}
