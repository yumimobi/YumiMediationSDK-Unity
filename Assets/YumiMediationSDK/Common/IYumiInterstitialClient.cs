using System;
using YumiMediationSDK.Api;

namespace YumiMediationSDK.Common
{
    public interface IYumiInterstitialClient
    {
        // Ad event fired when the interstitial ad has been received.
        event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the interstitial ad has failed to load.
        event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the interstitial ad is closed.
        event EventHandler<EventArgs> OnAdClosed;
        // Ad event fired when the interstitial ad is clicked.
        event EventHandler<EventArgs> OnAdClicked;

        // Creates an InterstitialAd.
        void CreateInterstitialAd(string placementId, string channelId, string versionId);

        // Determines whether the interstitial has loaded.
        bool IsInterstitialReady();

        // Shows the InterstitialAd.
        void ShowInterstitial();

        // Destroys an InterstitialAd.
        void DestroyInterstitial();
    }
}
