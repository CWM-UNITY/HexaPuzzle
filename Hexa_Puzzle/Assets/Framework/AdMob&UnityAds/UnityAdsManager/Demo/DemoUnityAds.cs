using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using System.Linq;

public class DemoUnityAds : MonoBehaviour 
{
	public static DemoUnityAds Instance;

	[SerializeField] Text _text_bonus;
	[SerializeField] Button buttonAds;

	public int count = 0;

	private void Awake()
	{
		Instance = this;
		_text_bonus.text = count.ToString ();
//		ad = ServiceManager.Instance.adMobManager;

		ServiceManager sm = ServiceManager.Instance;
//		sm.IsEnableAdMobManager = false;
//		sm.IsEnableFacebookService = false;
//		sm.IsEnableUnityAdsManager = false;

		sm.Initialize ();
	}


	private void OnEnable()
	{
		UnityAdsManager.OnFinished += OnFinished; 
		UnityAdsManager.OnFailed += OnFailed; 
		UnityAdsManager.OnSkipped += OnSkipped; 

		UnityAdsManager.onVideoAvailable += OnVideoAvailable;
	}

	private void OnDisable()
	{
		UnityAdsManager.OnFinished -= OnFinished; 
		UnityAdsManager.OnFailed -= OnFailed; 
		UnityAdsManager.OnSkipped -= OnSkipped; 

		UnityAdsManager.onVideoAvailable -= OnVideoAvailable;
	}

	public void OnVideoAvailable(bool IsOK)
	{
		buttonAds.gameObject.SetActive(IsOK);
	}

	public void ShowAds()
	{
		buttonAds.gameObject.SetActive (false);
//		buttonAds.interactable = false;

		ServiceManager.Instance.unityAdsManager.ShowAd ();
	}

	//public void ShowBanner()
	//{
	//	ad.ShowBanner ();
	//}

	//public void HideBanner()
	//{
	//	ad.HideBanner ();
	//}

	//public void ShowInterstitial()
	//{
	//	ad.ShowInterstitial ();
	//}

	private void OnFinished()
	{
		count += 1;
		_text_bonus.text = count.ToString ();
		Debug.Log ("OnFinished");
	}

	private void OnFailed()
	{
		Debug.Log ("OnFailed");
	}

	private void OnSkipped()
	{
		Debug.Log ("OnSkipped");
	}



}
