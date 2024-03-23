using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShowInsterstitailType
{
    NONE = 0,
    SHOWADS = 1
}


public class PopupBanner : MonoBehaviour
{
    public ShowInsterstitailType showInsterstitailType;

    private void OnEnable()
    {
        ServiceManager.Instance.adsManager.adMobBanner.ShowBanner();
        if(showInsterstitailType == ShowInsterstitailType.SHOWADS)
        {
            ServiceManager.Instance.adsManager.adMobInterstitail.ShowInterstitial();
        }
    }

    private void OnDisable()
    {
        ServiceManager.Instance.adsManager.adMobBanner.HideBanner();
    }
}
