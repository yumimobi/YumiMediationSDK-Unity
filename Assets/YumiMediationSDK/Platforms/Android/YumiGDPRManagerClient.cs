#if UNITY_ANDROID

using YumiMediationSDK.Common;
using UnityEngine;

namespace YumiMediationSDK.Android

{
    public class YumiGDPRManagerClient: IYumiGDPRManagerClient
    {
        private AndroidJavaObject yumiSettings;

        public void CreateGDPRManager()
        {
            Logger.Log("YumiGDPRManagerClient: CreateGDPRManager");
            this.yumiSettings = new AndroidJavaObject(
                 YumiUtils.YumiSettingsClassName);
        }

        public void UpdateNetworksConsentStatus(YumiConsentStatus consentStatus)
        {
            this.yumiSettings.Call(
                    "setGDPRConsent",
                new object[1] { (int) consentStatus });
        }
    }
}
#endif
