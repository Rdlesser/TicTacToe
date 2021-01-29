using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class GameBoardController : MonoBehaviour, IGameBoardController
{
    [SerializeField] private GameObject gameBoardViewPrefab;
    [SerializeField] private GameObject hudViewPrefab;
    [SerializeField] private Canvas gameBoardCanvas;

    private IGameBoardView gameBoardView;
    private IHudView hudView;
    private IGameLogicController gameLogic;

    // private Canvas gameBoardCanvas;

    private void Awake()
    {
        gameBoardView = Instantiate(gameBoardViewPrefab, gameBoardCanvas.transform).GetComponent<IGameBoardView>();
        hudView = Instantiate(hudViewPrefab, gameBoardCanvas.transform).GetComponent<IHudView>();
    }

    private void Start()
    {
        gameBoardView?.Inject(this);
    }

    public void OnCellClicked(int cellId)
    {
        gameBoardView.MarkCell(cellId, gameLogic.GetCurrentPlayer());
        gameLogic.MarkCell(cellId);

    }

    public void ShowHint(int cellId)
    {
        gameBoardView.ShowHint(cellId);
    }

    public void Undo(int cellId)
    {
        gameBoardView.UnMarkCell(cellId);
    }

    public void Reset()
    {
        gameBoardView.ResetGame();
    }

    public void SetInputAvailable(bool isAvailable)
    {
        gameBoardView.SetInputAvailable(isAvailable);
    }

    public void EndGame()
    {
        gameBoardView.EndGame();
    }

    public void Inject(IGameLogicController gameLogic)
    {
        this.gameLogic = gameLogic;
        gameLogic.Inject(hudView);
    }
    
}
