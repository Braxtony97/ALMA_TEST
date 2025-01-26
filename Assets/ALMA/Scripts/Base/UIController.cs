using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class UIController : MonoBehaviour
{
    public ScreenUI CurrentScreen => _currentScreen;

    [SerializeField] private ScreenUI[] _screensUI;
    [SerializeField] private Transform _canvas;

    private ScreenUI _currentScreen;

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
                ScreenUI newScreen = Instantiate(screenUI);
                newScreen.transform.SetParent(_canvas, false);

                _currentScreen = newScreen; 
                _currentScreen.Initialize();
            }
        }
    }
}
