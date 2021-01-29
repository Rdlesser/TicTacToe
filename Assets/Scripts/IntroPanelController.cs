using UnityEngine;

public class IntroPanelController : MonoBehaviour, IIntroPanelController
{
    [SerializeField] private GameObject introPanelViewPrefab;

    private IIntroPanelView introPanelView;

    public void ShowIntroPanel()
    {
        if (introPanelView == null)
        {
            introPanelView = Instantiate(introPanelViewPrefab,transform).GetComponent<IIntroPanelView>();
            introPanelView.Inject(this);
        }
        introPanelView.SetActive(true);
    }
    
    public void OnCloseButtonClicked()
    {
        introPanelView.SetActive(false);
    }

    private void OnDestroy()
    {
        if (introPanelView != null)
        {
            introPanelView.UnSubscribe(this);
        }
    }

}
