using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PopupController : MonoBehaviour
{
    public GameObject PopupQuestionPrefab => _popupQuestionPrefab;
    public Vector2 MouseClickPosition;

    [Inject] private DiContainer _container;
    [Inject] private DataSaver _dataSaver;
    
    [SerializeField] private GameObject _pin;
    [SerializeField] private GameObject _popupQuestionPrefab;
    [SerializeField] private GameObject _popupInputPrefab;
    [SerializeField] private GameObject _popupPreviewPrefab;
    [SerializeField] private GameObject _popupMainPrefab;
    [SerializeField] private Transform _popupContainer;

    private GameObject _currentQuestionPopup;
    private GameObject _currentInputPopup;
    private GameObject _currentPreviewPopup;
    private GameObject _currentMainPopup;
    private RectTransform _inputPopupRectTransform;
    private RectTransform _questionPopupRectTransform;
    private RectTransform _previewPopupRectTransform;
    private RectTransform _mainPopupRectTransform;
    private PopupInputController _popupInputController;

    public void LoadPins()
    {
        List<PopupData> popupDataList = _dataSaver.LoadAllData("popupData");

        foreach (var data in popupDataList)
        {
            GameObject newPin = _container.InstantiatePrefab(_pin, _popupContainer);
            RectTransform pinRectTransform = newPin.GetComponent<RectTransform>();
            pinRectTransform.anchoredPosition = data.Position;
            newPin.GetComponent<InfoPin>().Id = data.Id;
            pinRectTransform.SetSiblingIndex(0);
        }
    }

    public void CreateNewInfoPin(PopupData data)
    {
        GameObject newPin = _container.InstantiatePrefab(_pin, _popupContainer);
        RectTransform pinRectTransform = newPin.GetComponent<RectTransform>();
        pinRectTransform.anchoredPosition = MouseClickPosition;
        InfoPin pinComponent = newPin.GetComponent<InfoPin>();

        if (pinComponent != null)
        {
            pinComponent.Id = data.Id;
        }

        pinRectTransform.SetSiblingIndex(0);
    }

    public GameObject QuestionPopupShow(Vector2 clickPosition)
    {
        if (_currentQuestionPopup == null)
        {
            _currentQuestionPopup = _container.InstantiatePrefab(_popupQuestionPrefab, _popupContainer);
            _questionPopupRectTransform = _currentQuestionPopup.GetComponent<RectTransform>();
        }

        _questionPopupRectTransform.anchoredPosition = clickPosition;
        _questionPopupRectTransform.transform.SetAsLastSibling();
        _currentQuestionPopup.SetActive(true);

        return _currentQuestionPopup;
    }
     
    public GameObject InputPopupShow(bool isFromEditPopup)
    {
        if (_currentInputPopup == null)
        {
            _currentInputPopup = _container.InstantiatePrefab(_popupInputPrefab, _popupContainer);  
        }

        _inputPopupRectTransform = _currentInputPopup.GetComponent<RectTransform>();
        _popupInputController = _currentInputPopup.GetComponent<PopupInputController>();

        if (!isFromEditPopup)
        {
            _popupInputController.ChangeID();
        }
        _inputPopupRectTransform.anchoredPosition = Vector2.zero;
        _inputPopupRectTransform.transform.SetAsLastSibling();
        _currentInputPopup.SetActive(true);

        return _currentInputPopup;
    }

    public GameObject PreviewPopupShow(PopupData popupData)
    {
        if (_currentPreviewPopup == null)
        {
            _currentPreviewPopup = _container.InstantiatePrefab(_popupPreviewPrefab, _popupContainer);
            _previewPopupRectTransform = _currentPreviewPopup.GetComponent<RectTransform>();
        }

        _currentPreviewPopup.GetComponent<PopupPreview>().FillInfo(popupData);
        _previewPopupRectTransform.transform.SetAsLastSibling();
        _currentPreviewPopup.SetActive(true);

        return _currentPreviewPopup;
    }

    public GameObject MainPopupShow(PopupData popupData)
    {
        if (_currentMainPopup == null)
        {
            _currentMainPopup = _container.InstantiatePrefab(_popupMainPrefab, _popupContainer);
            _mainPopupRectTransform = _currentMainPopup.GetComponent<RectTransform>();
        }

        _currentMainPopup.GetComponent<MainPin>().FillInfo(popupData);
        _mainPopupRectTransform.anchoredPosition = Vector2.zero;
        _mainPopupRectTransform.transform.SetAsLastSibling();
        _currentMainPopup.SetActive(true);

        return _currentMainPopup;
    }

    public void DeleteAllPins()
    {
        GameObject[] pins = GameObject.FindGameObjectsWithTag("PinIcon");
        foreach (GameObject pin in pins)
        {
            Destroy(pin);
        }
    }

    private void OnDisable()
    {
        
    }

    public void ClosePopup(GameObject popup)
    {
        Destroy(popup);
    }
}
