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
            debugcenter = new AndroidJavaObject(
                YumiUtils.DebugCenterClassName, activity);
        }

        public void CallYumiMediationDebugCenter(string bannerPlacementID, string interstitialPlacementID, string videoPlacementID, string nativePlacementID, string channelID, string versionID)
        {
            debugcenter.Call("presentDebugCenter", bannerPlacementID, interstitialPlacementID, videoPlacementID, nativePlacementID, channelID, versionID);
        }

        public void ChangeToTestServer()
        {
            debugcenter.Call("changeToTestServer");
        }
    }
}
#endif