using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;


//Observer pattern
public class GameController : MonoBehaviour
{
    public static event Action<List<List<int>>> MapRerender = delegate(List<List<int>> map) {  };
    [SerializeField] private MapData mapData;
    public static Tilemap Tilemap;
    protected virtual void OnMapRerender(List<List<int>> obj)
    {
        MapRerender?.Invoke(obj);
    }

    public void GenerateMap()
    {
        MapGenerator.md = mapData;
        Tilemap = GameObject.Find("Tilemap Base").GetComponent<Tilemap>();
        OnMapRerender(MapGenerator.GenerateMap(Tilemap));
        
    }
    //temp
    public void button()
    {
        GenerateMap();
    }
}
