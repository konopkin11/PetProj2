using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject gameController;
    
    void Start()
    {
        StartGame();
    }
    

    private void StartGame()
    {
        gameController.SetActive(true);
        character.SetActive(true);
        gameController.GetComponent<GameController>().GenerateMap();
    }
}
