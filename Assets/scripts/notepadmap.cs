using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class notepadmap : MonoBehaviour


{// mostly the same assests from the gen script 
    public TextAsset mapFile;
    public MapGenerator mapGenerator;
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile floorTile;
    public Tile doorTile;
    public Tile chestTile;

    void Start()
    {
        if (mapFile != null)
        {
            mapGenerator.ConvertMapToTilemap(mapFile.text);
        }
        else
        {
            Debug.LogError("Map file not assigned!"); // if there is no map assigned to the assest at the top 
        }
    }

    public void ConvertMapToTilemap(string mapData) // just kinda copy and pasted from the map gen script 
    {
        int width = mapData.Split('\n')[0].Length;
        int height = mapData.Split('\n').Length;

        tilemap.ClearAllTiles(); // Clear existing tiles

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char tileChar = mapData[y * (width + 1) + x];
                Vector3Int tilePosition = new Vector3Int(x, y, 0);


                switch (tileChar)
                {
                    case '#':
                        tilemap.SetTile(tilePosition, wallTile);
                        break;
                    case '*':
                        tilemap.SetTile(tilePosition, floorTile);
                        break;
                    case 'O':
                        tilemap.SetTile(tilePosition, doorTile);
                        break;
                    case '$':
                        tilemap.SetTile(tilePosition, chestTile);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}