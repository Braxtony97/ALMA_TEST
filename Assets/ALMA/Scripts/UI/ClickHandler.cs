using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ClickHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected GameObject _mainWindow;
    [Inject] protected UIController _UIController;

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
