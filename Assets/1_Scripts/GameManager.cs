using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Piece
{
    public bool Active = false;
    public bool Settled = false;
    public Color Color = new Color(0, 0, 0, 0);

    public void CopyOtherPiece(Piece other)
    {
        Active = other.Active;
        Settled = other.Settled;
        Color = other.Color;
    }
}

// Every possible tetronimo shape, used to create coordinates in the shape class
public enum TetronimoShape
{
    I,
    O,
    T,
    S,
    Z,
    J,
    L
}

public enum RotationFacing
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}
public enum RotationDirection
{
    LEFT = -1,
    RIGHT = 1
}
public enum MoveDirection
{
    LEFT = -1,
    RIGHT = 1
}
// Shape stores the coordinate for every rotation in an array populating it depending on the passed TetronimoShape shape
public class Shape
{

    private int[][,] iCoordinates = new int[4][,]
            {
                    new int[4,2]{
                        {0, 2},
                        {1, 2},
                        {2, 2},
                        {3, 2},
                    },
                   new int[4,2] {
                        {2, 0},
                        {2, 1},
                        {2, 2},
                        {2, 3},
                    },
                    new int[4,2]{
                        {0, 1},
                        {1, 1},
                        {2, 1},
                        {3, 1},
                    },
                    new int[4,2]{
                        {1, 0},
                        {1, 1},
                        {1, 2},
                        {1, 3},
                    }
            };
    private int[][,] oCoordinates = new int[4][,]
                {
                    new int[4,2]{
                        {1, 1},
                        {2, 1},
                        {1, 2},
                        {2, 2},
                    },
                   new int[4,2] {
                        {1, 1},
                        {2, 1},
                        {1, 2},
                        {2, 2},
                    },
                   new int[4,2] {
                        {1, 1},
                        {2, 1},
                        {1, 2},
                        {2, 2},
                    },
                   new int[4,2] {
                        {1, 1},
                        {2, 1},
                        {1, 2},
                        {2, 2},
                    }
                };
    private int[][,] tCoordinates = new int[4][,]
                {
                   new int[4,2] {
                        {0, 1},
                        {1, 1},
                        {2, 1},
                        {1, 2},
                    },
                   new int[4,2] {
                        {1, 0},
                        {1, 1},
                        {2, 1},
                        {1, 2},
                    },
                  new int[4,2]  {
                        {1, 0},
                        {0, 1},
                        {1, 1},
                        {2, 1},
                    },
                 new int[4,2]   {
                        {1, 0},
                        {0, 1},
                        {1, 1},
                        {1, 2},
                    }
                };
    private int[][,] sCoordinates = new int[4][,]
                {
                  new int[4,2]  {
                        {0, 1},
                        {1, 1},
                        {1, 2},
                        {2, 2},
                    },
                   new int[4,2] {
                        {2, 0},
                        {1, 1},
                        {2, 1},
                        {1, 2},
                    },
                 new int[4,2]   {
                        {0, 0},
                        {1, 0},
                        {1, 1},
                        {2, 1},
                    },
                  new int[4,2]  {
                        {1, 0},
                        {0, 1},
                        {1, 1},
                        {0, 2},
                    }
                };
    private int[][,] zCoordinates = new int[4][,]
                {
                  new int[4,2]  {
                        {0, 2},
                        {1, 2},
                        {1, 1},
                        {2, 1},
                    },
                 new int[4,2]   {
                        {1, 0},
                        {1, 1},
                        {2, 1},
                        {2, 2},
                    },
                   new int[4,2] {
                        {1, 0},
                        {2, 0},
                        {0, 1},
                        {1, 1},
                    },
                  new int[4,2]  {
                        {0, 0},
                        {0, 1},
                        {1, 1},
                        {1, 2},
                    }
                };
    private int[][,] jCoordinates = new int[4][,]
                {
                 new int[4,2]   {
                        {0, 1},
                        {1, 1},
                        {2, 1},
                        {0, 2},
                    },
                  new int[4,2]  {
                        {1, 0},
                        {1, 1},
                        {1, 2},
                        {2, 2},
                    },
                 new int[4,2]   {
                        {2, 0},
                        {0, 1},
                        {1, 1},
                        {2, 1},
                    },
                  new int[4,2]  {
                        {0, 0},
                        {1, 0},
                        {1, 1},
                        {1, 2},
                    }
                };
    private int[][,] lCoordinates = new int[4][,]
                {
                new int[4,2]    {
                        {0, 1},
                        {1, 1},
                        {2, 1},
                        {2, 2},
                    },
                 new int[4,2]   {
                        {1, 0},
                        {2, 0},
                        {1, 1},
                        {1, 2},
                    },
                 new int[4,2]   {
                        {0, 0},
                        {0, 1},
                        {1, 1},
                        {2, 1},
                    },
                  new int[4,2]  {
                        {1, 0},
                        {1, 1},
                        {0, 2},
                        {1, 2},
                    }
                };

