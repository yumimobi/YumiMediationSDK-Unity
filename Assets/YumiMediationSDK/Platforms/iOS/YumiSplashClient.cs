using System;
using YumiMediationSDK.Common;
using YumiMediationSDK.Api;

namespace YumiMediationSDK.iOS
{
    public class YumiSplashClient : IYumiSplashClient
    {

        private IntPtr splashPtr;
        private IntPtr splashClientPtr;

        // Ad event fired when the splash ad success to show 
        public event EventHandler<EventArgs> OnAdSuccessToShow;
        // Ad event fired when the splash ad has failed to load.
        public event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        // Ad event fired when the splash ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;
        // Ad event fired when the splash ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;

        // Creates an Splash.
        public void CreateSplashAd(string placementId, string channelId, string versionId, YumiSplashOptions splashOptions) 
        { 

        }
        //load and show full screen splash
        public void LoadAdAndShow()
        { 
        
        }
        // Destroies the splash ad.
        public void DestroySplashAd()
        { 

        }
    }
}
