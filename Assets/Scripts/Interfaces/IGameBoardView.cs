namespace Interfaces
{
    public interface IGameBoardView : IInjectionContainer<IGameBoardController>
    {
        void MarkCell(int cellId, Player player);
        void SetInputAvailable(bool isAvailable);
        void ShowHint(int cellId);
        void UnMarkCell(int cellId);
        void EndGame();
        void ResetGame();
    }
}