
using GoogleMobileAds.Api;
using System;

namespace AdMob.AdMobManager
{
    public class AdMobBanner
    {

        private bool isFirstTime = false;
        private BannerView bannerView;


        public static Action onAdLoaded = null;
        public static Action<AdFailedToLoadEventArgs> onAdFailedToLoad = null;
        public static Action onAdOpening = null;
        public static Action onAdClosed = null;
        public static Action onAdLeavingApplication = null;

        private AdmobAdRequest admobAdRequest;

        #region Banner
        public AdMobBanner(string _bannerId, AdSize adSize, AdPosition positoin)
        {
            admobAdRequest = new AdmobAdRequest();

            if (this.bannerView != null)
            {
                this.bannerView.Destroy();
            }

            // Create a 320x50 banner at the top of the screen.
            this.bannerView = new BannerView(_bannerId, adSize, positoin);

            // Register for ad events.
            this.bannerView.OnAdLoaded += this.HandleAdLoaded;
            this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
            this.bannerView.OnAdOpening += this.HandleAdOpened;
            this.bannerView.OnAdClosed += this.HandleAdClosed;
            this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

            // Load a banner ad.
            //this.bannerView.LoadAd(this.CreateAdRequest());
            RequestAds();
            
        }
#endregion

        public void RequestAds()
        {
            this.bannerView.LoadAd(admobAdRequest.CreateAdRequest());
        }

        public void ShowBanner()
        {
            RequestAds();
            this.bannerView.Show();
        }

        public void HideBanner()
        {
            this.bannerView.Hide();
        }

#region Banner callback handlers

        public void HandleAdLoaded(object sender, EventArgs args)
        {
            if (isFirstTime == false)
            {
                this.HideBanner();
                isFirstTime = true;
            }
            if (onAdLoaded != null)
                onAdLoaded.Invoke();
        }

        public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            if (onAdFailedToLoad != null)
                onAdFailedToLoad.Invoke(args);
        }

        public void HandleAdOpened(object sender, EventArgs args)
        {
            if (onAdOpening != null)
                onAdOpening.Invoke();
        }

        public void HandleAdClosed(object sender, EventArgs args)
        {
            if (onAdClosed != null)
                onAdClosed.Invoke();
        }

        public void HandleAdLeftApplication(object sender, EventArgs args)
        {
            if (onAdLeavingApplication != null)
                onAdLeavingApplication.Invoke();
        }

#endregion
    }
}
