using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _requiredHoldTime = 0.1f;
    private bool _isDragging = false;
    private bool _isPointerDown = false;  
    private float _holdTime = 0f;
    private RectTransform _rectTransform; 
    private Canvas _canvas; 

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();

        if (_canvas == null)
        {
            Debug.LogError("Объект должен находиться внутри Canvas!");
        }
    }

    private void Update()
    {
        if (_isPointerDown)
        {
            _holdTime += Time.deltaTime;

            if (_holdTime >= _requiredHoldTime)
            {
                if (!_isDragging)
                {
                    _isDragging = true; 
                }
            }
        }

        if (_isDragging)
        {
            MoveObject(); 
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true; 
        _holdTime = 0f;

        EventsProvider.OnPointerDownUpEvent?.Invoke(_isPointerDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false; 
        _isDragging = false; 
        _holdTime = 0f;

        EventsProvider.OnPointerDownUpEvent?.Invoke(_isPointerDown);
    }

    private void MoveObject()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            Input.mousePosition,
            _canvas.worldCamera,
            out position);

        _rectTransform.anchoredPosition = position;
    }
}
