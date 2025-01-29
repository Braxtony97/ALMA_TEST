using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Enums;

public class UIController : MonoBehaviour
{
    public Transform Canvas => _canvas;
    public PopupController PopupController => _popupController;
    public ScreenUI CurrentScreen => _currentScreen;

    [SerializeField] private PopupController _popupController;
    [SerializeField] private ScreenUI[] _screensUI;
    [SerializeField] private Transform _canvas;

    private ScreenUI _currentScreen;

    [Inject] private DiContainer _container;

    public void OpenScreen (ScreenType screenType)
    {
        if (_currentScreen != null)
        {
            Destroy(_currentScreen.gameObject);
        }

        foreach (ScreenUI screenUI in _screensUI)
        {
            if (screenUI.ScreenType == screenType)
            {
                ScreenUI newScreen = _container.InstantiatePrefabForComponent<ScreenUI>(screenUI, _canvas);
                newScreen.transform.SetAsFirstSibling();

                _currentScreen = newScreen; 
                _currentScreen.Initialize();
            }
        }
    }

    public void OpenScreenwwww()
    {
        Debug.Log("s");
    }
}