    private int[][,] localCoordinates;
    public Shape(TetronimoShape tetronimoShape)
    {
        // getting correct coordinates
        // coordinates are based on the left upper corner
        switch (tetronimoShape)
        {
            case TetronimoShape.I:
                localCoordinates = iCoordinates;
                break;
            case TetronimoShape.O:
                localCoordinates = oCoordinates;
                break;
            case TetronimoShape.T:
                localCoordinates = tCoordinates;
                break;
            case TetronimoShape.S:
                localCoordinates = sCoordinates;
                break;
            case TetronimoShape.Z:
                localCoordinates = zCoordinates;
                break;
            case TetronimoShape.J:
                localCoordinates = jCoordinates;
                break;
            case TetronimoShape.L:
                localCoordinates = lCoordinates;
                break;
        }
    }
    public int[,] GetCoordinates(RotationFacing rotationFacing)
    {
        return localCoordinates[(int)rotationFacing];
    }


}

public class Tetronimo
{

    public int[] Coordinates;
    public Shape shape;
    public RotationFacing rotationFacing;
    public Color Color { get; }

    private Color[] tetronimosColors =
        {
        new Color(0.0f, 1.0f, 1.0f),
        new Color(1.0f, 1.0f, 0.0f),
        new Color(0.5f, 0.0f, 0.5f),
        new Color(0.0f, 1.0f, 0.0f),
        new Color(1.0f, 0.0f, 0.0f),
        new Color(0.0f, 0.0f, 1.0f),
        new Color(1.0f, 0.5f, 0.0f)
    };
    public Tetronimo(TetronimoShape tetronimoShape, int[] coordinates)
    {
        shape = new Shape(tetronimoShape);
        rotationFacing = RotationFacing.UP;
        Coordinates = coordinates;
        Color = tetronimosColors[(int)tetronimoShape];
    }

    //TODO: move this to utility class
    static int mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }
    public int[,] PeekRotation(RotationDirection rotationDirection)
    {
        RotationFacing newFacing = (RotationFacing)mod((int)rotationFacing + (int)rotationDirection, Enum.GetNames(typeof(RotationFacing)).Length);
        RotationFacing tempFacing = rotationFacing;
        rotationFacing = newFacing;
        int[,] coordinates = GetPiecesCoordinates();
        rotationFacing = tempFacing;
        return coordinates;
    }
    public int[,] ApplyRotation(RotationDirection rotationDirection)
    {
        rotationFacing = (RotationFacing)mod((int)rotationFacing + (int)rotationDirection, Enum.GetNames(typeof(RotationFacing)).Length);
        return GetPiecesCoordinates();
    }

    public int[,] ApplyMoveDown()
    {
        Coordinates[1] -= 1;
        return GetPiecesCoordinates();
    }

    public int[,] PeekMoveDown()
    {
        Coordinates[1] -= 1;
        int[,] coordinates = GetPiecesCoordinates();
        Coordinates[1] += 1;
        return coordinates;
    }

    public int[,] ApplyHorizontalMove(MoveDirection direction)
    {
        Coordinates[0] += (int)direction;
        return GetPiecesCoordinates();
    }

    public int[,] PeekHorizontalMove(MoveDirection direction)
    {
        Coordinates[0] += (int)direction;
        int[,] coordinates = GetPiecesCoordinates();
        Coordinates[0] -= (int)direction;
        return coordinates;
    }

    public int[,] GetPiecesCoordinates()
    {
        int[,] coordinates = new int[4, 2];
        int[,] localCoordinates = shape.GetCoordinates(rotationFacing);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                coordinates[i, j] = Coordinates[j] + localCoordinates[i, j];
            }
        }
        return coordinates;
    }
}

