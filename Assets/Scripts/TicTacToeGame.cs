using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class TicTacToeGame : MonoBehaviour
{
    [SerializeField] private GameObject gameBoardControllerPrefab;
    [SerializeField] private GameObject gameLogicControllerPrefab;

    private IGameBoardController gameBoardController;
    private IGameLogicController gameLogicController;

    private void Awake()
    {
        gameBoardController = Instantiate(gameBoardControllerPrefab, gameObject.transform).GetComponent<IGameBoardController>();
        gameLogicController = Instantiate(gameLogicControllerPrefab,gameObject.transform).GetComponent<IGameLogicController>();
        
    }

    private void Start()
    {
        gameBoardController.Inject(gameLogicController);
        gameLogicController.Inject(gameBoardController);
    }
}
