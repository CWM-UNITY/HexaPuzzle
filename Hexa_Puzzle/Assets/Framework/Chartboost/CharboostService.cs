using ChartboostSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharboostService : MonoBehaviour
{
    private bool ageGate = false;
    private bool autocache = true;

    private bool hasInterstitial = false;
    private bool hasRewardedVideo = false;
    private int frameCount = 0;

    private bool showInterstitial = true;
    private bool showRewardedVideo = true;

    public static System.Action onCompleteRewardedVideo = null;
    public static System.Action<bool> onCacheRewardedVideo = null;

    private void Start()
    {
#if UNITY_IPHONE
		Chartboost.setShouldPauseClickForConfirmation(ageGate);
#endif
        Chartboost.setAutoCacheAds(autocache);
    }

    private void OnEnable()
    {
        SetupDelegates();
    }

    private void OnDisable()
    {
        RemoveDelegates();
    }

    //private void Update()
    //{
    //    frameCount++;
    //    if (frameCount > 30)
    //    {
    //        // update these periodically and not every frame
    //        if(hasInterstitial == false)
    //            hasInterstitial = Chartboost.hasInterstitial(CBLocation.Default);
    //        if(hasRewardedVideo == false)
    //            hasRewardedVideo = Chartboost.hasRewardedVideo(CBLocation.Default);

    //        frameCount = 0;
    //    }
    //}

    public bool HasInterstitial()
    {
        return Chartboost.hasInterstitial(CBLocation.Default);
    }

    public bool HasRewardedVideo()
    {
        return Chartboost.hasRewardedVideo(CBLocation.Default);
    }

    public void LoadInterstitial()
    {
        Chartboost.cacheInterstitial(CBLocation.Default);
    }

    public void ShowInterstitial()
    {
        Chartboost.showInterstitial(CBLocation.Default);
    }

    public void LoadRewardedVideo()
    {
        Chartboost.cacheRewardedVideo(CBLocation.Default);
    }

    public void ShowRewardedVideo()
    {
        Chartboost.showRewardedVideo(CBLocation.Default);
    }

    public void SetupDelegates()
    {
        // Listen to all impression-related events
        Chartboost.didInitialize += didInitialize;
        Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
        Chartboost.didDismissInterstitial += didDismissInterstitial;
        Chartboost.didCloseInterstitial += didCloseInterstitial;
        Chartboost.didClickInterstitial += didClickInterstitial;
        Chartboost.didCacheInterstitial += didCacheInterstitial;
        Chartboost.shouldDisplayInterstitial += shouldDisplayInterstitial;
        Chartboost.didDisplayInterstitial += didDisplayInterstitial;
        Chartboost.didFailToRecordClick += didFailToRecordClick;
        Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
        Chartboost.didDismissRewardedVideo += didDismissRewardedVideo;
        Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
        Chartboost.didClickRewardedVideo += didClickRewardedVideo;
        Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
        Chartboost.shouldDisplayRewardedVideo += shouldDisplayRewardedVideo;
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;
        Chartboost.didPauseClickForConfirmation += didPauseClickForConfirmation;
        Chartboost.willDisplayVideo += willDisplayVideo;
//#if UNITY_IPHONE
//		Chartboost.didCompleteAppStoreSheetFlow += didCompleteAppStoreSheetFlow;
//#endif
    }

    public void RemoveDelegates()
    {
        // Remove event handlers
        Chartboost.didInitialize -= didInitialize;
        Chartboost.didFailToLoadInterstitial -= didFailToLoadInterstitial;
        Chartboost.didDismissInterstitial -= didDismissInterstitial;
        Chartboost.didCloseInterstitial -= didCloseInterstitial;
        Chartboost.didClickInterstitial -= didClickInterstitial;
        Chartboost.didCacheInterstitial -= didCacheInterstitial;
        Chartboost.shouldDisplayInterstitial -= shouldDisplayInterstitial;
        Chartboost.didDisplayInterstitial -= didDisplayInterstitial;
        Chartboost.didFailToRecordClick -= didFailToRecordClick;
        Chartboost.didFailToLoadRewardedVideo -= didFailToLoadRewardedVideo;
        Chartboost.didDismissRewardedVideo -= didDismissRewardedVideo;
        Chartboost.didCloseRewardedVideo -= didCloseRewardedVideo;
        Chartboost.didClickRewardedVideo -= didClickRewardedVideo;
        Chartboost.didCacheRewardedVideo -= didCacheRewardedVideo;
        Chartboost.shouldDisplayRewardedVideo -= shouldDisplayRewardedVideo;
        Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
        Chartboost.didDisplayRewardedVideo -= didDisplayRewardedVideo;
        Chartboost.didPauseClickForConfirmation -= didPauseClickForConfirmation;
        Chartboost.willDisplayVideo -= willDisplayVideo;
//#if UNITY_IPHONE
//		Chartboost.didCompleteAppStoreSheetFlow -= didCompleteAppStoreSheetFlow;
//#endif
    }


    void didInitialize(bool status)
    {
        Debug.Log(string.Format("didInitialize: {0}", status));
    }

    void didFailToLoadInterstitial(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
    }

    void didDismissInterstitial(CBLocation location)
    {
        Debug.Log("didDismissInterstitial: " + location);
    }

    void didCloseInterstitial(CBLocation location)
    {
        Debug.Log("didCloseInterstitial: " + location);
    }

    void didClickInterstitial(CBLocation location)
    {
        Debug.Log("didClickInterstitial: " + location);
    }

    void didCacheInterstitial(CBLocation location)
    {
        Debug.Log("didCacheInterstitial: " + location);
    }

    bool shouldDisplayInterstitial(CBLocation location)
    {
        // return true if you want to allow the interstitial to be displayed
        Debug.Log("shouldDisplayInterstitial @" + location + " : " + showInterstitial);
        return showInterstitial;
    }

    void didDisplayInterstitial(CBLocation location)
    {
        Debug.Log("didDisplayInterstitial: " + location);
    }

    void didFailToRecordClick(CBLocation location, CBClickError error)
    {
        Debug.Log(string.Format("didFailToRecordClick: {0} at location: {1}", error, location));
    }

    void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
        hasInterstitial = false;
        if (onCacheRewardedVideo != null)
            onCacheRewardedVideo.Invoke(false);
    }

    void didDismissRewardedVideo(CBLocation location)
    {
        Debug.Log("didDismissRewardedVideo: " + location);
    }

    void didCloseRewardedVideo(CBLocation location)
    {
        Debug.Log("didCloseRewardedVideo: " + location);
    }

    void didClickRewardedVideo(CBLocation location)
    {
        Debug.Log("didClickRewardedVideo: " + location);
    }

    void didCacheRewardedVideo(CBLocation location)
    {
        Debug.Log("didCacheRewardedVideo: " + location);
        if (onCacheRewardedVideo != null)
            onCacheRewardedVideo.Invoke(true);
    }

    bool shouldDisplayRewardedVideo(CBLocation location)
    {
        Debug.Log("shouldDisplayRewardedVideo @" + location + " : " + showRewardedVideo);
        return showRewardedVideo;
    }

    void didCompleteRewardedVideo(CBLocation location, int reward)
    {
        Debug.Log(string.Format("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
        hasRewardedVideo = false;
        if (onCompleteRewardedVideo != null)
            onCompleteRewardedVideo.Invoke();
    }

    void didDisplayRewardedVideo(CBLocation location)
    {
        Debug.Log("didDisplayRewardedVideo: " + location);
    }

    void didPauseClickForConfirmation()
    {
#if UNITY_IPHONE
		Debug.Log("didPauseClickForConfirmation called");
		//activeAgeGate = true;
#endif
    }

    void willDisplayVideo(CBLocation location)
    {
        Debug.Log("willDisplayVideo: " + location);
    }

}
