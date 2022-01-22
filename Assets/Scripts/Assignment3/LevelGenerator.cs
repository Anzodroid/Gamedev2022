using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject DestroyWalls;
    public GameObject DestroyItems;
    public GameObject DestroyEnemies;
    public GameObject[] TileMaps;
    private float Center_X;
    private float Center_Y;
    private float Pos_X;
    private float Pos_Y;
    private float RowLength;
    private float ColLength;



    public int[,] levelMap = {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
            };


    /*    // ## Test Map Large ##
        public int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,4,0,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        };*/


    // ## Test Map Small ##
    /*    public int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,3,5,3,4,4,4,3,5,4},
        {2,5,3,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,5,0,0,0,4,0,0,0},
        };*/



    /*    public int[,] levelMap =   {
    { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 1, 0, 0, 2, 0, 2, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 2, 5, 5, 5, 5, 5, 5, 6, 2, 0, 0, 2, 0, 2, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 2, 5, 3, 4, 4, 4, 3, 5, 2, 0, 0, 2, 0, 1, 2, 2, 1, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 2, 5, 4, 0, 0, 0, 4, 5, 2, 0, 0, 2, 5, 5, 5, 5, 2, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 2, 0, 1, 2, 2, 1, 5, 3, 4, 4, 4, 3, 5, 1, 2, 2, 1, 5, 3, 3, 5, 2, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 5, 1, 2 },
    { 0, 0, 0, 0, 0, 0, 0, 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 4, 4, 3, 5, 3, 3, 5, 4, 4, 5, 5, 5 },
    { 0, 0, 0, 0, 0, 0, 0, 2, 5, 4, 0, 0, 4, 5, 4, 0, 0, 0, 0, 0, 4, 5, 4, 4, 5, 4, 4, 5, 3, 4 },
    { 0, 0, 1, 2, 2, 2, 2, 1, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 0, 4, 5, 4, 4, 5, 3, 3, 5, 3, 4 },
    { 0, 0, 2, 5, 5, 5, 5, 5, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 0, 4, 5, 4, 4, 5, 5, 5, 5, 5, 5 },
    { 0, 0, 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 3, 5, 3, 4, 3, 5, 4, 0, 4, 5, 4, 4, 5, 3, 3, 5, 3, 4 },
    { 0, 0, 2, 5, 4, 0, 0, 4, 5, 4, 0, 0, 4, 5, 4, 0, 4, 5, 4, 0, 4, 5, 3, 3, 5, 4, 4, 5, 4, 0 },
    { 0, 0, 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 3, 5, 3, 4, 3, 5, 3, 4, 3, 5, 5, 5, 5, 4, 4, 5, 4, 0 },
    { 0, 0, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 4, 4, 3, 4, 5, 4, 0 },
    { 0, 0, 7, 4, 4, 4, 3, 5, 3, 3, 5, 3, 3, 5, 3, 4, 4, 4, 4, 4, 3, 5, 4, 0, 0, 0, 4, 5, 4, 0 },
    { 0, 0, 7, 4, 4, 4, 3, 5, 4, 4, 5, 4, 4, 5, 3, 4, 4, 4, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 3, 4 },
    { 0, 0, 2, 5, 5, 5, 5, 5, 4, 4, 5, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
    { 0, 0, 2, 5, 3, 4, 3, 5, 4, 4, 5, 4, 3, 4, 4, 4, 4, 4, 3, 5, 3, 4, 4, 3, 5, 3, 3, 5, 3, 4 },
    { 0, 0, 2, 5, 4, 0, 4, 5, 4, 4, 5, 4, 0, 0, 0, 0, 0, 0, 4, 5, 4, 0, 0, 4, 5, 4, 4, 5, 3, 4 },
    { 2, 2, 1, 5, 3, 4, 3, 5, 3, 3, 5, 3, 4, 4, 4, 4, 4, 4, 3, 5, 4, 0, 0, 4, 5, 4, 4, 5, 5, 5 },
    { 0, 0, 0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 0, 0, 4, 5, 4, 4, 5, 3, 4 },
    { 2, 2, 2, 2, 2, 2, 1, 5, 3, 4, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 4, 0, 0, 4, 5, 4, 4, 5, 4, 0 },
    { 0, 0, 0, 0, 0, 0, 2, 5, 3, 4, 4, 3, 4, 5, 4, 3, 4, 4, 3, 5, 3, 4, 4, 3, 5, 3, 3, 5, 4, 0 },
    { 0, 0, 0, 0, 0, 0, 2, 5, 5, 5, 5, 4, 4, 5, 4, 4, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 0 },
    { 0, 0, 0, 0, 0, 0, 7, 4, 4, 3, 5, 4, 4, 5, 4, 4, 5, 3, 4, 4, 4, 4, 3, 5, 3, 3, 0, 0, 4, 0 },
    { 0, 0, 0, 0, 0, 0, 7, 4, 4, 3, 5, 3, 3, 5, 3, 3, 5, 4, 0, 0, 0, 0, 4, 5, 4, 4, 0, 0, 3, 4 },
    { 0, 0, 1, 2, 2, 2, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3, 4, 4, 4, 4, 3, 5, 4, 4, 0, 0, 0, 0 },
    { 0, 0, 2, 6, 5, 5, 5, 5, 3, 3, 5, 3, 4, 4, 4, 3, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 0, 3, 4, 0 },
    { 0, 0, 2, 5, 3, 4, 4, 4, 3, 4, 5, 4, 0, 0, 0, 3, 4, 4, 4, 3, 5, 3, 3, 5, 4, 4, 0, 4, 0, 0 },
    { 0, 0, 2, 5, 4, 0, 0, 0, 0, 4, 5, 4, 0, 0, 0, 0, 0, 0, 0, 4, 5, 4, 4, 5, 4, 4, 0, 4, 0, 0 }};*/


    // Start Method Removed for Assignment 4 

