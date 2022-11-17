using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Game Data/Map Data")]
    public class MapData : ScriptableObject
    {
        public TileBase tileBaseTop;
        public TileBase tileBaseTopLeftBorder;
        public TileBase tileBaseTopRightBorder;
        public TileBase ground;
    }
}