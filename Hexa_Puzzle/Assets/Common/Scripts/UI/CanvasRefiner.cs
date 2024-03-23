using UnityEngine;
using System.Collections;

public class CanvasRefiner : MonoBehaviour
{
    public bool isFullScreen;
    private RectTransform tr;

    private void Start()
    {
        tr = GetComponent<RectTransform>();
        OnScreenSizeChanged();
        UICamera.instance.onScreenSizeChanged += OnScreenSizeChanged;
    }

    private void OnLevelWasLoaded(int level)
    {
        tr = GetComponent<RectTransform>();
        OnScreenSizeChanged();
        UICamera.instance.onScreenSizeChanged += OnScreenSizeChanged;
    }

    private void OnScreenSizeChanged()
    {
        if (isFullScreen)
        {
            tr.sizeDelta = new Vector2(UICamera.instance.virtualWidth, UICamera.instance.virtualHeight);
        }
        else
        {
            tr.sizeDelta = new Vector2(UICamera.instance.GetWidth() * 200, UICamera.instance.GetHeight() * 200);
        }
    }
}
