#if UNITY_IOS

using YumiMediationSDK.Common;

namespace YumiMediationSDK.iOS
{
    public class YumiGDPRManagerClient: IYumiGDPRManagerClient
    {
        public void CreateGDPRManager()
        {
            Logger.Log("YumiGDPRManagerClient: CreateGDPRManager");
        }

        public void UpdateNetworksConsentStatus(YumiConsentStatus consentStatus)
        {
            
            YumiExterns.UpdateNetworksConsentStatus((int)consentStatus);
        }
    }
}
#endif