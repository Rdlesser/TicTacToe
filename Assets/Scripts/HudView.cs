using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class HudView : MonoBehaviour, IHudView
{
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button undoButton;
    [SerializeField] private Button restartButton;

    private Action onTimerEnded;
    private Action onHintButtonClicked;
    private Action onUndoButtonClicked;
    private Action onRestartButtonClicked;
    
    private Coroutine timerRoutine;

    private GameMode gameMode;
    
    public void ReStartClock(int time)
    {
        StopTimer();
        timeSlider.value = 1;
        timerRoutine = StartCoroutine(ClockRoutine(time));
    }

    private void StopTimer()
    {
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }
    }
    
    /*
     * Clock Routine to be used for the timer
     */
    private IEnumerator ClockRoutine(float targetTime)
    {
        float timeLeft = targetTime;
        // Start count-down
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            // Set the timer text
            timeSlider.value -= 1 / targetTime;
        }

        onTimerEnded?.Invoke();
		
    }

    public void OnHintButtonClicked()
    {
        onHintButtonClicked?.Invoke();
    }
    
    public void OnUndoButtonClicked()
    {
        onUndoButtonClicked?.Invoke();
    }

    public void OnRestartButtonClicked()
    {
        onRestartButtonClicked?.Invoke();
    }

    public void Inject(IGameLogicController gameLogicController)
    {
        onTimerEnded += gameLogicController.TimerEnded;
        onHintButtonClicked += gameLogicController.HintButtonClicked;
        onUndoButtonClicked += gameLogicController.Undo;
        onRestartButtonClicked += gameLogicController.Restart;
        gameMode = gameLogicController.GetGameMode();

    }

    public void EndGame()
    {
        StopTimer();
        hintButton.interactable = false;
        undoButton.interactable = false;
    }

    public void ResetGame()
    {
        EndGame();
        if (gameMode == GameMode.HvsH || gameMode == GameMode.CvsC)
        {
            SetInteractable(false);
        }
        else
        {
            SetInteractable(true);
        }
    }

    public void SetInteractable(bool isInteractable)
    {
        undoButton.interactable = isInteractable;
        hintButton.interactable = isInteractable;
    }


}
