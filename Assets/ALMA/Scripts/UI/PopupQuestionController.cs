using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupQuestionController : MonoBehaviour
{
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    public Action<bool> OnAnswerSelected;

    private void Start()
    {
        _yesButton.onClick.AddListener(() => OnAnswerSelected?.Invoke(true));
        _noButton.onClick.AddListener(() => OnAnswerSelected?.Invoke(false));
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
