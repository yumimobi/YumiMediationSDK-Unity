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
		/// <summary>
		/// The user has granted consent for personalized ads.
		/// </summary>
		PERSONALIZED,

		/// <summary>
		/// The user has granted consent for non-personalized ads.
		/// </summary>
		NONPERSONALIZED,
		/// <summary>
		///  The user has neither granted nor declined consent for personalized or non-personalized ads.
		/// </summary>
		UNKNOWN

	}
}