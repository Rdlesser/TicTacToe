using UnityEngine.UI;

namespace Interfaces
{
    public interface IMainMenuController
    {
        void OnModeButtonClicked();
        void OnStartButtonClicked();
        void OnIntroButtonClicked();
        void OnQuitButtonClicked();
        void OnReskinButtonClicked(Text inputPath);
    }
}