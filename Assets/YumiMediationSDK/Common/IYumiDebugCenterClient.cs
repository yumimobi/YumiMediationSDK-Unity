using System;

namespace YumiMediationSDK.Common
{
    public interface IYumiDebugCenterClient
    {
        // call debugcenter to test ads
        void CallYumiMediationDebugCenter(string bannerPlacementID, string interstitialPlacementID, string videoPlacementID, string nativePlacementID, string channelID, string versionID);

        // Changes target server host
        void ChangeToTestServer();
    }
}
