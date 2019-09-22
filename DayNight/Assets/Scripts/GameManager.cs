using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public float turnDelay = 0.1f;                            //Delay between each Player turn.
    public int playerhealthPoints = 100;                      //Starting value for Player health points.
    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
    [HideInInspector] public bool playersTurn = true;         //Boolean to check if it's players turn, hidden in inspector but public.

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the level
        InitGame();
    }


    // Use this for initialization
    void Start ()
    {
    }

    //This is called each time a scene is loaded.
    void OnLevelWasLoaded(int index)
    {
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
    }


    //Update is called every frame.
    void Update()
    {
        playersTurn = true;
        if(playersTurn)
            return;
    }


    //GameOver is called when the player reaches 0 health points
    public void GameOver()
    {
        //Disable this GameManager.
        enabled = false;
    }
}
