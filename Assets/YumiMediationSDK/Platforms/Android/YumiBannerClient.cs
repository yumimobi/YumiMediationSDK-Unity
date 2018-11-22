using System;
using YumiMediationSDK.Common;
using YumiMediationSDK.Api;
using UnityEngine;

namespace YumiMediationSDK.Android
{
    public class YumiBannerClient : AndroidJavaProxy,IYumiBannerClient
    {
        private AndroidJavaObject bannerView;
        public YumiBannerClient() : base(YumiUtils.UnityBannerAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(YumiUtils.UnityActivityClassName);
            AndroidJavaObject activity =
                    playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            this.bannerView = new AndroidJavaObject(
                YumiUtils.BannerViewClassName, activity, this);
        }
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the banner ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the banner ad is click.
        public event EventHandler<EventArgs> OnAdClick;

        #region implement IYumiBannerClient

        // Creates a banner view and adds it to the view hierarchy.
        public void CreateBannerView(string placementId, string channelId, string versionId, YumiAdPosition adPosition){
            this.bannerView.Call(
                    "create",
                new object[3] { placementId,channelId,versionId });
        }

        // Requests a new ad for the banner view.
        public void LoadAd(bool isSmart){
            this.bannerView.Call("requestAd");
        }

        // Shows the banner view on the screen.
        public void ShowBannerView(){
            this.bannerView.Call("showBannerView");
        }

        // Hides the banner view from the screen.
        public void HideBannerView(){
            this.bannerView.Call("hideBanner");
        }

        // Destroys a banner view.
        public void DestroyBannerView(){
            this.bannerView.Call("destroyBanner");
        }
        #endregion

        #region Callbacks from UnityBannerAdListener.

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
            if (this.OnAdClick != null)
            {
                this.OnAdClick(this, EventArgs.Empty);
            }
        }

       

        #endregion
    }
}
