using UnityEngine;
using UnityEngine.EventSystems;

public class UIDebug : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("UI click detected");
    }
}
