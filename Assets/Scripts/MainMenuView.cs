using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    [SerializeField] private TMPro.TMP_Text modeButtonText;
    [SerializeField] private InputField reskinInput;

    private Action modeButtonClicked;
    private Action startButtonClicked;
    private Action introButtonClicked;
    private Action<Text> reskinButtonClicked;
    private Action quitButtonClicked;
    

    public void Inject(IMainMenuController controller)
    {
        modeButtonClicked += controller.OnModeButtonClicked;
        startButtonClicked += controller.OnStartButtonClicked;
        introButtonClicked += controller.OnIntroButtonClicked;
        reskinButtonClicked += controller.OnReskinButtonClicked;
        quitButtonClicked += controller.OnQuitButtonClicked;
    }

    public void OnModeButtonClicked()
    {
        modeButtonClicked?.Invoke();
    }

    public void OnStartButtonClicked()
    {
        startButtonClicked?.Invoke();
    }

    public void OnIntroButtonClicked()
    {
        introButtonClicked?.Invoke();
    }

    public void OnQuitButtonClicked()
    {
        quitButtonClicked?.Invoke();
    }

    public void OnReskinButtonClicked()
    {
        reskinButtonClicked?.Invoke(reskinInput.textComponent);
    }

    public void SwitchMode(GameMode newMode)
    {
        modeButtonText.text = newMode.ToString();
    }
}
