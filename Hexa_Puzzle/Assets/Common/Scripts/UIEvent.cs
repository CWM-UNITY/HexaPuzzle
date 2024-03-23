using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UIEvent : MonoBehaviour, IPointerClickHandler {
    public Action<PointerEventData> onPointerClick;
    public Action onMouseDown, onMouseUp;
    public Action onMouseDrag;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onPointerClick != null) onPointerClick(eventData);
    }

    private void OnMouseDown()
    {
        if (onMouseDown != null) onMouseDown();
    }

    private void OnMouseUp()
    {
        if (onMouseUp != null) onMouseUp();
    }

    private void OnMouseDrag()
    {
        if (onMouseDrag != null) onMouseDrag();
    }
}
