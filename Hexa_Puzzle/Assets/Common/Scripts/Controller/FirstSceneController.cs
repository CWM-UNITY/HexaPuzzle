using UnityEngine;
using System.Collections;

public class FirstSceneController : MonoBehaviour
{
	public static FirstSceneController instance;

	private void Awake()
	{
		instance = this;
		Application.targetFrameRate = 60;
        CPlayerPrefs.useRijndael(CommonConst.ENCRYPTION_PREFS);

		if (!CUtils.IsGameInitialzied())
		{
			CUtils.SetInitGame();
		}

		CUtils.SetAndroidVersion(GameVersion.ANDROID);
		CUtils.SetIOSVersion(GameVersion.IOS);
		CUtils.SetWindowsPhoneVersion(GameVersion.WP);
	}

	private void Update()
    {
#if !UNITY_WSA
        if (Input.GetKeyDown(KeyCode.Escape) && !DialogController.instance.IsDialogShowing())
		{
			QuitGame.instance.ShowConfirmDialog();
        }
#endif
    }
}
