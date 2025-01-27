using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected GameObject _mainWindow;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("������ ��� �������: " + gameObject.name);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("���� ����� � ������: " + gameObject.name);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("���� ����� �� �������: " + gameObject.name);
    }
}
