using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    public float SideLength = 1.0f;
    public GameObject PiecePrefab;
    private GameManager gameManager;
    private Grid grid;
    private GameObject[,] pieces = new GameObject[GameManager.GRID_WIDTH, GameManager.GRID_HEIGHT];
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        grid = GetComponent<Grid>();
        grid.cellSize = new Vector3(SideLength, SideLength, SideLength);

        for (int x = 0; x < GameManager.GRID_WIDTH; x++)
        {
            GameObject col = new GameObject("Col " + x);
            col.transform.parent = transform;
            for (int y = 0; y < GameManager.GRID_HEIGHT; y++)
            {
                Vector3 position = grid.CellToLocal(new Vector3Int(x, y, 0)) + Vector3.left * SideLength * 0.5f + Vector3.down * SideLength * 0.5f;
                GameObject piece = Instantiate(PiecePrefab, position, Quaternion.identity, col.transform);
                piece.name = ("(" + x + "|" + y + ")");
                piece.transform.localScale = new Vector3(SideLength, SideLength, SideLength);
                pieces[x, y] = piece;
            }
        }
    }

    private void Update()
    {
        render();
    }

    private void render()
    {
        //TODO: update score
        Piece[,] gridInformation = gameManager.Grid;
        for (int x = 0; x < GameManager.GRID_WIDTH; x++)
        {
            for (int y = 0; y < GameManager.GRID_HEIGHT; y++)
            {
                GameObject pieceVisual = pieces[x, y];
                Piece pieceInfo = gridInformation[x, y];
                if (pieceInfo.Active)
                {
                    pieceVisual.GetComponent<SpriteDebugVisualizer>().Color = pieceInfo.Color;
                }
                else
                {
                    pieceVisual.GetComponent<SpriteDebugVisualizer>().Color = Color.clear;
                }
            }
        }
        //TODO: update next tetris tile
    }
}
