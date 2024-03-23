using AdMob.AdMobManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour 
{
	private static ServiceManager ins;

    [Header("Service Property")]
    public AdMobManager adsManager = null;
    public UnityAdsManager unityAdsManager = null;
    public CharboostService charboostService = null;

    [Header("Property Enable Service")]
	public bool IsEnableAdMobManager = true;
	public bool IsEnableUnityAdsManager = true;

	public static ServiceManager Instance 
	{
		get
		{ 
			if (ins == null) {
				ins = new GameObject ("ServiceManager").AddComponent<ServiceManager>();
                ins.Initialize();
				DontDestroyOnLoad (ins.gameObject);
			}
			return ins;
		}
	}


	public void Initialize()
	{
        if (IsEnableAdMobManager == true)
        {
            adsManager = new AdMobManager(new AdMobConfiguration() {
                //========== Test ID ======================================//
                Android_AppID = "",
                Android_BannerID = "ca-app-pub-3426258309246005/5267356861",
                Android_InterstitialID = "ca-app-pub-3426258309246005/5824408995",
                Android_VideoRewordID = ""

                //========= Real ID ========================================//
            });
            adsManager.AdMobRequest();
        }

        if (IsEnableUnityAdsManager == true) {
			unityAdsManager = new GameObject ("UnityAdsManager").AddComponent<UnityAdsManager> ();
			unityAdsManager.transform.SetParent (this.transform);
			unityAdsManager.Initialize ();
		}

        charboostService = new GameObject("CharboostService").AddComponent<CharboostService>();
        charboostService.transform.SetParent(this.transform);
    }
}
