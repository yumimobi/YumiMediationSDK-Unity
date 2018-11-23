using System;
using YumiMediationSDK.Common;
using System.Reflection;

namespace YumiMediationSDK.Api
{
    public class YumiDebugCenter
    {
        private IYumiDebugCenterClient client;
        public YumiDebugCenter()
        {
            Type yumiAdsClientFactory = Type.GetType(
                  "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildDebugCenterClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiDebugCenterClient)method.Invoke(null, null);
           
        }
        public void PresentYumiMediationDebugCenter(string bannerPlacementID, string interstitialPlacementID, string videoPlacementID, string channelID, string versionID){
            this.client.CallYumiMediationDebugCenter(bannerPlacementID,interstitialPlacementID,videoPlacementID,channelID,versionID);
        }

    }
}
