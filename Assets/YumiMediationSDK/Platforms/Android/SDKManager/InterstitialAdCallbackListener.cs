using UnityEngine;
public class InterstitialAdCallbackListener:MonoBehaviour,YumiUnityAdUtils.InterstitialAdCallbackListener
{
    public void onInterstitialPreparedFailed(string data)
    {
		Logger.LogError("yumiMobi SDK Interstitial Prepared Failed :" + data);
    }
    public void onInterstitialPrepared(string data)
    {
		Logger.LogError("yumiMobi SDK Interstitial Prepared Succeed:" + data);
    }
    public void onInterstitialExposure(string data)
    {
		Logger.LogError("yumiMobi SDK Interstitial Exposure Succeed:" + data);
    }
    public void onInterstitialClosed(string data)
    {
		Logger.LogError("yumiMobi SDK Interstitial Closed :" + data);
    }
    public void onInterstitialClicked(string data)
    {
		Logger.LogError("yumiMobi SDK Interstitial Clicke :" + data);
    }
}
