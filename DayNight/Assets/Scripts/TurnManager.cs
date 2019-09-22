﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Text turnCountText;

    private int _turnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleTurn()
    {
        //Update turn count
        _turnCount++;
        turnCountText.text = _turnCount.ToString();
    }
}