public class GameManager : MonoBehaviour
{
    public Button GameOverButton;
    public const int GRID_WIDTH = 10;
    public const int GRID_HEIGHT = 20;
    public int Score = 0;
    public TetronimoShape NextTetronimoShape { get => nextTetronimoShape; }
    public Piece[,] Grid { get => grid; }

    private System.Random random = new System.Random();

    private float tickStep = 0.25f;
    private float tickDeadline = 0.0f;

    private Piece[,] grid = new Piece[GRID_WIDTH, GRID_HEIGHT];

    private TetronimoShape nextTetronimoShape;
    private Tetronimo tetronimo = null;

    private float softDropDeadline = 0.0f;
    private bool softDropActive = false;
    private int maxSoftDropScore = 0;
    private int currentSoftDropScore = 0;

    private int currentLevel = 0;
    private int currentLinesCleared = 0;
    private int[] framesPerGridCellByLevel = {
        48, 43, 38, 33, 28, 23, 18, 8, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 
};

    private int[] linesBeforeLevelIncrease =
    {
        10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 100, 100, 100, 100, 100, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, 200, 200, 200
    };

    private int[] scoreMultiplyer = { 40, 100, 300, 1200 };

    private bool running = true;

    private void Start()
    {
        GameOverButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        GameOverButton.gameObject.SetActive(false);
        for (int x = 0; x < GRID_WIDTH; x++)
        {
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                Piece piece = new Piece();
                grid[x, y] = piece;
            }
        }
        nextTetronimoShape = pickRandomTetronimoShape();
        spawnTetronimo();
    }

    private float accumulatedDelta = 0.0f;
    private float frameDelta = 1.0f / 60.0f;
    private int frameCounter = 0;
    private void Update()
    {
        if (!running)
        {
            return;
        }
        clearTetronimo();
        checkInput();
        saveTetronimo();

        accumulatedDelta += Time.deltaTime;
        while (accumulatedDelta >= frameDelta)
        {
            accumulatedDelta -= frameDelta;
            frameCounter++;
        }
        while(frameCounter>=framesPerGridCellByLevel[Mathf.Min(currentLevel, framesPerGridCellByLevel.Length - 1)])
        {
            frameCounter -= framesPerGridCellByLevel[Mathf.Min(currentLevel, framesPerGridCellByLevel.Length - 1)];
            tick();
        }

        softDropDeadline += Time.deltaTime;
        while (softDropDeadline >= frameDelta)
        {
            softDropDeadline -= frameDelta;
            softDrop();
        }

    }

    private void checkInput()
    {
        checkKeyboardInput();
        checkTouchInput();
    }

    private void checkKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveTetronimo(MoveDirection.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveTetronimo(MoveDirection.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotateTetronimo(RotationDirection.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rotateTetronimo(RotationDirection.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            softDropActive = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            maxSoftDropScore = Mathf.Max(maxSoftDropScore, currentSoftDropScore);
            currentSoftDropScore = 0;
            softDropActive = false;
        }
    }

    private Vector2 startPos;
    private bool slided=false;
    private void checkTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchMovement;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    slided = false;
                    startPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    touchMovement = touch.position - startPos;
                    handleTouchMovement(touchMovement, touch.position, false);
                    break;
                case TouchPhase.Ended:
                    touchMovement = touch.position - startPos;
                    handleTouchMovement(touchMovement, touch.position, true);
                    maxSoftDropScore = Mathf.Max(maxSoftDropScore, currentSoftDropScore);
                    currentSoftDropScore = 0;
                    softDropActive = false;
                    break;
            }
        }
    }

    private void handleTouchMovement(Vector2 touchMovement, Vector2 endPosition, bool touchEnded)
    {
        Debug.Log(touchMovement);
        float slideEpsilon = Screen.width * 0.1f;
        Debug.Log(slideEpsilon);
        if (touchEnded &&  !slided)
        {
            float middle = Screen.width * 0.5f;
            if (endPosition.x < middle)
            {
                rotateTetronimo(RotationDirection.LEFT);
            }
            else
            {
                rotateTetronimo(RotationDirection.RIGHT);
            }
            return;
        }

        if (touchMovement.magnitude >= slideEpsilon)
        {
            slided = true;
            startPos = endPosition;
            if (Mathf.Abs(touchMovement.x) > Mathf.Abs(touchMovement.y))
            {
                if (touchMovement.x >= 0)
                {
                    moveTetronimo(MoveDirection.RIGHT);
                }
                else
                {
                    moveTetronimo(MoveDirection.LEFT);
                }
            }
            else
            {
                if (touchMovement.y <= 0)
                {
                    softDropActive = true;
                }
                else
                {
                    maxSoftDropScore = Mathf.Max(maxSoftDropScore, currentSoftDropScore);
                    currentSoftDropScore = 0;
                    softDropActive = false;
                }
            }
        }

    }

    private void moveTetronimo(MoveDirection direction)
    {
        if (checkLegalHorizontalMove(direction))
        {
            tetronimo.ApplyHorizontalMove(direction);
        }
    }
    private void rotateTetronimo(RotationDirection direction)
    {
        bool rotated = false;
        if (checkLegalRotation(direction))
        {
            tetronimo.ApplyRotation(direction);
            rotated = true;
        }

        //wallkicks
        if (!rotated)
        {
            if (checkLegalHorizontalMove(MoveDirection.RIGHT))
            {
                tetronimo.ApplyHorizontalMove(MoveDirection.RIGHT);
                if (checkLegalRotation(direction))
                {
                    tetronimo.ApplyRotation(direction);
                    rotated = true;
                }
                else
                {
                    tetronimo.ApplyHorizontalMove(MoveDirection.LEFT);
                }
            }
        }
        if (!rotated)
        {
            if (checkLegalHorizontalMove(MoveDirection.LEFT))
            {
                tetronimo.ApplyHorizontalMove(MoveDirection.LEFT);
                if (checkLegalRotation(direction))
                {
                    tetronimo.ApplyRotation(direction);
                    rotated = true;
                }
                else
                {
                    tetronimo.ApplyHorizontalMove(MoveDirection.RIGHT);
                }
            }
        }
    }
    private bool checkLegalHorizontalMove(MoveDirection direction)
    {
        return checkLegalCoordinates(tetronimo.PeekHorizontalMove(direction));
    }
    private bool checkLegalRotation(RotationDirection direction)
    {
        return checkLegalCoordinates(tetronimo.PeekRotation(direction));
    }

    private bool checkLegalCoordinates(int[,] coordinates)
    {

        bool legal = true;

        for (int i = 0; i < 4; i++)
        {
            int x = coordinates[i, 0];
            int y = coordinates[i, 1];
            if (x >= 0 && x < GRID_WIDTH)
            {
                if (y >= 0 && y < GRID_HEIGHT)
                {
                    if (grid[x, y].Active && grid[x, y].Settled)
                    {
                        legal = false;
                    }
                }

            }
            else
            {
                legal = false;
            }
        }
        return legal;
    }
    private void tick()
    {
        moveTetronimoDown();
        Score += checkRowsClear();
    }

    private void softDrop()
    {
        if (!softDropActive)
        {
            return;
        }
        if (!checkTetronimoSettled())
        {
            currentSoftDropScore++;
            clearTetronimo();
            tetronimo.ApplyMoveDown();
            saveTetronimo();
        }
        else
        {
            maxSoftDropScore = Mathf.Max(maxSoftDropScore, currentSoftDropScore);
            currentSoftDropScore = 0;
            softDropActive = false;
        }
    }

    private void spawnTetronimo()
    {
        maxSoftDropScore = 0;
        int[] spawnCoordinates = new int[] { (GRID_WIDTH / 2) - 2, GRID_HEIGHT - 1 };
        tetronimo = new Tetronimo(nextTetronimoShape, spawnCoordinates);
        nextTetronimoShape = pickRandomTetronimoShape();
    }
    private TetronimoShape pickRandomTetronimoShape()
    {
        TetronimoShape randomTetronimoShape = nextTetronimoShape;
        Array values = Enum.GetValues(typeof(TetronimoShape));
        while (randomTetronimoShape == nextTetronimoShape)
        {
            randomTetronimoShape = (TetronimoShape)values.GetValue(random.Next(values.Length));
        }
        return randomTetronimoShape;
    }
    private void moveTetronimoDown()
    {
        if (checkTetronimoSettled())
        {
            settleTetronimo();
            spawnTetronimo();
        }
        else
        {
            clearTetronimo();
            tetronimo.ApplyMoveDown();
            saveTetronimo();
        }
    }
    private bool checkTetronimoSettled()
    {
        int[,] coordinates = tetronimo.PeekMoveDown();
        bool settled = false;

        for (int i = 0; i < 4; i++)
        {
            int x = coordinates[i, 0];
            int y = coordinates[i, 1];
            if (y >= 0)
            {
                if (x >= 0 && x < GRID_WIDTH && y < GRID_HEIGHT)
                {
                    if (grid[x, y].Active && grid[x, y].Settled)
                    {
                        settled = true;
                    }
                }

            }
            else
            {
                settled = true;
            }
        }

        return settled;
    }
    private void clearTetronimo()
    {
        int[,] coordinates = tetronimo.GetPiecesCoordinates();
        for (int i = 0; i < 4; i++)
        {
            int x = coordinates[i, 0];
            int y = coordinates[i, 1];
            if (x >= 0 && x < GRID_WIDTH && y >= 0 && y < GRID_HEIGHT)
            {
                grid[x, y].Active = false;
            }
        }
    }
    private void saveTetronimo()
    {
        int[,] coordinates = tetronimo.GetPiecesCoordinates();
        for (int i = 0; i < 4; i++)
        {
            int x = coordinates[i, 0];
            int y = coordinates[i, 1];
            if (x >= 0 && x < GRID_WIDTH && y >= 0 && y < GRID_HEIGHT)
            {
                grid[x, y].Active = true;
                grid[x, y].Color = tetronimo.Color;
            }
        }
    }
    private void settleTetronimo()
    {
        int[,] coordinates = tetronimo.GetPiecesCoordinates();
        bool lost = false;
        for (int i = 0; i < 4; i++)
        {
            int x = coordinates[i, 0];
            int y = coordinates[i, 1];
            if (x >= 0 && x < GRID_WIDTH && y >= 0 && y < GRID_HEIGHT)
            {
                grid[x, y].Settled = true;
            }
            if (y == GRID_HEIGHT - 1 && x == GRID_WIDTH / 2)
            {
                lost = true;
            }
        }
        Score += maxSoftDropScore;
        if (lost)
        {
            gameOver();
        }
    }
    private int checkRowsClear()
    {
        int clearedRows = 0;
        for (int y = GRID_HEIGHT - 1; y >= 0; y--)
        {
            bool full = true;
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                if (!(grid[x, y].Active && grid[x, y].Settled))
                {
                    full = false;
                }
            }
            if (full)
            {
                clearedRows++;
                clearRow(y);
            }

        }
        if (clearedRows > 0)
        {
            currentLinesCleared += clearedRows;

            while (currentLinesCleared >= linesBeforeLevelIncrease[Mathf.Min(currentLevel, linesBeforeLevelIncrease.Length - 1)])
            {
                currentLinesCleared -= linesBeforeLevelIncrease[Mathf.Min(currentLevel, linesBeforeLevelIncrease.Length - 1)];
                currentLevel = Mathf.Min(currentLevel+1, 29);
                Debug.Log("Level " + currentLevel);
            }
            return scoreMultiplyer[clearedRows - 1] * (currentLevel + 1);
        }
        return 0;
    }
    private void clearRow(int row)
    {
        for (int y = row + 1; y < GRID_HEIGHT; y++)
        {
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                if (!(grid[x, y].Active && !grid[x, y].Settled))
                {
                    grid[x, y - 1].Active = grid[x, y].Active;
                    grid[x, y - 1].Settled = grid[x, y].Settled;
                    grid[x, y - 1].Color = grid[x, y].Color;
                }
            }
        }
    }

    private void gameOver()
    {
        running = false;
        GameOverButton.gameObject.SetActive(true);
    }
}


