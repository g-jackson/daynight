using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable {

    public Sprite openedSprite;
    private bool isClosed = true;

    public override void Interact()
    {
        if (isClosed == true){
            GetComponent<SpriteRenderer>().sprite = openedSprite;
            SoundManager.getInstance().Find("Creak").source.Play();
        }

        isClosed = false;
    }

    public bool IsClosed()
    {
        return isClosed;
    }

}
