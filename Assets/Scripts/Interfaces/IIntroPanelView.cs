using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIntroPanelView : IInjectionContainer<IIntroPanelController>
{

    void UnSubscribe(IIntroPanelController introPanelController);
    void SetActive(bool b);
}

