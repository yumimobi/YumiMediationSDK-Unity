using System;
using YumiMediationSDK.Api;

namespace YumiMediationSDK.Common
{
    public interface IYumiSplashClient
    {
        // Ad event fired when the splash ad success to show 
        event EventHandler<EventArgs> OnAdSuccessToShow;
        // Ad event fired when the splash ad has failed to load.
        event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        // Ad event fired when the splash ad is closed.
        event EventHandler<EventArgs> OnAdClosed;
        // Ad event fired when the splash ad is clicked.
        event EventHandler<EventArgs> OnAdClicked;

        // Creates an Splash.
        void CreateSplashAd(string placementId, string channelId, string versionId, YumiSplashOptions splashOptions);
        //load and show full screen splash
        void LoadAdAndShow();
        // Destroies the splash ad.
        void DestroySplashAd();
    }
}
