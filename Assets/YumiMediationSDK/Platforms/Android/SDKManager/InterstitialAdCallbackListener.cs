using UnityEngine;
public class InterstitialAdCallbackListener : MonoBehaviour, YumiUnityAdUtils.InterstitialAdCallbackListener
{
    public void onInterstitialPreparedFailed(string data)
    {
       
        YumiSDKEventHandler.OnYumiAdInterstitialDidFailToLoad(data);
    }
    public void onInterstitialPrepared(string data)
    {
      
        YumiSDKEventHandler.OnYumiAdInterstitialDidLoad();
    }
    public void onInterstitialExposure(string data)
    {
       
    #if UNITY_ANDROID
        YumiSDKEventHandler.OnYumiAdInterstitialExposure();
    #endif

    }
    public void onInterstitialClosed(string data)
    {
        YumiSDKEventHandler.OnYumiAdInterstitialDidClose();
    }
    public void onInterstitialClicked(string data)
    {
        YumiSDKEventHandler.OnYumiAdInterstitialDidClick();
    }
}
