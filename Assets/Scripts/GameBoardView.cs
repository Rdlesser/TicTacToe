using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;


public class GameBoardView : MonoBehaviour, IGameBoardView
{
    [SerializeField] private Button[] gridCells;
    [SerializeField] private GraphicRaycaster graphicRaycaster;

    private Action<int> onCellClicked;

    private Coroutine hintRoutine;

    private int lastHint = -1;

    private void Start()
    {
        ResetGame();
    }

    public void OnButtonClicked(int cellId)
    {
        onCellClicked?.Invoke(cellId);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < gridCells.Length; i++)
        {
            gridCells[i].onClick.RemoveAllListeners();
        }
    }

    public void Inject(IGameBoardController controller)
    {
        onCellClicked += controller.OnCellClicked;
    }

    public void MarkCell(int cellId, Player player)
    {
        if (hintRoutine != null)
        {
            StopCoroutine(hintRoutine);
        }
        gridCells[cellId].image.sprite = player.SeedImage;
        var tempColor = gridCells[cellId].image.color;
        tempColor.a = 1f;
        gridCells[cellId].image.color = tempColor;
        gridCells[cellId].interactable = false;
    }

    public void SetInputAvailable(bool isAvailable)
    {
        graphicRaycaster.enabled = isAvailable;
    }


    public void ResetGame()
    {
        for (int i = 0; i < gridCells.Length; i++)
        {
            UnMarkCell(i);
            gridCells[i].interactable = true;
        }

        graphicRaycaster.enabled = true;

    }

    public void UnMarkCell(int cellId)
    {
        var tempColor = gridCells[cellId].image.color;
        tempColor.a = 0f;
        gridCells[cellId].image.color = tempColor;
        gridCells[cellId].image.sprite = null;
        gridCells[cellId].interactable = true;
    }

    public void EndGame()
    {
        foreach (var gridCell in gridCells)
        {
            gridCell.interactable = false;
        }
    }

    public void ShowHint(int cellId)
    {
        if (hintRoutine != null)
        {
            StopCoroutine(hintRoutine);
            Color lastHintColor = gridCells[lastHint].image.color;
            lastHintColor.a = 0;
            gridCells[lastHint].image.color = lastHintColor;
        }
        hintRoutine = StartCoroutine(ShowHintFlash(cellId));
    }

    private IEnumerator ShowHintFlash(int cellId)
    {
        float targetAlpha = 0.0f;
        Color curColor = gridCells[cellId].image.color;
        curColor.a = 1f;
        gridCells[cellId].image.color = curColor;
        lastHint = cellId;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, 2f * Time.deltaTime);
            gridCells[cellId].image.color = curColor;
            yield return null;
        }

        curColor.a = 0f;
        gridCells[cellId].image.color = curColor;
    }
}


