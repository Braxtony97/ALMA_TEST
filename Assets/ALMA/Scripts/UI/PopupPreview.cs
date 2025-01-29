using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PopupPreview : MonoBehaviour
{
    [Inject] private UIController _uiController;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openFillInfo;

    private PopupData _data;

    private void Start()
    {
        _closeButton.onClick.AddListener(Hide);
        _openFillInfo.onClick.AddListener(OpenFullInfo);
    }

    public void FillInfo(PopupData popupData)
    {
        _title.text = popupData.Title;
        _data = popupData;
    }

    private void OpenFullInfo()
    {
        _uiController.PopupController.MainPopupShow(_data);
        _data = null;
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
