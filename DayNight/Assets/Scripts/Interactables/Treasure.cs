using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable {



    public Sprite openedSprite;
    public AudioSource openSound;
    private bool isClosed = true;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        openSound = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
    }

    public override void Interact()
    {
        if (isClosed == true){
            openSound.Play();
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }

        isClosed = false;
    }

    public bool IsClosed()
    {
        return isClosed;
    }

}
