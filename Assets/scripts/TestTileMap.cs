using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TestTileMap : MonoBehaviour
{
    public Tilemap tilemap;
    public Camera myCam;
    public TileBase myTile;
    public int[,] multidimensionalmap = new int[25, 25];


    void Start()
    {
       for (int y = 0; y < multidimensionalmap.GetLength(1); y++)
        {
            for (int x = 0; x <multidimensionalmap.GetLength(0); x++)
            {
                multidimensionalmap[x, y] = Random.Range(0,2);
            }
        }
        DrawTileMap();
    }

   void DrawTileMap()
    {
        for (int y = 0; y < multidimensionalmap.GetLength(1); y++)
        {
            for (int x = 0; x < multidimensionalmap.GetLength(0); x++)
            {
                if (multidimensionalmap[x, y] == 0)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null); 
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), myTile); 
                }
            }
        }


    }
  
    void CountNextCells()
    {



    }

    void Update()
    {
        
    }
}
