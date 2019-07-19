using System;
using YumiMediationSDK.Common;
using System.Reflection;

namespace YumiMediationSDK.Api
{
    public class YumiInterstitialAd
    {
        private IYumiInterstitialClient client;
        /// <summary>
        /// Creates an InterstitialAd and loads
        /// </summary>
        /// <param name="placementId">Placement identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="versionId">Version identifier.</param>
        public YumiInterstitialAd(string placementId, string channelId, string versionId)
        {

            client = YumiAdsClientFactory.BuildInterstitialClient();
            client.CreateInterstitialAd(placementId,channelId,versionId);

            ConfigureInterstitialEvents();
        }
        /// <summary>
        /// Determines whether the InterstitialAd has loaded.
        /// </summary>
        /// <returns><c>true</c>, if ready was ised, <c>false</c> otherwise.</returns>
        public bool IsReady(){
            return this.client.IsInterstitialReady();
        }
        /// <summary>
        ///  Displays the InterstitialAd.
        /// </summary>
        public void Show(){
            this.client.ShowInterstitial();
        }
        /// <summary>
        ///  Destroys the InterstitialAd.
        /// </summary>
        public void Destroy(){
            this.client.DestroyInterstitial();
        }

        /// <summary>
        /// Occurs when the interstitial ad has been received.
        /// </summary>
        public event EventHandler<EventArgs> OnAdLoaded;
        /// <summary>
        /// Occurs when the interstitial ad has failed to load.
        /// </summary>
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        /// <summary>
        /// Occurswhen the interstitial ad has failed to show.
        /// </summary>
        public event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        /// <summary>
        /// Occurs when the interstitial ad is opened.
        /// </summary>
        public event EventHandler<EventArgs> OnAdOpening;
        /// <summary>
        /// Occurs when the interstitial ad has started playing.
        /// </summary>
        public event EventHandler<EventArgs> OnAdStartPlaying;
        /// <summary>
        /// Occurs when the interstitial ad is closed.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClosed;
        /// <summary>
        /// Occurs  when the interstitial ad is clicked.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClicked;

        private void ConfigureInterstitialEvents()
        {
            this.client.OnAdLoaded += (sender, args) =>
            {
                if (this.OnAdLoaded != null)
                {
                    this.OnAdLoaded(this, args);
                }
            };

            this.client.OnAdFailedToLoad += (sender, args) =>
            {
                if (this.OnAdFailedToLoad != null)
                {
                    this.OnAdFailedToLoad(this, args);
                }
            };
            this.client.OnAdFailedToShow += (sender, args) =>
            {
                if (this.OnAdFailedToShow != null)
                {
                    this.OnAdFailedToShow(this, args);
                }
            };
            this.client.OnAdOpening += (sender, args) =>
            {
                if (this.OnAdOpening != null)
                {
                    this.OnAdOpening(this, args);
                }
            };
            this.client.OnAdStartPlaying += (sender, args) =>
            {
                if (this.OnAdStartPlaying != null)
                {
                    this.OnAdStartPlaying(this, args);
                }
            };
            this.client.OnAdClicked += (sender, args) =>
            {
                if (this.OnAdClicked != null)
                {
                    this.OnAdClicked(this, args);
                }
            };
            this.client.OnAdClosed += (sender, args) =>
            {
                if (this.OnAdClosed != null)
                {
                    this.OnAdClosed(this, args);
                }
            };


        }
    }
}
