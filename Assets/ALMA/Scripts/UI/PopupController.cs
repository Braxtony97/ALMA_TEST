using UnityEngine;
using Zenject;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject _popupPrefab; 

    private Transform _popupContainer;

    public void ShowPopup()
    {
        GameObject popup = Instantiate(_popupPrefab, _popupContainer);
        popup.SetActive(true);
    }

    public void ClosePopup(GameObject popup)
    {
        Destroy(popup);
    }
}
