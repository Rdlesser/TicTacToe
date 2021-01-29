using Enums;
using Interfaces;

public interface IMainMenuView : IInjectionContainer<IMainMenuController>
{

    void OnModeButtonClicked();
    void OnStartButtonClicked();
    void OnIntroButtonClicked();
    void OnQuitButtonClicked();
    void SwitchMode(GameMode newMode);
}

