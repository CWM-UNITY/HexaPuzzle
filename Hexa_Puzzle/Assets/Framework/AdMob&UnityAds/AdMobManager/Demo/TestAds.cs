using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AdMob.AdMobManager;
using GoogleMobileAds.Api;

public class TestAds : MonoBehaviour
{
    AdMobManager adsManager = null;
    public RectTransform buttonVideoAds = null;
    public UnityEngine.UI.Text text = null;

    private void Start ()
    {
        AdMobConfiguration adc = new AdMobConfiguration();
        //========= Android key ===========================//
        adc.Android_AppID = "";
        adc.Android_BannerID = "ca-app-pub-3426258309246005/5267356861";
        adc.Android_InterstitialID = "ca-app-pub-3426258309246005/5824408995";
        adc.Android_VideoRewordID = "ca-app-pub-3940256099942544/5224354917";

        //========= IOS key ===========================//


        adsManager = new AdMobManager(adc);
        adsManager.AdMobRequest();

        AdMobVideoAds.onVideoLoaded += HandleRewardBasedVideoLoaded;
        AdMobVideoAds.onVideoOpened += HandleRewardBasedVideoOpened;
        AdMobVideoAds.onVideoRewarded += HandleRewardBasedVideoRewarded;
        AdMobVideoAds.onVideoStarted += HandleRewardBasedVideoStarted;
        AdMobVideoAds.onVideoFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        AdMobVideoAds.onVideoLeftApplication += HandleRewardBasedVideoLeftApplication;
        AdMobVideoAds.onVideoClosed += HandleRewardBasedVideoClosed;

        AdMobBanner.onAdLoaded += HandleOnAdLoaded;
        AdMobBanner.onAdFailedToLoad += HandleOnAdFailedToLoad;
        AdMobBanner.onAdOpening += HandleOnAdOpened;
        AdMobBanner.onAdClosed += HandleOnAdClosed;
        AdMobBanner.onAdLeavingApplication += HandleOnAdLeavingApplication;

        AdMobInterstitail.onAdClosed += HandleInterstitialClosed;
        AdMobInterstitail.onAdFailedToLoad += HandleInterstitialFailedToLoad;
        AdMobInterstitail.onAdLoaded += HandleInterstitialLoaded;
        AdMobInterstitail.onAdOpening += HandleInterstitialOpened;
        AdMobInterstitail.onAdLeavingApplication += HandleInterstitialLeftApplication;
    }

    #region Listener Banner 

    private void HandleOnAdLoaded()
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    private void HandleOnAdFailedToLoad(AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    private void HandleOnAdOpened()
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    private void HandleOnAdClosed()
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    private void HandleOnAdLeavingApplication()
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion


    #region RewardBasedVideo callback handlers

    private void HandleRewardBasedVideoLoaded()
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    private void HandleRewardBasedVideoFailedToLoad(AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    private void HandleRewardBasedVideoOpened()
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    private void HandleRewardBasedVideoStarted()
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    private void HandleRewardBasedVideoClosed()
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    private void HandleRewardBasedVideoRewarded(Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    private void HandleRewardBasedVideoLeftApplication()
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    private void HandleInterstitialLoaded()
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    private void HandleInterstitialFailedToLoad( AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                           + args.Message);
    }

    private void HandleInterstitialOpened()
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    private void HandleInterstitialClosed()
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    private void HandleInterstitialLeftApplication()
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion

    public void ShowBanner()
    {
        adsManager.adMobBanner.ShowBanner();
    }


    public void HideBanner()
    {
        adsManager.adMobBanner.HideBanner();
    }

    public void ShowInterstitial()
    {
        if (adsManager.adMobInterstitail.IsLoaded())
        {
            adsManager.adMobInterstitail.ShowInterstitial();
        }
        else
        {
            adsManager.adMobInterstitail.RequestInterstitial();
        }
    }

    public void RequestVideo()
    {
        adsManager.adMobVideoAds.RequestRewardVideoAds();
    }

    public void VideoAds()
    {
        if (adsManager.adMobVideoAds.IsLoaded())
        {
            adsManager.adMobVideoAds.ShowRewardVideoAds();
        }
    }

}
