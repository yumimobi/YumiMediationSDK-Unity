using UnityEngine;
public class MediaAdCallbackListener: MonoBehaviour,YumiUnityAdUtils.MediaAdCallbackListener {

    public void onMediaIncentived(string data)
    {
		
        YumiSDKEventHandler.OnYumiAdVideoDidReward();
    }
    public void onMediaExposure(string data)
    {

        YumiSDKEventHandler.OnYumiAdVideoDidOpen();
        YumiSDKEventHandler.OnYumiAdVideoDidStartPlaying();
    }
    public void onMediaClosed(string data)
    {
       
        //Once the video screen is successful, it will be retrained to see if the video screen is loaded
        YumiSDKEventHandler.OnYumiAdVideoDidClose();
		#if UNITY_ANDROID
		YumiSDKAdapter.Instance.GetRotaIsMediaPrepared = false;
		#endif
    }
    public void onMediaClicked(string data)
    {
       
#if UNITY_ANDROID
        YumiSDKEventHandler.OnYumiAdVideoDidClick();
#endif
    }

}
