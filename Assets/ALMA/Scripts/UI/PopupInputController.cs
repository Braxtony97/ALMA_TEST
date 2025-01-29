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

    public string IdPopup;

    private void Start()
    {
        _closeButton.onClick.AddListener(Hide);
        _saveButton.onClick.AddListener(SaveData);
    }

    public void ChangeID(string popupId = null)
    {
        if (string.IsNullOrEmpty(popupId))
        {
            IdPopup = Guid.NewGuid().ToString();
        }
        else
        {
            IdPopup = popupId;
            LoadData(IdPopup);
        }
    }

    private void SaveData()
    {
        Debug.Log(IdPopup);

        PopupData data = new PopupData
        {
            Id = IdPopup,
            Title = _title.text,
            Description = _description.text,
            Position = _uiController.PopupController.MouseClickPosition
        };

        Debug.Log(data.Id);
        PopupData existingData = _dataSaver.LoadDataById(data.Id, "popupData");

        if (existingData != null)
        {
            _dataSaver.UpdateDataById(data, "popupData"); 
        }
        else
        {
            _dataSaver.SaveToJson(data, "popupData");
            _uiController.PopupController.CreateNewInfoPin(data);
        }

        EventsProvider.OnSavePopupData?.Invoke();

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

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(Hide);
        _saveButton.onClick.RemoveListener(SaveData);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
