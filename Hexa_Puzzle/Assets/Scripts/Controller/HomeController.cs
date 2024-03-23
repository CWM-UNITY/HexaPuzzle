using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeController : BaseController {
    private const int FACEBOOK = 0;

    ServiceManager sm = null;

    protected override void Start()
    {
        base.Start();
        sm = ServiceManager.Instance;
        sm.adsManager.adMobBanner.ShowBanner();
    }

    public void OnClick(int index)
    {
        switch (index)
        {
            case FACEBOOK:
                CUtils.LikeFacebookPage(CommonConst.FACE_PAGE_ID);
                break;
        }
        Sound.instance.PlayButton();
    }
}
