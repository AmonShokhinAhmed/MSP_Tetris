using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour
{
    public GameObject I_PreviewTetronimo;
    public GameObject O_PreviewTetronimo;
    public GameObject T_PreviewTetronimo;
    public GameObject S_PreviewTetronimo;
    public GameObject Z_PreviewTetronimo;
    public GameObject J_PreviewTetronimo;
    public GameObject L_PreviewTetronimo;
    public Text ScoreText; 
    public float SideLength = 1.0f;
    public GameObject PiecePrefab;
    private GameManager gameManager;
    private Grid grid;
    private PieceSpriteManager[,] pieces = new PieceSpriteManager[GameManager.GRID_WIDTH, GameManager.GRID_HEIGHT];
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
                PieceSpriteManager piece = Instantiate(PiecePrefab, position, Quaternion.identity, col.transform).GetComponent<PieceSpriteManager>();
                piece.gameObject.name = ("(" + x + "|" + y + ")");
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
        ScoreText.text = ""+gameManager.Score;
        Piece[,] gridInformation = gameManager.Grid;
        for (int x = 0; x < GameManager.GRID_WIDTH; x++)
        {
            for (int y = 0; y < GameManager.GRID_HEIGHT; y++)
            {
                PieceSpriteManager pieceVisual = pieces[x, y];
                Piece pieceInfo = gridInformation[x, y];
                if (pieceInfo.Active)
                {
                    pieceVisual.SetColor(pieceInfo.Color);
                    pieceVisual.GetComponent<SpriteDebugVisualizer>().Color = Color.clear;
                }
                else
                {
                    pieceVisual.SetColor(Color.clear);
                    pieceVisual.GetComponent<SpriteDebugVisualizer>().Color = new Color(0.4f,0.4f,0.4f,0.4f);
                }
            }
        }
        activatePreviewTetronimo(gameManager.NextTetronimoShape);
    }

    private void activatePreviewTetronimo(TetronimoShape tetronimo)
    {
        I_PreviewTetronimo.SetActive(false);
        O_PreviewTetronimo.SetActive(false);
        T_PreviewTetronimo.SetActive(false);
        S_PreviewTetronimo.SetActive(false);
        Z_PreviewTetronimo.SetActive(false);
        J_PreviewTetronimo.SetActive(false);
        L_PreviewTetronimo.SetActive(false);
        switch (tetronimo)
        {
            case TetronimoShape.I:
                I_PreviewTetronimo.SetActive(true);
                break;
            case TetronimoShape.O:
                O_PreviewTetronimo.SetActive(true);
                break;
            case TetronimoShape.T:
                T_PreviewTetronimo.SetActive(true);
                break;
            case TetronimoShape.S:
                S_PreviewTetronimo.SetActive(true);
                break;
            case TetronimoShape.Z:
                Z_PreviewTetronimo.SetActive(true);
                break;
            case TetronimoShape.J:
                J_PreviewTetronimo.SetActive(true);
                break;
            case TetronimoShape.L:
                L_PreviewTetronimo.SetActive(true);
                break;
        }
    }
}
