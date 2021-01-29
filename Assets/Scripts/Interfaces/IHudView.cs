namespace Interfaces
{
    public interface IHudView : IInjectionContainer<IGameLogicController>
    {
        void EndGame();
        void ResetGame();
        void SetInteractable(bool isInteractable);

        void ReStartClock(int time);
    }
}