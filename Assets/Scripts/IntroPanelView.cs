using System;
using UnityEngine;

public class IntroPanelView : MonoBehaviour, IIntroPanelView
{
    private IIntroPanelController controller;
    
    private event Action OnClickCloseButtonPressed;

    
    public void Inject(IIntroPanelController controller)
    {
        OnClickCloseButtonPressed += controller.OnCloseButtonClicked;
    }

    public void SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    public void UnSubscribe(IIntroPanelController controller)
    {
        OnClickCloseButtonPressed -= controller.OnCloseButtonClicked;
    }

    public void OnCloseButtonClicked()
    {
        OnClickCloseButtonPressed?.Invoke();
    }

}
