using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {
  
    public GameWorld gameWorld;

    public Image itemHeldImage;
    public Image wieldedHeldImage;
    public Sprite emptyItemSprite;

    public PlayerScript player;

    public GameObject treasure;

    // Use this for initialization
    void Start () {
        itemHeldImage.sprite = emptyItemSprite;
        //gameWorld.setMap(new Map());//for now map contains hard coded info. future will be spat out of a map generator
        gameWorld.player = player;
        gameWorld.map.initializeInteractibleMap();
        gameWorld.buildWorld();
        Instantiate(treasure, new Vector3(0.69f, 1.6f, -4f), Quaternion.Euler(0, -44.66f, 0));
    }
	
	// Update is called once per frame
	void Update () {
        if (player.isItemHeldChanged()) {
            itemHeldImage.sprite = player.getHeldItem().getItemGraphic();
            wieldedHeldImage.sprite = player.getHeldItem().getItemGraphic();
            player.resetItemHeldChanged();
        }
        if (player.isInteractionPressed()) {
            //InteractionPressed
            player.resetInteractionPressed();
            Interactible curInteractible;
            Map map = gameWorld.map;
            int x = player.blockx;
            int z = player.blockz;
            int y = player.blocky;
            if ((curInteractible = map.GetInteractible(y, -1*z, x)) != null)
            {
                if (curInteractible is Item) {
                    Item curItem = (Item)curInteractible;
                    curItem.removeGameObject();
                    player.setHeldItem(curItem);
                    map.setInteractible(y, -1 * z, x, null);
                }
                Debug.Log("Item present");
            }
            else {
                Debug.Log("Empty");
            }
            
        }
	}
}
