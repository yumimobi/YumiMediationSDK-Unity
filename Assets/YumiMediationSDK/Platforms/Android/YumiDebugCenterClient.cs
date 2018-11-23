#if UNITY_ANDROID
using System;
using YumiMediationSDK.Common;
using UnityEngine;

namespace YumiMediationSDK.Android
{
    public class YumiDebugCenterClient : IYumiDebugCenterClient
    {
        private AndroidJavaObject debugcenter;
        public YumiDebugCenterClient()
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(YumiUtils.UnityActivityClassName);
            AndroidJavaObject activity =
                    playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            this.debugcenter = new AndroidJavaObject(
                YumiUtils.DebugCenterClassName, activity);
        }

        public void CallYumiMediationDebugCenter(string bannerPlacementID, string interstitialPlacementID, string videoPlacementID, string channelID, string versionID){
            this.debugcenter.Call("presentDebugCenter",
                                  new object[5] { bannerPlacementID,interstitialPlacementID,videoPlacementID, channelID, versionID });
        }
    }
}
#endif