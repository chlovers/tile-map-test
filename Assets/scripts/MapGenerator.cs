using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapGenerator : MonoBehaviour
{
   // 2 seperate tiles maps one for moving the other for the walls 
    public Tilemap tilemap;
    public Tilemap collisonmap;
    public Tile wallTile;
    public Tile floorTile;
    public Tile doorTile;
    public Tile chestTile;
    public GameObject Player;
    private bool doorPlaced = false; // this makes sure it knows i placed a door casue sometimes it was spawning so man doors lol

  
    private bool IsMapConnected(char[,] map, int width, int height)
    {
        bool[,] visited = new bool[width, height];
        Queue<Vector2Int> queue = new Queue<Vector2Int>(); //start of the queue

        
        for (int y = 1; y < height - 1; y++)  // looking for the first floor tile in the map 
        {
            for (int x = 1; x < width - 1; x++)
            {
                if (map[x, y] == '*' || map[x, y] == 'O') // floor tile 
                {
                    queue.Enqueue(new Vector2Int(x, y));
                    visited[x, y] = true;
                    break;
                }
            }
            if (queue.Count > 0) break;
        }

        if (queue.Count == 0) return false; // if we cant find a good starting spot

        
        while (queue.Count > 0) // we starting the "BFS" to check all the floor tiels 
        {
            Vector2Int current = queue.Dequeue();
            int x = current.x;
            int y = current.y;

            // checking the tiles around 
            foreach (var direction in new Vector2Int[] { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1) })
            {
                int nx = x + direction.x;
                int ny = y + direction.y;

                if (nx >= 0 && nx < width && ny >= 0 && ny < height && !visited[nx, ny])
                {
                    if (map[nx, ny] == '*' || map[nx, ny] == 'O') // If it's a floor tile
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue(new Vector2Int(nx, ny));
                    }
                }
            }
        }

        // check if all the floor tiles were seen 
        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                if ((map[x, y] == '*' || map[x, y] == 'O') && !visited[x, y])
                {
                    return false; // if there is a unreachable floor time
                }
            }
        }

        return true; // you can reach the door 
    }

    
    public string GenerateMapString(int width, int height)
    {
        char[,] map;
        bool mapIsConnected;

        do
        {
            map = new char[width, height];

            // putting the walls in 
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = '#'; // settin the wall 
                }
            }

            
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    if (Random.Range(0, 100) < 70) // added a 70% chance to be a floor tile so i can have walls in the generation 
                    {
                        map[x, y] = '*'; // mmm floor tiel
                    }
                }
            }

            
            doorPlaced = false; // checking if the door was place, if it isnt we doing this loop untill it is 
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    if (map[x, y] == '*' && IsAdjacentToWall(x, y, map) && !doorPlaced)
                    {
                        map[x, y] = 'O'; // puttin in  the door
                        doorPlaced = true; // flipping the bool to show the door was put down
                        break; // break ing out the loop so it stops looking for doors
                    }
                }
                if (doorPlaced) break; // now that the door is down we can move on to the chests 
            }

            
            int chestCount = 0;
            while (chestCount < 3) // set it so we can only have a max of 3 chests 
            {
                int x = Random.Range(1, width - 1);
                int y = Random.Range(1, height - 1);

                if (map[x, y] == '*' && Random.Range(0, 100) < 15) // 15% chance to put a chest
                {
                    map[x, y] = '$'; // Chest $$$ 
                    chestCount++;
                }
            }

            // cHeck if the map is connected
            mapIsConnected = IsMapConnected(map, width, height);

        } while (!mapIsConnected); // regenerate the map until it is usable and connected 

        // convert the map array to a string 
        string mapString = "";
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                mapString += map[x, y];
            }
            mapString += "\n"; // so we can have a  line at the end of each row
        }

        return mapString;
    }

    
    private bool IsAdjacentToWall(int x, int y, char[,] map)
    {
        return map[x - 1, y] == '#' || map[x + 1, y] == '#' ||
               map[x, y - 1] == '#' || map[x, y + 1] == '#';
    }

    // convert the map string to tiles in the tilemap
    public void ConvertMapToTilemap(string mapData)
    {
        int width = mapData.Split('\n')[0].Length;
        int height = mapData.Split('\n').Length;

        tilemap.ClearAllTiles(); // Clear existing tiles
        collisonmap.ClearAllTiles();// clearing the tiles for the collisoinmap

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
                        collisonmap.SetTile(tilePosition, wallTile); //setting the walls for the collison map
                        break;
                    case '*':
                        tilemap.SetTile(tilePosition, floorTile);
                        break;
                    case 'O':
                        tilemap.SetTile(tilePosition, doorTile);
                        break;
                    case '$':
                        tilemap.SetTile(tilePosition, chestTile);
                        collisonmap.SetTile(tilePosition, chestTile);
                        break;
                    default:
                        break;
                }
            }
        }
    }

   

    void Start()
    {
        string map = GenerateMapString(15, 20); // stetting and generating the map in the speficed size
        Debug.Log(map); 
        ConvertMapToTilemap(map); // converting to tilemap
        
    }
}