using System;
using YumiMediationSDK.Common;
using System.Reflection;

namespace YumiMediationSDK.Api
{
    public class YumiBannerView
    {
        private IYumiBannerClient client;
        // Creates a BannerView and adds it to the view hierarchy.
        public YumiBannerView(string placementId, string channelId, string versionId, YumiAdPosition adPosition)
        {
            Type yumiAdsClientFactory = Type.GetType(
                "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildBannerClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiBannerClient)method.Invoke(null, null);
            client.CreateBannerView(placementId,channelId,versionId,adPosition);

            ConfigureBannerEvents();
        }

        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the banner ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the banner ad is click.
        public event EventHandler<EventArgs> OnAdClick;

        // Loads an ad into the BannerView.
        public void LoadAd(bool isSmart)
        {
            client.LoadAd(isSmart);
        }
        // Hides the BannerView from the screen.
        public void Hide()
        {
            client.HideBannerView();
        }

        // Shows the BannerView on the screen.
        public void Show()
        {
            client.ShowBannerView();
        }

        // Destroys the BannerView.
        public void Destroy()
        {
            client.DestroyBannerView();
        }

        private void ConfigureBannerEvents()
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

            this.client.OnAdClick += (sender, args) =>
            {
                if (this.OnAdClick != null)
                {
                    this.OnAdClick(this, args);
                }
            };


        }
    }
}
