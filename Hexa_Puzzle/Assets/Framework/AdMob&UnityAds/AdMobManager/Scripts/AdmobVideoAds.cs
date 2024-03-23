using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

namespace AdMob.AdMobManager
{
    public class AdMobVideoAds
    {
        public RewardBasedVideoAd rewardBasedVideo = null;
        private string adUnitId = string.Empty;

        public static Action onVideoLoaded = null;
        public static Action<AdFailedToLoadEventArgs> onVideoFailedToLoad = null;
        public static Action onVideoOpened = null;
        public static Action onVideoStarted = null;
        public static Action onVideoClosed = null;
        public static Action<Reward> onVideoRewarded = null;
        public static Action onVideoLeftApplication = null;

        private AdmobAdRequest admobAdRequest;

        public AdMobVideoAds(string videoRewordID)
        {
            admobAdRequest = new AdmobAdRequest();

            this.adUnitId = videoRewordID;
            // Get singleton reward based video ad reference.
            this.rewardBasedVideo = RewardBasedVideoAd.Instance;

            // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
            this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
            this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
            this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
            this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
            this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
            this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
            this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;

            this.RequestRewardVideoAds();
        }

        public bool IsLoaded()
        {
            return this.rewardBasedVideo.IsLoaded();
        }

        public void RequestRewardVideoAds()
        {
            this.rewardBasedVideo.LoadAd(admobAdRequest.CreateAdRequest(), adUnitId);
        }


        public void ShowRewardVideoAds()
        {
            if (this.rewardBasedVideo.IsLoaded())
            {
                this.rewardBasedVideo.Show();
            }
            else
            {
                this.RequestRewardVideoAds();
            }
        }


        #region RewardBasedVideo callback handlers

        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
            //MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
            if (onVideoLoaded != null)
                onVideoLoaded.Invoke();
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            //MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
            if (onVideoFailedToLoad != null)
                onVideoFailedToLoad.Invoke(args);
        }

        public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
        {
            //MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
            if (onVideoOpened != null)
                onVideoOpened.Invoke();
        }

        public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
        {
            //MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
            if (onVideoStarted != null)
                onVideoStarted.Invoke();
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
            //MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
            if (onVideoClosed != null)
                onVideoClosed.Invoke();
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            //string type = args.Type;
            //double amount = args.Amount;
            //MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
            if (onVideoRewarded != null)
                onVideoRewarded.Invoke(args);
        }

        public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
        {
            //MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
            if (onVideoLeftApplication != null)
                onVideoLeftApplication.Invoke();
        }

        #endregion
    }
}
