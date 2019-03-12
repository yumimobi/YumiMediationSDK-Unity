#if UNITY_IOS
using System;
using YumiMediationSDK.Common;

namespace YumiMediationSDK.iOS
{
    public class YumiDebugCenterClient  : IYumiDebugCenterClient
    {

        public void CallYumiMediationDebugCenter(string bannerPlacementID, string interstitialPlacementID, string nativePlacementID, string videoPlacementID, string channelID, string versionID){
            YumiExterns.PresentDebugCenter(bannerPlacementID,interstitialPlacementID,videoPlacementID,"",channelID,versionID);
        }
    }
}
#endif