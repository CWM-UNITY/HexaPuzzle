using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogOverlay : MonoBehaviour
{
    private Image overlay;

    private void Awake()
    {
        overlay = GetComponent<Image>();
    }

    private void Start()
    {
        DialogController.instance.onDialogsOpened += OnDialogOpened;
        DialogController.instance.onDialogsClosed += OnDialogClosed;
    }

    private void OnLevelWasLoaded(int level)
    {
        overlay.enabled = false;
    }

    private void OnDialogOpened()
    {
        overlay.enabled = true;
    }

    private void OnDialogClosed()
    {
        overlay.enabled = false;
    }

    private void OnDestroy()
    {
        DialogController.instance.onDialogsOpened -= OnDialogOpened;
        DialogController.instance.onDialogsClosed -= OnDialogClosed;
    }
}
