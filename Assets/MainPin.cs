using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainPin : MonoBehaviour
{
    [Inject] private DataSaver _dataSaver;
    [Inject] private UIController _uiController;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _editButton;

    private PopupData _popupData;

    private void Start()
    {
        _closeButton.onClick.AddListener(Hide);
        _editButton.onClick.AddListener(EditPopup);
    }

    public void FillInfo(PopupData popupData)
    {
        _title.text = popupData.Title;
        _description.text = popupData.Description;
        _popupData = popupData;
    }

    private void EditPopup()
    {
        _uiController.PopupController.InputPopupShow(true, _popupData);
        this.gameObject.SetActive(false);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
