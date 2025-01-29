using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PinClickHandler : ClickHandler
{
    protected bool _isDragging = false;

    private GameObject _previewPopup;

    public void Start() 
    {
        EventsProvider.OnPointerDownUpEvent += ChangeIsDraggingState;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (!_isDragging)
        {
            string pinId = gameObject.GetComponent<InfoPin>().Id;

            PopupData popupData = _dataSaver.LoadDataById(pinId, "popupData");

            if (popupData != null)
            {
                _previewPopup = _UIController.PopupController.PreviewPopupShow(popupData);
                SetWindowPosition(_previewPopup);
            }
        }   
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (!_isDragging)
        {
            if (_previewPopup != null && !IsPointerOverPreviewPopup())
            {
                _previewPopup.SetActive(false);
                _previewPopup = null;
            }
        }
    }

    private void SetWindowPosition(GameObject prefab)
    {
        if (prefab == null)
        {
            return;
        }

        RectTransform windowRect = prefab.GetComponent<RectTransform>();
        RectTransform parentRect = GetComponent<RectTransform>();

        if (windowRect == null || parentRect == null)
        {
            return;
        }

        windowRect.position = parentRect.position;

        float offsetX = CalculateOffset(parentRect, windowRect);
        windowRect.anchoredPosition = new Vector2(offsetX, windowRect.anchoredPosition.y);

        prefab.SetActive(true);
    }

    private float CalculateOffset(RectTransform parentRect, RectTransform windowRect)
    {
        float parentHalfWidth = parentRect.rect.width / 2;
        float windowHalfWidth = windowRect.rect.width / 2;

        if (parentRect.anchoredPosition.x > 0)
        {
            return parentRect.anchoredPosition.x - parentHalfWidth - windowHalfWidth;
        }
        else
        {
            return parentRect.anchoredPosition.x + parentHalfWidth + windowHalfWidth;
        }
    }

    public void ChangeIsDraggingState(bool isDragging)
    {
        _isDragging = isDragging;

        _previewPopup.SetActive(!_isDragging);

        if (!_isDragging)
        {
            SetWindowPosition(_previewPopup); 
        }
    }

    private bool IsPointerOverPreviewPopup()
    {
        if (_previewPopup == null)
        {
            return false;
        }

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == _previewPopup)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDestroy()
    {
        EventsProvider.OnPointerDownUpEvent -= ChangeIsDraggingState;
    }
}
