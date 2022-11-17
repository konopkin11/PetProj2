using System.Linq;
using Character;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        private static List<List<int>> _map;
        [SerializeField] public static MapData md;
        public static List<List<int>> GenerateMap(Tilemap tilemap)
        {
            
            _map = GenerateArray(50, 15, true);
            _map = RandomWalkTopSmoothed(_map, Random.value, Random.Range(2, 5));
            RenderMap(_map, tilemap);
            return _map;
        }
        public static List<List<int>> GenerateArray(int width, int height, bool empty)
        {
            List<List<int>> map = new List<List<int>>();//new int[width, height];
            for (int i = 0; i < width; i++)
            {
                map.Add(new List<int>());
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x].Add(empty ? 0 : 1);
                }
            }
            return map;
        }
        public static void RenderMap(List<List<int>> map, Tilemap tilemap)
        {
            
            //Clear the map (ensures we dont overlap)
            tilemap.ClearAllTiles(); 
            //Loop through the width of the map
            int maxY = map[0].Count;
            int maxX = map.Count;
            for (int x = 0; x <  maxX; x++) 
            {
                //Loop through the height of the map
                for (int y = 0; y < maxY ; y++)
                {
                    switch (map[x][y])
                    {
                        /*case 0: //do not really know if that will increase func speed because i heard
                                // that processor can predict things like "every next element is zero -> skip loop"
                        {
                            break;
                        }*/
                        // 1 = tile, 0 = no tile
                        case 1:
                            tilemap.SetTile(new Vector3Int(x, y, 0), md.ground);
                            break;
                        case 2:
                        {
                            tilemap.SetTile(new Vector3Int(x, y, 0), md.tileBaseTop);
                            if (x + 1 != maxX && y != 0)
                            {
                                if (map[x + 1][y - 1] == 2) 
                                    tilemap.SetTile(new Vector3Int(x, y, 0), md.tileBaseTopRightBorder);
                            }
                            if (x != 0 && y != 0)
                            {
                                if (map[x - 1][y - 1] == 2)
                                {
                                    tilemap.SetTile(new Vector3Int(x, y, 0), md.tileBaseTopLeftBorder);
                                    tilemap.SetTransformMatrix(new Vector3Int(x,y,0), 
                                        Matrix4x4.TRS(Vector3.zero, 
                                        Quaternion.Euler(0f,180f, tilemap.GetTransformMatrix(new Vector3Int(x, y, 0)).rotation.eulerAngles.z),
                                        Vector3.one));
                                    // tilemap.GetTile(new Vector3Int(x, y, 0))..GameObject().transform.rotation =
                                     //   new Quaternion(0f, 180f, 0f, 0f);
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }
        public static void UpdateMap(List<List<int>> map, Tilemap tilemap) //Takes in our map and tilemap, setting null tiles where needed
        {
            for (int x = 0; x < map.Count; x++)
            {
                for (int y = 0; y < map[0].Count; y++)
                {
                    //We are only going to update the map,s rather than rendering again
                    //This is because it uses less resoufrces to update tiles to null
                    //As opposed to re-drawifng every single tile (and collision data)
                    if (map[x][y] == 0)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }
        public static List<List<int>> RandomWalkTopSmoothed(List<List<int>> map, float seed, int minSectionWidth) // minimum section width for flat areas
        {
            System.Random rand = new System.Random(seed.GetHashCode());
            int lastHeight = Random.Range(0, map[0].Count);
            int nextMove = 0;
            int sectionWidth = 0;
            for (int x = 0; x < map.Count; x++)
            {
                nextMove = rand.Next(2);
                switch (nextMove)
                {
                    case 0 when lastHeight > 0 && sectionWidth > minSectionWidth:
                        lastHeight--;
                        sectionWidth = 0;
                        break;
                    case 1 when lastHeight < map[0].Count && sectionWidth > minSectionWidth:
                        lastHeight++;
                        sectionWidth = 0;
                        break;
                }
                sectionWidth++;
                if(lastHeight >=0 && lastHeight < map.Count) map[x][lastHeight] = 2; // top tile to paint different tilebase
                for (int y = lastHeight-1; y >= 0; y--)
                {
                    map[x][y] = 1;
                }
            }
            return map;
        }
    }
}
