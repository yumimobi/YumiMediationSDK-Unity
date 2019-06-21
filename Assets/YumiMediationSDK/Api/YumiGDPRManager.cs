using System;
using System.Reflection;
using YumiMediationSDK.Common;

namespace YumiMediationSDK.Api
{
    public class YumiGDPRManager
    {

        private IYumiGDPRManagerClient client;

        private static readonly YumiGDPRManager instance = new YumiGDPRManager();
        /// <summary>
        /// single YumiGDPRManager instance.
        /// </summary>
        public static YumiGDPRManager Instance
        {
            get
            {
                return instance;
            }
        }

        // Creates a Singleton YumiGDPRManager.
        private YumiGDPRManager()
        {
            Type yumiAdsClientFactory = Type.GetType(
                "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildGDPRManagerClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiGDPRManagerClient)method.Invoke(null, null);
            client.CreateGDPRManager();
           
        }
        /// <summary>
        /// update networks consent status.
        /// consent: Your user's consent string. Default is the user has given consent to store and process personal information.
        /// </summary>
        /// <param name="consentStatus"></param>
        public void UpdateNetworksConsentStatus(YumiConsentStatus consentStatus)
        {
            client.UpdateNetworksConsentStatus(consentStatus);
        }
    }
}
