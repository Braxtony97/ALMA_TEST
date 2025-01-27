using UnityEngine;
using UnityEngine.EventSystems;

public class PinClickHandler : ClickHandler
{
    protected bool _isDragging = false;

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
            SetWindowPosition();
        }   
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (!_isDragging)
        {
            _mainWindow.SetActive(false);
        }
    }

    private void SetWindowPosition()
    {
        if (_mainWindow == null)
        {
            return;
        }

        RectTransform windowRect = _mainWindow.GetComponent<RectTransform>();
        RectTransform parentRect = GetComponent<RectTransform>();

        if (windowRect == null || parentRect == null)
        {
            return;
        }

        windowRect.position = parentRect.position;

        float offsetX = CalculateOffset(parentRect, windowRect);
        windowRect.anchoredPosition = new Vector2(offsetX, windowRect.anchoredPosition.y);

        _mainWindow.SetActive(true);
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

        _mainWindow.SetActive(!_isDragging);

        if (!_isDragging)
        {
            SetWindowPosition(); 
        }
    }

    private void OnDestroy()
    {
        EventsProvider.OnPointerDownUpEvent -= ChangeIsDraggingState;
    }
}
