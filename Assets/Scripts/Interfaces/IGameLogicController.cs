using Enums;

namespace Interfaces
{
    public interface IGameLogicController : IInjectionContainer<IHudView>, IInjectionContainer<IGameBoardController>
    {
        Player GetCurrentPlayer();
        void MarkCell(int cellId);
        void TimerEnded();
        void HintButtonClicked();
        void Undo();
        GameMode GetGameMode();
        void Restart();
    }
}