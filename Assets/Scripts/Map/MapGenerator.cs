using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;

        private int[,] map;

        [SerializeField] private TileBase _tileBase;
        // Start is called before the first frame update
        void Start()
        {
            map = GenerateArray(50, 15, true);
            map = RandomWalkTopSmoothed(map, 432, 3);
            RenderMap(map, _tilemap, _tileBase);
        }
        public static int[,] GenerateArray(int width, int height, bool empty)
        {
            int[,] map = new int[width, height];
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    if (empty)
                    {
                        map[x, y] = 0;
                    }
                    else
                    {
                        map[x, y] = 1;
                    }
                }
            }
            return map;
        }
        public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
        {
            //Clear the map (ensures we dont overlap)
            tilemap.ClearAllTiles(); 
            //Loop through the width of the map
            for (int x = 0; x < map.GetUpperBound(0) ; x++) 
            {
                //Loop through the height of the map
                for (int y = 0; y < map.GetUpperBound(1); y++) 
                {
                    // 1 = tile, 0 = no tile
                    if (map[x, y] == 1) 
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), tile); 
                    }
                }
            }
        }
        public static void UpdateMap(int[,] map, Tilemap tilemap) //Takes in our map and tilemap, setting null tiles where needed
        {
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    //We are only going to update the map,s rather than rendering again
                    //This is because it uses less resoufrces to update tiles to null
                    //As opposed to re-drawifng every single tile (and collision data)
                    if (map[x, y] == 0)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }
        public static int[,] RandomWalkTopSmoothed(int[,] map, float seed, int minSectionWidth) // minimum section width for flat areas
        {
            System.Random rand = new System.Random(seed.GetHashCode());
            int lastHeight = Random.Range(0, map.GetUpperBound(1));
            int nextMove = 0;
            int sectionWidth = 0;
            for (int x = 0; x <= map.GetUpperBound(0); x++)
            {
                nextMove = rand.Next(2);
                switch (nextMove)
                {
                    case 0 when lastHeight > 0 && sectionWidth > minSectionWidth:
                        lastHeight--;
                        sectionWidth = 0;
                        break;
                    case 1 when lastHeight < map.GetUpperBound(1) && sectionWidth > minSectionWidth:
                        lastHeight++;
                        sectionWidth = 0;
                        break;
                }
                sectionWidth++;
                for (int y = lastHeight; y >= 0; y--)
                {
                    map[x, y] = 1;
                }
            }
            return map;
        }
    }
}
