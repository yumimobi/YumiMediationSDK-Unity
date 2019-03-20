using System;
using YumiMediationSDK.Common;
using System.Reflection;

namespace YumiMediationSDK.Api
{
    public class YumiInterstitialAd
    {
        private IYumiInterstitialClient client;
        // Creates an InterstitialAd and loads
        public YumiInterstitialAd(string placementId, string channelId, string versionId)
        {
            Type yumiAdsClientFactory = Type.GetType(
                "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildInterstitialClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiInterstitialClient)method.Invoke(null, null);
            client.CreateInterstitialAd(placementId,channelId,versionId);

            ConfigureInterstitialEvents();
        }
        // Determines whether the InterstitialAd has loaded.
        public bool IsReady(){
            return this.client.IsInterstitialReady();
        }
        // Displays the InterstitialAd.
        public void Show(){
            this.client.ShowInterstitial();
        }
        // Destroys the InterstitialAd.
        public void Destroy(){
            this.client.DestroyInterstitial();
        }

        // Ad event fired when the interstitial ad has loaded.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the interstitial ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the interstitial ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;
        // Ad event fired when the interstitial ad is clicked.
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
