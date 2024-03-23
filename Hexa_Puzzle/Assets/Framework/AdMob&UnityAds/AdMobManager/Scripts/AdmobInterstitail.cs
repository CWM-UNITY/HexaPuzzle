
using GoogleMobileAds.Api;
using System;

namespace AdMob.AdMobManager
{
    public class AdMobInterstitail
    {
        private InterstitialAd interstitial;

        private AdmobAdRequest admobAdRequest;

        public static Action onAdLoaded = null;
        public static Action<AdFailedToLoadEventArgs> onAdFailedToLoad = null;
        public static Action onAdOpening = null;
        public static Action onAdClosed = null;
        public static Action onAdLeavingApplication = null;

        public AdMobInterstitail(string _adUnitId)
        {
            admobAdRequest = new AdmobAdRequest();

            if (this.interstitial != null)
            {
                this.interstitial.Destroy();
            }

            // Create an interstitial.
            this.interstitial = new InterstitialAd(_adUnitId);

            // Register for ad events.
            this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
            this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
            this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
            this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
            this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

            // Load an interstitial ad.
            RequestInterstitial();
        }

        public bool IsLoaded()
        {
            return this.interstitial.IsLoaded();
        }

        public void ShowInterstitial()
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
            }
            else
            {
                RequestInterstitial();
            }
        }

        public void RequestInterstitial()
        {
            this.interstitial.LoadAd(admobAdRequest.CreateAdRequest());
        }

        #region Interstitial callback handlers

        public void HandleInterstitialLoaded(object sender, EventArgs args)
        {
            if (onAdLoaded != null)
                onAdLoaded.Invoke();
        }

        public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            if (onAdFailedToLoad != null)
                onAdFailedToLoad.Invoke(args);
        }

        public void HandleInterstitialOpened(object sender, EventArgs args)
        {
            if (onAdOpening != null)
                onAdOpening.Invoke();
        }

        public void HandleInterstitialClosed(object sender, EventArgs args)
        {
            if (onAdClosed != null)
                onAdClosed.Invoke();
        }

        public void HandleInterstitialLeftApplication(object sender, EventArgs args)
        {
            if (onAdLeavingApplication != null)
                onAdLeavingApplication.Invoke();
        }

        #endregion
    }
}
