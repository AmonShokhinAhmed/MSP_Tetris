using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    private GameManager gameManager;
    private Grid grid;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        grid = GetComponent<Grid>();
        //TODO: setup grid
    }

    private void Update()
    {
        render();
    }

    private void render()
    {
        //TODO: update score
        //TODO: update piece sprites based on grid
        //TODO: update next tetris tile
    }
}
