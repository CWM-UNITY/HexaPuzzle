//#define DEVELOPMENT
public class CommonConst
{
	public const string IOS_APP_ID = "961632429";
	public const string MAC_APP_ID = "1031341769";
    public const string BB_APP_ID = "59959909";
    public const string FACE_PAGE_ID = "416022028565283";
    public const string GP_PAGE_LINK = "https://plus.google.com/106625749530285902481/posts";

    public const iTween.DimensionMode ITWEEN_MODE = iTween.DimensionMode.mode2D;

    public const string FEED_LINK = "https://play.google.com/store/apps/details?id=com.superpow.fishshooter";
    public const string FEED_PICTURE = "http://s13.postimg.org/e5ztzvglj/banner1024x500.jpg";
    public static readonly int[] START_FRIEND_LEVELS = { 3, 5, 7, 12, 18 };
    public static int GetTargetScore(int level)
    {
        return 1000;
    }

    public const bool HAS_INVITE_FRIEND = true;

#if DEVELOPMENT
    public const int MIN_INVITE_FRIEND = 1;
    public const int MAX_INVITE_FRIEND = 20;
    public const bool ENCRYPTION_PREFS = false;
    public const int MIN_LEVEL_TO_RATE = 1;
    public const int ADS_PERIOD = 5;
#else
    public const int MIN_INVITE_FRIEND = 40;
    public const int MAX_INVITE_FRIEND = 50;
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
    public const bool ENCRYPTION_PREFS = true;
#else
    public const bool ENCRYPTION_PREFS = false;
#endif
    public const int MIN_LEVEL_TO_RATE = 3;
    public const int ADS_PERIOD = 2 * 60;
#endif

    public const int MAX_FRIEND_IN_MAP = 15;
    public const int FACE_AVATAR_SIZE = 100;

    public const int TOTAL_LEVELS = 50;
    public const int NOTIFICATION_DAILY_GIFT = 0;
    public static readonly string[] LEADERBOARD = { "CgkIx92ynI8eEAIQBw", "CgkIx92ynI8eEAIQCA" };
    public const int MAX_AUTO_SIGNIN = 2;
}
