using System;
using YumiMediationSDK.Api;

namespace YumiMediationSDK.Common
{
    public class YumiDummyClient : IYumiBannerClient
    {
        public YumiDummyClient()
        {
            Logger.LogError("dummy client");
        }
        // Disable warnings for unused dummy ad events.
#pragma warning disable 67
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the banner ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the banner ad is click.
        public event EventHandler<EventArgs> OnAdClick;
       
#pragma warning restore 67
        // banner method
        // Creates a banner view and adds it to the view hierarchy.
        public void CreateBannerView(string placementId, string channelId, string versionId, YumiAdPosition adPosition){
            Logger.LogError("create banner");
        }

        // Requests a new ad for the banner view.
        public void LoadAd(bool isSmart){
            Logger.LogError("load ad");
        }

        // Shows the banner view on the screen.
        public void ShowBannerView(){
            Logger.LogError("ShowBannerView");
        }

        // Hides the banner view from the screen.
        public void HideBannerView(){
            Logger.LogError("HideBannerView");
        }

        // Destroys a banner view.
        public void DestroyBannerView(){
            Logger.LogError("DestroyBannerView");
        }

    }
}
