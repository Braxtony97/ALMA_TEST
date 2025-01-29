using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MapClickHandler : ClickHandler
{
    private PopupQuestionController _popupQuestionController;
    public override void OnPointerClick(PointerEventData eventData)
    {
        Vector2 clickPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _UIController.Canvas.GetComponent<RectTransform>(),
            eventData.position,
            null,
            out clickPosition
        );

        _UIController.PopupController.MouseClickPosition = clickPosition;
        CreateQuestionPopup(clickPosition);
    }

    private void CreateQuestionPopup(Vector2 clickPosition)
    {
        if (_popupQuestionController == null)
        {
            GameObject popup = _UIController.PopupController.QuestionPopupShow(clickPosition);
            _popupQuestionController = popup.GetComponent<PopupQuestionController>();
            _popupQuestionController.OnAnswerSelected += OnAnswerSelected;
        }
        else
        {
            if (_popupQuestionController.OnAnswerSelected == null)
            {
                _popupQuestionController.OnAnswerSelected += OnAnswerSelected;
            }

            _popupQuestionController.Show();
            _popupQuestionController.transform.localPosition = clickPosition;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
    }

    private void OnAnswerSelected(bool answer)
    {
        _popupQuestionController.Hide();
        _popupQuestionController.OnAnswerSelected -= OnAnswerSelected;

        if (answer)
        {
            _UIController.PopupController.InputPopupShow(false);
        }
    }
}
