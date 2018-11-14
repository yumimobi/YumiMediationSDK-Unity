using UnityEngine;
public class BannerAdCallbackListener : MonoBehaviour, YumiUnityAdUtils.BannerAdCallbackListener
{

    public void onBannerPreparedFailed(string data)
    {

        YumiSDKEventHandler.OnYumiAdBannerDidFailToLoad(data);
    }
    public void onBannerPrepared(string data)
    {
       
        YumiSDKEventHandler.OnYumiAdBannerDidLoad();
    }

    public void onBannerExposure(string data)
    {
       
    #if UNITY_ANDROID
        YumiSDKEventHandler.OnYumiAdBannerExposure();
    #endif
    }
    public void onBannerClosed(string data)
    {
       
    #if UNITY_ANDROID
        YumiSDKEventHandler.OnYumiAdBannerDidClose();
    #endif
    }
    public void onBannerClicked(string data)
    {
       
        YumiSDKEventHandler.OnYumiAdBannerDidClick();
    }
}
