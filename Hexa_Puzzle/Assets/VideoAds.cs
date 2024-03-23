using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoAds : MonoBehaviour
{
    public RectTransform wheel = null;
    private ServiceManager sm = null;

    private void Start()
    {
        sm = ServiceManager.Instance;
    }

    private void OnEnable()
    {
        //CharboostService.onCompleteRewardedVideo += OnFinished;
        UnityAdsManager.OnFinished += OnFinished;
    }

    private void OnDisable()
    {
        //CharboostService.onCompleteRewardedVideo -= OnFinished;
        UnityAdsManager.OnFinished -= OnFinished;
    }

    private void OnFinished()
    {
        DialogController.instance.ShowDialog(DialogType.DailyGift);
    }

    public void ShowVideoAds()
    {
        if(sm.charboostService.HasRewardedVideo() == true)
        {
            CharboostService.onCompleteRewardedVideo = OnFinished;
            sm.charboostService.ShowRewardedVideo();
        }
        else
        {
            sm.charboostService.LoadRewardedVideo();
            if(sm.unityAdsManager.IsReady() == true)
            {
                sm.unityAdsManager.ShowAd();
            }
        }
    }

    private void Update()
    {
        wheel.transform.Rotate(new Vector3(0, 0, 1));
    }
}
