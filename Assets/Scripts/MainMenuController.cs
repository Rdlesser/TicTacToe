using System;
using System.IO;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, IMainMenuController
{

    [SerializeField] private MainMenuView mainMenuView;
    [SerializeField] private IntroPanelController introPanelController;
    [SerializeField] private GameData gameData;

    private GameMode gameMode = GameMode.HvsH;

    private void Start()
    {
        gameData.GameState = GameState.MainMenu;
        gameData.GameMode = gameMode;
        mainMenuView.Inject(this);
    }

    public void OnModeButtonClicked()
    {
        if (gameMode == GameMode.HvsH)
        {
            gameMode = GameMode.HvsC;
        }
        else if (gameMode == GameMode.HvsC)
        {
            gameMode = GameMode.CvsC;
        }
        else if (gameMode == GameMode.CvsC)
        {
            gameMode = GameMode.HvsH;
        }
        mainMenuView.SwitchMode(gameMode);
    }

    public void OnStartButtonClicked()
    {
        gameData.GameMode = gameMode;
        StartGame();
    }


    public void OnIntroButtonClicked()
    {
        introPanelController.ShowIntroPanel();
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnReskinButtonClicked(Text inputPath)
    {
        
        var myLoadedAssetBundle 
            = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, inputPath.text));
        if (myLoadedAssetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
