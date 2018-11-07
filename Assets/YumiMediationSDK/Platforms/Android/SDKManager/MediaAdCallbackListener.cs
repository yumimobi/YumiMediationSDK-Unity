using UnityEngine;
public class MediaAdCallbackListener: MonoBehaviour,YumiUnityAdUtils.MediaAdCallbackListener {

    public void onMediaIncentived(string data)
    {
		Logger.LogError("yumiMobi SDK Media Incentived Succeed callBack:" + data);
        //Give a reward       
    }
    public void onMediaExposure(string data)
    {
		Logger.LogError("yumiMobi SDK Media Exposure Succeed :" + data);
    }
    public void onMediaClosed(string data)
    {
		Logger.LogError("yumiMobi SDK Media Closed :" + data);
        //Once the video screen is successful, it will be retrained to see if the video screen is loaded
		#if UNITY_ANDROID
		YumiSDKAdapter.Instance.GetRotaIsMediaPrepared = false;
		#endif
    }
    public void onMediaClicked(string data)
    {
		Logger.LogError("yumiMobi SDK Media Clicked :" + data);
    }
   
}
