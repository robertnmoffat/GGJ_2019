using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactible {

        public enum ItemType {
        none,
        sword
        };

    public Sprite itemGraphic;
    public SpriteRenderer spriteRenderer;
    public ItemType itemType;

    public Item(int x, int y, int z, Sprite itemGraphic, ItemType itemType)
        : base(x, y, z)
    {
        this.itemGraphic = itemGraphic;
        this.itemType = itemType;
    }

    public Sprite getItemGraphic() {
        return itemGraphic;
    }

    public void setGraphic(Sprite graphic) {
        itemGraphic = graphic;
        spriteRenderer.sprite = itemGraphic;
    }

    public void copyItem(Item toCopy) {
        setGraphic(toCopy.getItemGraphic());
    }

    public void initGameObject() {
        gameObjectScript.GetComponent<SpriteRenderer>().sprite = itemGraphic;
    }

    public void removeGameObject() {
        if (gameObjectScript != null) {
            gameObjectScript.removeSelf();
            gameObjectScript = null;
        }
    }

    public ItemType getItemType() {
        return itemType;
    }
}
