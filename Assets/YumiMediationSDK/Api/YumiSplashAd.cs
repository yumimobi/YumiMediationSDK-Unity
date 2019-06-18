using System;
using YumiMediationSDK.Common;
using System.Reflection;

namespace YumiMediationSDK.Api
{
    public class YumiSplashAd
    {
        private IYumiSplashClient client;
        /// <summary>
        /// Creates a splash
        /// </summary>
        /// <param name="placementId">Placement identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="versionId">Version identifier.</param>
        /// <param name="splashOptions">Banner options.</param>
        public YumiSplashAd(string placementId, string channelId, string versionId, YumiSplashOptions splashOptions)
        {
            Type yumiAdsClientFactory = Type.GetType(
                "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildBannerClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiSplashClient)method.Invoke(null, null);
        
            client.CreateSplashAd(placementId, channelId, versionId, splashOptions);

            ConfigureSPlashEvents();
        }
        /// <summary>
        /// Occurs when on ad success to show.
        /// </summary>
        public event EventHandler<EventArgs> OnAdSuccessToShow;
        /// <summary>
        /// Occurs when on ad failed to show.
        /// </summary>
        public event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        /// <summary>
        /// Occurs when on ad closed.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClosed;
        /// <summary>
        /// Occurs when on ad clicked.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClicked;
        /// <summary>
        /// Loads the ad and show.
        /// </summary>
        public void LoadAdAndShow()
        {
            client.LoadAdAndShow();
        }
        /// <summary>
        /// Destroies the splash ad.
        /// </summary>
        public void DestroySplashAd()
        {
            client.DestroySplashAd();
        }

        private void ConfigureSPlashEvents() 
        {
            this.client.OnAdSuccessToShow += (sender, args) =>
            {
                if (OnAdSuccessToShow != null)
                {
                    OnAdSuccessToShow(this, args);
                }
            };
            this.client.OnAdFailedToShow += (sender, args) =>
            {
                if (OnAdFailedToShow != null)
                {
                    OnAdFailedToShow(this, args);
                }
            };
            this.client.OnAdClosed += (sender, args) =>
            {
                if (OnAdClosed != null)
                {
                    OnAdClosed(this, args);
                }
            };
            this.client.OnAdClicked += (sender, args) =>
            {
                if (OnAdClicked != null)
                {
                   OnAdClicked(this, args);
                }
            };
        }
    }
}
