using System;

namespace YumiMediationSDK.Common
{
    public interface IYumiGDPRManagerClient
    {
        /// <summary>
        /// Create gdpr manager instance
        /// </summary>
        void CreateGDPRManager();
        /// <summary>
        /// Update sdk gdpr consent
        /// </summary>
        void UpdateNetworksConsentStatus(YumiConsentStatus consentStatus);

    }
    public enum YumiConsentStatus
    {
        PERSONALIZED,
        ///The user has granted consent for non-personalized ads.
        NONPERSONALIZED,
        /// The user has neither granted nor declined consent for personalized or non-personalized ads.
        UNKNOWN
    
    }
}