/*    void Start()
    {
        DestroyGameObject();
        Center_X = 0;
        Center_Y = 0;
        RowLength = levelMap.GetLength(0); // number of rows in array : 15
        ColLength = levelMap.GetLength(1); // number of columns in array : 14

        // Create each of the 4 Quadrants 
        Quad1();
        Quad2();
        Quad3();
        Quad4();

        // Rotate the corner pieces
        OutWallRotate();
        InWallRotate();
    }*/

    void DestroyGameObject()
    {
        //Destroy(DestroyWalls);
      //  Destroy(DestroyItems);
       // Destroy(DestroyEnemies);
    }


    List<GameObject> TilesOutCorner = new List<GameObject>(); // create a list (dont need to know the array size in advance)
    List<GameObject> TilesOutside = new List<GameObject>(); 
    List<GameObject> TilesInCorner = new List<GameObject>(); 
    List<GameObject> TilesInside = new List<GameObject>();


    void InWallRotate()
    {
        for (int i = 0; i < TilesInCorner.Count; i++)
        {

            Vector3 CornerPos = TilesInCorner[i].transform.position;

            float X = 99;
            float Y = 99;
            float Z = 99;

            for (int j = 0; j < TilesInCorner.Count; j++)
            {
                Vector3 WallPos = TilesInCorner[j].transform.position - CornerPos;
                if (WallPos.x == 1 && WallPos.y == 0 || WallPos.x == -1f && WallPos.y == 0)
                {
                    X = WallPos.x;
                }
                if (WallPos.x == 0 && WallPos.y == 1 || WallPos.x == 0 && WallPos.y == -1f)
                {
                    Y = WallPos.y;
                }
            }

            for (int j = 0; j < TilesInside.Count; j++)
            {
                Vector3 WallPos = TilesInside[j].transform.position - CornerPos;
                if (WallPos.x == 1 && WallPos.y == 0 || WallPos.x == -1f && WallPos.y == 0)
                {
                    X = WallPos.x;
                }
                if (WallPos.x == 0 && WallPos.y == 1 || WallPos.x == 0 && WallPos.y == -1f)
                {
                    Y = WallPos.y;
                }
            }
            // Assign rotation
            if (X == 1 && Y == -1)
            {
                Z = 0;
            }
            else if (X == 1 && Y == 1)
            {
                Z = 90;
            }
            else if (X == -1 && Y == 1)
            {
                Z = 180;
            }
            else if (X == -1 && Y == -1)
            {
                Z = 270;
            }
            else
            {
                Debug.Log("Error");
            }
            TilesInCorner[i].transform.rotation = Quaternion.Euler(0f, 0f, Z);
        }
    }



    void OutWallRotate()
    {
        for (int i = 0; i < TilesOutCorner.Count; i++)
        {
           Vector3 CornerPos = TilesOutCorner[i].transform.position;

            float X = 99;
            float Y = 99;
            float Z = 99;

            for (int j = 0; j < TilesOutside.Count; j++)
            {
                Vector3 WallPos = TilesOutside[j].transform.position - CornerPos;
                if (WallPos.x == 1 && WallPos.y == 0 || WallPos.x == -1f && WallPos.y == 0)
                {
                    X = WallPos.x;
                }
                if (WallPos.x == 0 && WallPos.y == 1 || WallPos.x == 0 && WallPos.y == -1f)
                {
                    Y = WallPos.y;
                }
            }

            // Assign rotation
            if (X == 1 && Y == -1)
            {
                Z =0;
            }
            else if (X == 1 && Y == 1)
            {
                Z = 90;
            }
            else if (X == -1 && Y == 1)
            {
                Z = 180;
            }
            else if (X == -1 && Y == -1)
            {
                Z = 270;
            }
            else
            {
                Debug.Log("Error");
            }

            TilesOutCorner[i].transform.rotation = Quaternion.Euler(0f, 0f, Z);
        }
    }

    int OutTiles(float x, float y, float Pos_X, float Pos_Y)
    {
        for (int i = 0; i < TilesOutCorner.Count; i++)
        {
            Vector3 wall_distance = TilesOutCorner[i].transform.position - new Vector3(Pos_X, Pos_Y, 0);

            if (wall_distance.x == x && wall_distance.y == 0f)
            {
                return 1;
            }
            else if (wall_distance.x == 0f && wall_distance.y == y)
            {
                return 2;
            }
            else 
            {
                for (int j = 0; j < TilesOutside.Count; j++)
                {
                    Vector3 wall_distance2 = TilesOutside[j].transform.position - new Vector3(Pos_X, Pos_Y, 0);

                    if (wall_distance2.x == x && wall_distance2.y == 0f)
                    {
                        return 3;
                    }
                    else if (wall_distance2.x == 0f && wall_distance2.y == y)
                    {
                        return 4;
                    }
                    continue;
                }
            }
        }
        return 0;
    }

    int InTiles(float x, float y, float Pos_X, float Pos_Y)
    {
        int bestMatch = 0;
        for (int i = 0; i < TilesInCorner.Count; i++)
        {
            Vector3 wall_distance = TilesInCorner[i].transform.position - new Vector3(Pos_X, Pos_Y, 0);

            if (wall_distance.x == x && wall_distance.y == 0f)
            {
                bestMatch = 1;
            }
            if (wall_distance.x == 0f && wall_distance.y == y)
            {
                bestMatch = 2;
            }
        }

        if (bestMatch == 0) {

            for (int j = 0; j < TilesInside.Count; j++)
            {
                Vector3 wall_distance2 = TilesInside[j].transform.position - new Vector3(Pos_X, Pos_Y, 0);
                if (wall_distance2.x == x && wall_distance2.y == 0f)
                {
                    bestMatch = 3;
                }

                if (wall_distance2.x == 0f && wall_distance2.y == y)
                {
                    bestMatch = 4;
                }
            }
        }
        return bestMatch;
    }
        
    
    void Quad1()
    {
        Quaternion Qua = Quaternion.Euler(0, 0, 90);

        float horizontal = 90f;
        float vertical = 0f;
        float ArrayPos = ColLength;
        float Adj_X;

        for (int row = 0; row < RowLength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {
                Adj_X = ColLength + ArrayPos - 1;
                Vector3 pos = new Vector3(Adj_X, Pos_Y, 0);
                Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                switch (levelMap[row, col])
                {
                    case 0:
                        Instantiate(TileMaps[0], new Vector3(Adj_X, Pos_Y, 0), Qua);
                        break;

                    case 1:
                        if (TilesOutCorner.Count == 0)
                        {
                            TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 270)));
                        }
                        else
                        {
                            TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Adj_X, Pos_Y, 0), Qua));
                        }
                        break;
                    case 2:

                        switch (OutTiles(1, 1, Adj_X, Pos_Y))
                            {
                            case 0:
                                Debug.Log("#### " + pos + " #####");
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, quat));
                                break;
                            case 1:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 2:
                               TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                            case 3:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 4:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                        }
                        break;

                    case 3:
                        TilesInCorner.Add(Instantiate(TileMaps[3], new Vector3(Adj_X, Pos_Y, 0), Qua));
                        break;
                    case 4:
                        switch (InTiles(1, 1, Adj_X, Pos_Y))
                        {
                            case 0:
                                Debug.Log(" -----------Unknown TileMap------------#### " + pos + " #####--- ");
                                TilesInside.Add(Instantiate(TileMaps[4], pos, quat));
                                break;
                            case 1:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 2:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                            case 3:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, TilesInside[TilesInside.Count - 1].transform.eulerAngles.z)));
                                break;
                            case 4:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                        }
                        break;
                    case 5:
                        Instantiate(TileMaps[5], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 6:
                        Instantiate(TileMaps[6], new Vector3(Adj_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 7:
                        TilesInCorner.Add(Instantiate(TileMaps[7], new Vector3(Adj_X, Pos_Y, 0), Qua));
                        break;
                }
                ArrayPos--;

                if (ArrayPos <= 0)
                {
                    ArrayPos = ColLength;
                }
            }
            Pos_Y--;

            if (Pos_Y == Center_Y - RowLength)
            {
                Pos_Y = Center_Y;
            }
        }

    }



    void Quad2()
    {
        Quaternion Qua = Quaternion.Euler(0, 0, 90);

        float horizontal = 90f;
        float vertical = 0f;

        for (int row = 0; row < RowLength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {
                Vector3 pos = new Vector3(Pos_X, Pos_Y, 0);
                Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                switch (levelMap[row, col])
                {

                    case 0:
                        Instantiate(TileMaps[0], new Vector3(Pos_X, Pos_Y, 0), Qua);
                        break;

                    case 1:
                        if (TilesOutCorner.Count == 0)
                        {
                            TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, 270)));
                        }
                        else
                        {
                            TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Pos_X, Pos_Y, 0), Qua));
                        }
                        break;
                    case 2:

                        switch (OutTiles(-1, 1, Pos_X, Pos_Y))
                        {
                            case 0:
                                Debug.Log("#### " + pos + " #####");
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, quat));
                                break;
                            case 1:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 2:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                            case 3:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 4:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                        }
                        break;

                    case 3:
                        TilesInCorner.Add(Instantiate(TileMaps[3], new Vector3(Pos_X, Pos_Y, 0), Qua));
                        break;
                    case 4:
                        switch (InTiles(-1, 1, Pos_X, Pos_Y))
                        {
                            case 0:
                                Debug.Log(" -----------Unknown TileMap------------#### " + pos + " #####--- ");
                                TilesInside.Add(Instantiate(TileMaps[4], pos, quat));
                                break;
                            case 1:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 2:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                            case 3:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, TilesInside[TilesInside.Count - 1].transform.eulerAngles.z)));
                                break;
                            case 4:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                        }
                        break;
                    case 5:
                        Instantiate(TileMaps[5], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 6:
                        Instantiate(TileMaps[6], new Vector3(Pos_X, Pos_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 7:
                        TilesInCorner.Add(Instantiate(TileMaps[7], new Vector3(Pos_X, Pos_Y, 0), Qua));
                        break;
                }
                Pos_X++;
                if (Pos_X == Center_X + ColLength)
                {
                    Pos_X = Center_X;
                }
            }
            Pos_Y--;
        }

    }

    void Quad3()
    {
        Quaternion Qua = Quaternion.Euler(0, 0, 90);
        float horizontal = 90f;
        float vertical = 0f;
        float ArrayPos = Center_Y - RowLength;
        float Adj_Y;


        for (int row = 0; row < RowLength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {
                Adj_Y = -RowLength + ArrayPos + 2;
                Vector3 pos = new Vector3(Pos_X, Adj_Y, 0);
                Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                switch (levelMap[row, col])
                {
                    case 0:
                        Instantiate(TileMaps[0], new Vector3(Pos_X, Adj_Y, 0), Qua);
                        break;

                    case 1:
                        if (TilesOutCorner.Count == 0)
                        {
                            TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 270)));
                        }
                        else
                        {
                            TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Pos_X, Adj_Y, 0), Qua));
                        }
                        break;
                    case 2:

                        switch (OutTiles(-1, -1, Pos_X, Adj_Y))
                        {
                            case 0:
                                Debug.Log("#### " + pos + " #####");
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, quat));
                                break;
                            case 1:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 2:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                            case 3:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 4:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                        }
                        break;

                    case 3:
                        TilesInCorner.Add(Instantiate(TileMaps[3], new Vector3(Pos_X, Adj_Y, 0), Qua));
                        break;
                    case 4:
                        switch (InTiles(-1, -1, Pos_X, Adj_Y))
                        {
                            case 0:
                                Debug.Log(" -----------Unknown TileMap------------#### "+ pos + " #####--- ");
                                TilesInside.Add(Instantiate(TileMaps[4], pos, quat));
                                break;
                            case 1:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 2:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                            case 3:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, TilesInside[TilesInside.Count - 1].transform.eulerAngles.z)));
                                break;
                            case 4:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                        }
                        break;
                    case 5:
                        Instantiate(TileMaps[5], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 6:
                        Instantiate(TileMaps[6], new Vector3(Pos_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 7:
                        TilesInCorner.Add(Instantiate(TileMaps[7], new Vector3(Pos_X, Adj_Y, 0), Qua));
                        break;
                }
                Pos_X++;
                if (Pos_X == Center_X + ColLength)
                {
                    Pos_X = Center_X;
                }
            }
            ArrayPos++;
        }

    }


    void Quad4()
    {
        Quaternion Qua = Quaternion.Euler(0, 0, 90);
        float horizontal = 90f;
        float vertical = 0f;
        float ArrayPosX = ColLength;
        float ArrayPosY = Center_Y - RowLength;
        float Adj_X;
        float Adj_Y ;

        for (int row = 0; row < RowLength; row++)
        {

            for (int col = 0; col < ColLength; col++)
            {
                Adj_X = ColLength + ArrayPosX - 1;
                Adj_Y = -RowLength + ArrayPosY + 2;
                Vector3 pos = new Vector3(Adj_X, Adj_Y, 0);
                Quaternion quat = Quaternion.Euler(0, 0, horizontal);

                switch (levelMap[row, col])
                {

                    case 0:
                        Instantiate(TileMaps[0], new Vector3(Adj_X, Adj_Y, 0), Qua);
                        break;

                    case 1:
                        if (TilesOutCorner.Count == 0)
                        {
                            TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 270)));
                        }
                        else
                        {
                            TilesOutCorner.Add(Instantiate(TileMaps[1], new Vector3(Adj_X, Adj_Y, 0), Qua));
                        }
                        break;
                    case 2:

                        switch (OutTiles(1, -1, Adj_X, Adj_Y))
                        {
                            case 0:
                                Debug.Log("#### " + pos + " #####");
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, quat));
                                break;
                            case 1:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 2:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                            case 3:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 4:
                                TilesOutside.Add(Instantiate(TileMaps[2], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                        }
                        break;

                    case 3:
                        TilesInCorner.Add(Instantiate(TileMaps[3], new Vector3(Adj_X, Adj_Y, 0), Qua));
                        break;
                    case 4:
                        switch (InTiles(1, -1, Adj_X, Adj_Y))
                        {
                            case 0:
                                Debug.Log(" -----------NO IDEA----------#### " + pos + " #####--- ");
                                TilesInside.Add(Instantiate(TileMaps[4], pos, quat));
                                break;
                            case 1:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, horizontal)));
                                break;
                            case 2:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                            case 3:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, TilesInside[TilesInside.Count - 1].transform.eulerAngles.z)));
                                break;
                            case 4:
                                TilesInside.Add(Instantiate(TileMaps[4], pos, Quaternion.Euler(0, 0, vertical)));
                                break;
                        }
                        break;
                    case 5:
                        Instantiate(TileMaps[5], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 6:
                        Instantiate(TileMaps[6], new Vector3(Adj_X, Adj_Y, 0), Quaternion.Euler(0, 0, 0));
                        break;
                    case 7:
                        TilesInCorner.Add(Instantiate(TileMaps[7], new Vector3(Adj_X, Adj_Y, 0), Qua));
                        break;
                }
                ArrayPosX--;

                if (ArrayPosX <= 0)
                {
                    ArrayPosX = ColLength;
                }
            }
            ArrayPosY++;
        }

    }


}


