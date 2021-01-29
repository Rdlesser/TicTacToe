using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLogicController : MonoBehaviour, IGameLogicController
{
    [SerializeField] private Player[] players;
    [SerializeField] private int turnTime = 5;
    [SerializeField] private GameData gameData;

    private int[] gridStatus;
    private Player currentPlayer;
    private int turnNumber;
    private Stack<int> turnPicks;

    private IHudView hudView;
    private IGameBoardController gameBoardController;

    private Coroutine aIPlayerRoutine;

    public GameMode GetGameMode()
    {
        return gameData.GameMode;
    }

    public void Restart()
    {
        gameBoardController.Reset();
        hudView.ResetGame();
        hudView.ReStartClock(turnTime);
        ResetGame();
    }

    private void Start()
    {
        hudView.Inject(this);
        if (gameData.GameMode == GameMode.HvsC)
        {
            players[1].playerController = PlayerController.AI;
        }
        else if (gameData.GameMode == GameMode.CvsC)
        {
            players[0].playerController = PlayerController.AI;
            players[1].playerController = PlayerController.AI;
        }
        ResetGame();
        if (gameData.GameMode == GameMode.CvsC)
        {
            RunAITurn();
        }
    }

    private void ResetGame()
    {
        if (aIPlayerRoutine != null)
        {
            StopCoroutine(aIPlayerRoutine);
        }
        if (gridStatus == null)
        {
            gridStatus = new[] {-1, -1, -1, -1, -1, -1, -1, -1, -1};
        }
        for (int i = 0; i < gridStatus.Length; i++)
        {
            gridStatus[i] = -1;
        }

        currentPlayer = players[0];
        
        if (turnPicks == null)
        {
            turnPicks = new Stack<int>();
        }
        
        if (turnPicks.Count > 0)
        {
            turnPicks.Clear();
        }
        gameData.GameState = GameState.MidGame;
        turnNumber = 0;
        
        hudView.ReStartClock(turnTime);
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void MarkCell(int cellId)
    {
        gridStatus[cellId] = currentPlayer.PlayerId;
        turnPicks.Push(cellId);
        if (CheckEndGame())
        {
            if (gameData.GameState == GameState.Win)
            {
                
                Debug.Log(currentPlayer.PlayerName + " WINS!");
                
            }
            else
            {
                Debug.Log(gameData.GameState);
            }
        }
        else
        {
            turnNumber++;
            SetNextPlayer();
            hudView.ReStartClock(turnTime);
        }
        
    }

    private bool CheckEndGame()
    {
        if (gridStatus[0] == currentPlayer.PlayerId && 
            gridStatus[1] == currentPlayer.PlayerId && 
            gridStatus[2] == currentPlayer.PlayerId)
        {
            // Top Row
            SetEndGame(GameState.Win);
            return true;
        }

        if (gridStatus[3] == currentPlayer.PlayerId &&
            gridStatus[4] == currentPlayer.PlayerId &&
            gridStatus[5] == currentPlayer.PlayerId)
        {
            // Middle Row
            SetEndGame(GameState.Win);
            return true;
        }

        if (gridStatus[6] == currentPlayer.PlayerId &&
            gridStatus[7] == currentPlayer.PlayerId &&
            gridStatus[8] == currentPlayer.PlayerId)
        {
            // Bottom Row
            SetEndGame(GameState.Win);
            return true;
        }
        
        if (gridStatus[0] == currentPlayer.PlayerId &&
            gridStatus[3] == currentPlayer.PlayerId &&
            gridStatus[6] == currentPlayer.PlayerId)
        {
            // Left Column
            SetEndGame(GameState.Win);
            return true;
        }
        
        if (gridStatus[1] == currentPlayer.PlayerId &&
            gridStatus[4] == currentPlayer.PlayerId &&
            gridStatus[7] == currentPlayer.PlayerId)
        {
            // Moddle Column
            SetEndGame(GameState.Win);
            return true;
        }

        if (gridStatus[2] == currentPlayer.PlayerId &&
            gridStatus[5] == currentPlayer.PlayerId &&
            gridStatus[8] == currentPlayer.PlayerId)
        {
            // Right Column
            SetEndGame(GameState.Win);
            return true;
        }
        
        if (gridStatus[0] == currentPlayer.PlayerId &&
            gridStatus[4] == currentPlayer.PlayerId &&
            gridStatus[8] == currentPlayer.PlayerId)
        {
            // Top Left Diagonal
            SetEndGame(GameState.Win);
            return true;
        }
        
        if (gridStatus[2] == currentPlayer.PlayerId &&
            gridStatus[4] == currentPlayer.PlayerId &&
            gridStatus[6] == currentPlayer.PlayerId)
        {
            // Bottom Left Diagonal
            SetEndGame(GameState.Win);
            return true;
        }

        if (turnNumber == 8)
        {
            SetEndGame(GameState.Draw);
            return true;
        }
        
        return false;
    }

    private void SetEndGame(GameState newState)
    {
        gameData.GameState = newState;
        gameBoardController.EndGame();
        hudView.EndGame();
    }

    private void SetNextPlayer()
    {
        var nextPlayer = (currentPlayer.PlayerId + 1) % players.Length; 
        currentPlayer = players[nextPlayer];
        if (gameData.GameState == GameState.MidGame && currentPlayer.playerController == PlayerController.AI)
        {
            RunAITurn();
        }
    }

    private void RunAITurn()
    {
        int randomCell = FindEmptyCell();
        aIPlayerRoutine = StartCoroutine(AIMarkCell(randomCell));
    }

    private IEnumerator AIMarkCell(int cellId)
    {
        gameBoardController.SetInputAvailable(false);
        hudView.SetInteractable(false);
        yield return new WaitForSeconds(2.5f);
        gameBoardController.OnCellClicked(cellId);
        hudView.SetInteractable(true);
        gameBoardController.SetInputAvailable(true);
    }

    public void TimerEnded()
    {
        SetEndGame(GameState.Win);
        SetNextPlayer();
        Debug.Log(currentPlayer.PlayerName + " WINS!");
        
    }

    public void HintButtonClicked()
    {
        var cellId = FindEmptyCell();
        gameBoardController.ShowHint(cellId);
    }

    public void Undo()
    {
        if (turnPicks.Count > 0)
        {
            var cellId = turnPicks.Pop();
            gameBoardController.Undo(cellId);
            hudView.ReStartClock(turnTime);
            turnNumber--;
        }

        if (turnPicks.Count > 0)
        {
            var cellid = turnPicks.Pop();
            gameBoardController.Undo(cellid);
            turnNumber--;
        }
    }

    private int FindEmptyCell()
    {
        List<int> emptyCells = new List<int>();
        for (int i = 0; i < gridStatus.Length; i++)
        {
            if (gridStatus[i] == -1)
            {
                emptyCells.Add(i);
            }
        }

        int randomCell = Random.Range(0, emptyCells.Count);
        return emptyCells[randomCell];
    }


    public void Inject(IHudView injection)
    {
        hudView = injection;
    }

    public void Inject(IGameBoardController injection)
    {
        gameBoardController = injection;
    }
}
