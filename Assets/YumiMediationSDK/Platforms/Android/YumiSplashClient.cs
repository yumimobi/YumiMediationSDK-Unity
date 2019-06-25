#if UNITY_ANDROID

using System;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;
using UnityEngine;

namespace YumiMediationSDK.Android
{
    public class YumiSplashClient : AndroidJavaProxy, IYumiSplashClient
    {
        private AndroidJavaObject splashAd;

        public YumiSplashClient() : base(YumiUtils.UnitySplashListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(YumiUtils.UnityActivityClassName);
            AndroidJavaObject activity =
                    playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            this.splashAd = new AndroidJavaObject(
                YumiUtils.SplashClassName, activity, this);
        }
        // Ad event fired when the splash ad success to show 
        public event EventHandler<EventArgs> OnAdSuccessToShow;
        // Ad event fired when the splash ad has failed to load.
        public event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        // Ad event fired when the splash ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;
        // Ad event fired when the splash ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;

        #region implement IYumiSplashClient
        // Creates an Splash.
        public void CreateSplashAd(string placementId, string channelId, string versionId, YumiSplashOptions splashOptions) {
                        
             this.splashAd.Call(
                    "create",
                new object[5] { placementId, channelId, versionId , splashOptions.adFetchTime, splashOptions.adBottomViewHeight});
        }

        //load and show full screen splash
        public void LoadAdAndShow() {
            this.splashAd.Call("loadAdAndShow");
        }

        // Destroies the splash ad.
        public void DestroySplashAd() {
            this.splashAd.Call("destroy");
        }
        #endregion

        #region Callbacks from UnitySplashListener.
        public void onAdSuccessToShow()
        {
            if (this.OnAdSuccessToShow != null)
            {
                this.OnAdSuccessToShow(this, EventArgs.Empty);
            }
        }

        public void onAdFailedToShow(string errorReason)
        {
            if (this.OnAdFailedToShow != null)
            {
                YumiAdFailedToShowEventArgs args = new YumiAdFailedToShowEventArgs()
                {
                    Message = errorReason
                };
                this.OnAdFailedToShow(this, args);
            }
        }

        public void onAdClosed()
        {
            if (this.OnAdClosed != null)
            {
                this.OnAdClosed(this, EventArgs.Empty);
            }
        }

        public void onAdClicked()
        {
            if (this.OnAdClicked != null)
            {
                this.OnAdClicked(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
#endif


