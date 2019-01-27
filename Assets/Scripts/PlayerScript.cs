using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Camera playerView;
    public PlayerInput playerInput;

    public Vector3 direction;

    public const int INIT_MAX_HP = 100;
    public const int INIT_MAX_SANITY = 0;

    private int maxHealth;
    private int currentHealth;
    private int maxSanity;
    private int currentSanity;


    // Use this for initialization
    void Start () {
        playerInput.playerTransform = transform;
        GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        playerInput.handlePlayerMovement();
        direction = transform.eulerAngles;
    }


    public PlayerScript()
    {
        maxHealth = 10;


    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public int MaxSanity
    {
        get
        {
            return maxSanity;
        }

        set
        {
            maxSanity = value;
        }
    }

    public int CurrentSanity
    {
        get
        {
            return currentSanity;
        }

        set
        {
            currentSanity = value;
        }
    }


}
