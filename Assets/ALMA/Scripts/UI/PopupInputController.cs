using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PopupInputController : MonoBehaviour
{
    [Inject] private DataSaver _dataSaver;
    [Inject] private UIController _uiController;

    [Header ("UI Elements")]
    [SerializeField] private TMP_InputField _title;
    [SerializeField] private TMP_InputField _description;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _saveButton;

    private string _popupId;

    private void Start()
    {
        _closeButton.onClick.AddListener(Hide);
        _saveButton.onClick.AddListener(SaveData);
    }

    public void ChangeID(string popupId = null)
    {
        if (string.IsNullOrEmpty(popupId))
        {
            _popupId = Guid.NewGuid().ToString();
        }
        else
        {
            _popupId = popupId;
            LoadData(_popupId);
        }

        gameObject.SetActive(true);
    }

    private void SaveData()
    {
        PopupData data = new PopupData
        {
            Id = _popupId,
            Title = _title.text,
            Description = _description.text,
            Position = _uiController.PopupController.MouseClickPosition
        };

        PopupData existingData = _dataSaver.LoadDataById(_popupId, "popupData");
        if (existingData != null)
        {
            _dataSaver.UpdateDataById(data, "popupData");
        }
        else
        {
            _dataSaver.SaveToJson(data, "popupData");
        }

        EventsProvider.OnSavePopupData?.Invoke();

        _uiController.PopupController.CreateNewInfoPin(data);
        Hide();
    }

    private void LoadData(string popupId)
    {
        PopupData data = _dataSaver.LoadDataById(popupId, "popupData");

        if (data != null)
        {
            _title.text = data.Title;
            _description.text = data.Description;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
