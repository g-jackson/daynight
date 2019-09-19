using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable {

    public Sprite openedSprite;
    private bool isClosed = true;

    public override void Interact()
    {
        GetComponent<SpriteRenderer>().sprite = openedSprite;
        isClosed = false;
    }

    public bool IsClosed()
    {
        return isClosed;
    }

}
