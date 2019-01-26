using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Camera playerView;
    public PlayerInput playerInput;

    public Vector3 direction;

    // Use this for initialization
    void Start () {
        playerInput.playerTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        playerInput.handlePlayerMovement();
        direction = transform.eulerAngles;
    }

    
}
