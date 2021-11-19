using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Piece
{
    public bool Active = false;
    public bool Settled = false;
    public Color Color = new Color(0, 0, 0, 0);
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
    public enum RotationDirection
    {
        LEFT = -1,
        RIGHT = 1
    }
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

    public void MoveDown()
    {
        Coordinates[1] -= 1;
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
    public const int GRID_WIDTH = 10;
    public const int GRID_HEIGHT = 20;
    public int Score = 0;
    public TetronimoShape NextTetronimoShape { get => nextTetronimoShape; }
    public Piece[,] Grid { get => grid;}

    private System.Random random = new System.Random();

    private float tickStep = 0.25f;
    private float tickDeadline = 0.0f;

    private int[] spawnCoordinates = new int[] { GRID_WIDTH/2, GRID_HEIGHT };
    private Piece[,] grid = new Piece[GRID_WIDTH, GRID_HEIGHT];

    private TetronimoShape nextTetronimoShape;
    private Tetronimo tetronimo = null;


    
    private void Start()
    {
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
    private void FixedUpdate()
    {
        checkInput();
        tickDeadline += Time.deltaTime;
        if (tickDeadline >= tickStep)
        {
            tickDeadline -= tickStep;
            tick();
        }
    }

    private void checkInput()
    {
        //TODO: check for rotation
            //TODO: check for rotation collision
        //TODO: check for sideways movement
            //TODO: check for sideways collision
        //TODO: check for down movement speedup
            //TODO: check for settled

    }

    private void tick()
    {
        moveTetronimo();
        Score += checkRowsClear();

    }

    private void spawnTetronimo()
    {
        tetronimo = new Tetronimo(nextTetronimoShape, spawnCoordinates);
        // TODO: check for loss
        // TODO: update pieces array
        nextTetronimoShape = pickRandomTetronimoShape();
    }
     private TetronimoShape pickRandomTetronimoShape()
    {
        Array values = Enum.GetValues(typeof(TetronimoShape));
        TetronimoShape randomTetronimoShape = (TetronimoShape)values.GetValue(random.Next(values.Length));
        return randomTetronimoShape;
    }
    private void moveTetronimo()
    {
        if (checkTetronimoSettled())
        {
            //TODO: mark pieces as settled 
            spawnTetronimo();
        }
        else
        {
            clearTetronimo();
            tetronimo.MoveDown();
            saveTetronimo();
        }
    }

    private bool checkTetronimoSettled()
    {
        //TODO: check if moving down tetronimo once more would intersect with already settled pieces or if they already hit the last row
        bool settled = false;
        return settled;
    }

    private void clearTetronimo()
    {
        int[,] coordinates = tetronimo.GetPiecesCoordinates();
        for (int i = 0; i < 4; i++)
        {
            int x = coordinates[i, 0];
            int y = coordinates[i, 1];
            if (x>=0 && x<GRID_WIDTH && y >= 0 && y < GRID_HEIGHT)
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
    
    private int checkRowsClear()
    {
        //TODO: check for full rows
        //TODO: clear full rows
        //TODO: move rows above down wards once for each cleared row
        //TODO: return score based on how many rows were cleared
        return 0;
    }
}


