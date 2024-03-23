
using GoogleMobileAds.Api;
using System;

namespace AdMob.AdMobManager
{
    public class AdmobAdRequest
    {
        public AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddTestDevice("")
                .AddKeyword("game")
                .SetGender(Gender.Male)
                .SetBirthday(new DateTime(1985, 1, 1))
                .TagForChildDirectedTreatment(false)
                .AddExtra("color_bg", "9B30FF")
                .Build();
        }
    }
}
