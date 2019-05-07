using System;
using YumiMediationSDK.Common;
using System.Reflection;

namespace YumiMediationSDK.Api
{
    public class YumiDebugCenter
    {
        private IYumiDebugCenterClient client;
        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public YumiDebugCenter()
        {
            Type yumiAdsClientFactory = Type.GetType(
                  "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildDebugCenterClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiDebugCenterClient)method.Invoke(null, null);
        }
        /// <summary>
        /// Presents the yumi mediation debug center.
        /// </summary>
        /// <param name="bannerPlacementID">Banner placement identifier.</param>
        /// <param name="interstitialPlacementID">Interstitial placement identifier.</param>
        /// <param name="videoPlacementID">Video placement identifier.</param>
        /// <param name="nativePlacementID">Native placement identifier.</param>
        /// <param name="channelID">Channel identifier.</param>
        /// <param name="versionID">Version identifier.</param>
        public void PresentYumiMediationDebugCenter(string bannerPlacementID, string interstitialPlacementID, string videoPlacementID, string nativePlacementID, string channelID, string versionID){
            this.client.CallYumiMediationDebugCenter(bannerPlacementID,interstitialPlacementID,videoPlacementID, nativePlacementID, channelID,versionID);
        }
        /// <summary>
        /// Changes to test server.
        /// </summary>
        public void ChangeToTestServer()
        {
            client.ChangeToTestServer();
        }
    }
}
