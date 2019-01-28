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

    public Item heldItem=null;
    public bool itemHeldChanged = false;
    public bool interactionPressed = false;
    public bool mouseClicked = false;

    public int blockx, blockz, blocky;


    // Use this for initialization
    void Start () {
        playerInput.playerTransform = transform;
        GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        playerInput.handlePlayerMovement();
        direction = transform.eulerAngles;

        blockx = (int)(transform.position.x+0.5f);
        blockz = (int)(transform.position.z+0.5f);
        blocky = (int)transform.position.y;

        if (playerInput.isInteractionPressed()) {
            interactionPressed = true;
            playerInput.resetInteractionPressed();
        }
        if (playerInput.isMouseClicked()) {
            BoxCollider bc = GetComponent<BoxCollider>();
            

            mouseClicked = true;
            playerInput.resetMouseClicked();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        

        if (col.gameObject.name == "OrangeGolblin")
        {
            Debug.Log("COLLISION ENTERED");
            if (mouseClicked) {
                col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y+1, col.transform.position.z);
            }
            //Destroy(col.gameObject);
        }
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

    public void setHeldItem(Item newItem) {
        heldItem = newItem;
        itemHeldChanged = true;
    }

    public bool isItemHeldChanged() {
        return itemHeldChanged;
    }

    public void resetItemHeldChanged() {
        itemHeldChanged = false;
    }

    public Item getHeldItem() {
        return heldItem;
    }


    public bool isInteractionPressed()
    {
        return interactionPressed;
    }

    public void resetInteractionPressed()
    {
        interactionPressed = false;
    }

}
