using UnityEngine;
using System.Collections;

using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour 
{
	public bool IsTestMode = false;
	[SerializeField] string gameID_Android 	= "2749971";
	[SerializeField] string gameID_IOS 		= "2749971";


	public static System.Action OnFinished;
	public static System.Action OnSkipped;
	public static System.Action OnFailed;

	private string zone_key = "rewardedVideo";

	public static System.Action<bool> onVideoAvailable;

	private ShowOptions options;

	public void Initialize()
	{
		string unity_key = gameID_Android;

#if UNITY_ANDROID
		unity_key = gameID_Android;
#else
		unity_key = gameID_IOS;
#endif
		if (Advertisement.isSupported && !Advertisement.isInitialized) {
			Advertisement.Initialize (unity_key, IsTestMode);
		}
		options = new ShowOptions ();
		options.resultCallback = AdCallbackhandler;

		StartCheckVideoAvailable ();
	}

	public void ShowAd()
	{
		if (string.Equals (zone_key, ""))
			zone_key = null;

		if (Advertisement.IsReady (zone_key))
			Advertisement.Show (zone_key, options);
	}

	public bool IsReady()
	{
		return Advertisement.IsReady (zone_key);
	}

	private void AdCallbackhandler (ShowResult result)
	{
		OnFinished -= StartCheckVideoAvailable;
		OnFinished += StartCheckVideoAvailable;

		OnSkipped -= StartCheckVideoAvailable;
		OnSkipped += StartCheckVideoAvailable;

		OnFailed  -= StartCheckVideoAvailable;
		OnFailed  += StartCheckVideoAvailable;

		switch(result)
		{
			case ShowResult.Finished:
				{
                    //				Debug.Log ("Ad Finished. Rewarding player...");
					if (OnFinished != null)
						OnFinished.Invoke ();
					break;
				}
			case ShowResult.Skipped:
				{
                    //				Debug.Log ("Ad skipped. Son, I am dissapointed in you");
                    if (OnSkipped != null)
						OnSkipped.Invoke ();
					break;
				}
			case ShowResult.Failed:
				{
                    //				Debug.Log ("I swear this has never happened to me before");
                    if (OnFailed != null)
						OnFailed.Invoke ();
					break;
				}
		}
	}

	private void StartCheckVideoAvailable()
	{
        StopCheckVideoAvailable();
        _coroutine_video = StartCoroutine (IEStartCheckVideoAvailable ());
	}

	private void StopCheckVideoAvailable()
	{
		if (_coroutine_video != null)
			StopCoroutine (_coroutine_video);
	}

	private Coroutine _coroutine_video;
	private IEnumerator IEStartCheckVideoAvailable()
	{
		while (true) {
			yield return new WaitForSeconds (2f);
			if (Advertisement.IsReady (zone_key) == true) {
				if (onVideoAvailable != null)
					onVideoAvailable.Invoke (true);
//				Debug.Log ("onVideoAvailable is true");
				break;
			} else {
				if (onVideoAvailable != null)
					onVideoAvailable.Invoke (false);
//				Debug.Log ("onVideoAvailable is false");
			}
		}
	}
}
