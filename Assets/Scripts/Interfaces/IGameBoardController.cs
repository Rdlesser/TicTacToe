using UnityEngine;

namespace Interfaces
{
    public interface IGameBoardController : IInjectionContainer<IGameLogicController>
    {
        void OnCellClicked(int cellId);
        void ShowHint(int cellId);
        void Undo(int cellId);
        void Reset();
        void SetInputAvailable(bool isAvailable);

        void EndGame();
    }
}