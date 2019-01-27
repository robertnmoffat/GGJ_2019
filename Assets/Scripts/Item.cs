using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactible {
    public Sprite itemGraphic;
    public SpriteRenderer spriteRenderer;

    

    public Item(int x, int y, int z, Sprite itemGraphic)
        : base(x, y, z)
    {
        this.itemGraphic = itemGraphic;
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
}
