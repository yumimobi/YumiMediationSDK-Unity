using System;
using YumiMediationSDK.Api;

namespace YumiMediationSDK.Common
{
    public interface IYumiBannerClient
    {
        // Ad event fired when the banner ad has been received.
        event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the banner ad has failed to load.
        event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the banner ad is click.
        event EventHandler<EventArgs> OnAdClick;
    
        // Creates a banner view and adds it to the view hierarchy.
        void CreateBannerView(string placementId, string channelId, string versionId, YumiAdPosition adPosition);

        // Requests a new ad for the banner view.
        void LoadAd(bool isSmart);

        // Shows the banner view on the screen.
        void ShowBannerView();

        // Hides the banner view from the screen.
        void HideBannerView();

        // Destroys a banner view.
        void DestroyBannerView();

    }
}
