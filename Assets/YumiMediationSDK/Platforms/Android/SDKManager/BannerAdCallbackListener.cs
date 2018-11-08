using UnityEngine;
public class BannerAdCallbackListener:MonoBehaviour,YumiUnityAdUtils.BannerAdCallbackListener {

    public void onBannerPreparedFailed(string data)
    {
        Logger.LogError("yumiMobi SDK Bannel Prepared Failed :" + data);
    }
    public void onBannerPrepared(string data)
    {
		Logger.LogError("yumiMobi SDK Bannel Prepared Succeed:" + data);
    }
    public void onBannerExposure(string data)
    {
		Logger.LogError("yumiMobi SDK Bannel Exposure Succeed:" + data);
    }
    public void onBannerClosed(string data)
    {
		Logger.LogError("yumiMobi SDK Bannel Closed:" + data);
    }
    public void onBannerClicked(string data)
    {
		Logger.LogError("yumiMobi SDK Bannel Clicked:" + data);
    }
}
