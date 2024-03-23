
using GoogleMobileAds.Api;

namespace AdMob.AdMobManager
{
    #region AdMobConfiguration
    public class AdMobConfiguration
    {
        public string IOS_AppID = "";
        public string Android_AppID = "";

        public string IOS_BannerID = "";
        public string Android_BannerID = "";
        public string IOS_InterstitailID = "";
        public string Android_InterstitialID = "";
        public string IOS_VideoRewordID = "";
        public string Android_VideoRewordID = "";
    }
    #endregion

    public class AdMobManager
    {
        private string appID = string.Empty;
        private string banner_ID = string.Empty;
        private string interstitial_ID = string.Empty;
        private string showRewardVideo_key = string.Empty;

        public AdMobBanner adMobBanner = null;
        public AdMobInterstitail adMobInterstitail = null;
        public AdMobVideoAds adMobVideoAds = null;

        public AdMobManager(AdMobConfiguration adMobConfiguration)
        {

#if UNITY_ANDROID
            appID = adMobConfiguration.Android_AppID;
            banner_ID = adMobConfiguration.Android_BannerID;
            interstitial_ID = adMobConfiguration.Android_InterstitialID;
            showRewardVideo_key = adMobConfiguration.Android_VideoRewordID;
#else

            appID = adMobConfiguration.IOS_AppID;
            banner_ID = adMobConfiguration.IOS_BannerID;
            interstitial_ID = adMobConfiguration.IOS_InterstitailID;
            showRewardVideo_key = adMobConfiguration.IOS_VideoRewordID;
#endif
            MobileAds.Initialize(appID);

            adMobBanner = new AdMobBanner(banner_ID,AdSize.SmartBanner,AdPosition.Bottom);
            adMobInterstitail = new AdMobInterstitail(interstitial_ID);
            adMobVideoAds = new AdMobVideoAds(showRewardVideo_key);
        }

        public void AdMobRequest()
        {
            adMobBanner.RequestAds();
            adMobInterstitail.RequestInterstitial();
            adMobVideoAds.RequestRewardVideoAds();
        }

    }
}
