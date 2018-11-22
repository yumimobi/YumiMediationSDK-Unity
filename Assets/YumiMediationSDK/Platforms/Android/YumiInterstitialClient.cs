using System;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;
using UnityEngine;

namespace YumiMediationSDK.Android
{
    public class YumiInterstitialClient : AndroidJavaProxy, IYumiInterstitialClient
    {

        private AndroidJavaObject interstitial;

        public YumiInterstitialClient() : base(YumiUtils.UnityInterstitialAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(YumiUtils.UnityActivityClassName);
            AndroidJavaObject activity =
                    playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            this.interstitial = new AndroidJavaObject(
                YumiUtils.InterstitialClassName, activity, this);
        }

        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the interstitial ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the interstitial ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;
        // Ad event fired when the interstitial ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;

        #region implement IYumiInterstitialClient
        // Creates an InterstitialAd.
        public void CreateInterstitialAd(string placementId, string channelId, string versionId){
            this.interstitial.Call(
                    "create",
                new object[3] { placementId, channelId, versionId });
        }

        // Determines whether the interstitial has loaded.
        public bool IsInterstitialReady(){

            return this.interstitial.Call<bool>("isReady");
        }

        // Shows the InterstitialAd.
        public void ShowInterstitial(){
            this.interstitial.Call("showInterstitial");
        }

        // Destroys an InterstitialAd.
        public void DestroyInterstitial(){
            this.interstitial.Call("destroyInterstitial");
        }

        #endregion
        #region Callbacks from UnityInterstitialAdListener.

        public void onAdLoaded()
        {
            if (this.OnAdLoaded != null)
            {
                this.OnAdLoaded(this, EventArgs.Empty);
            }
        }

        public void onAdFailedToLoad(string errorReason)
        {
            if (this.OnAdFailedToLoad != null)
            {
                YumiAdFailedToLoadEventArgs args = new YumiAdFailedToLoadEventArgs()
                {
                    Message = errorReason
                };
                this.OnAdFailedToLoad(this, args);
            }
        }

        public void onAdClick()
        {
            if (this.OnAdClicked != null)
            {
                this.OnAdClicked(this, EventArgs.Empty);
            }
        }
        public void onAdClosed()
        {
            if (this.OnAdClosed != null)
            {
                this.OnAdClosed(this, EventArgs.Empty);
            }
        }


        #endregion

    }
}
