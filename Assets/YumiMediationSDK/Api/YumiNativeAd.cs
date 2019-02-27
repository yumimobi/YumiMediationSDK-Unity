using System;
using YumiMediationSDK.Common;
using System.Reflection;
using UnityEngine;

namespace YumiMediationSDK.Api
{
    public class YumiNativeAd
    {
        private IYumiNativeClient client;

        public YumiNativeAd(string placementId, string channelId, string versionId)
        {
            Type yumiAdsClientFactory = Type.GetType(
               "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildNativeClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiNativeClient)method.Invoke(null, null);
            client.CreateNativeAd(placementId, channelId, versionId);

            ConfigureNativeEvents();
        }
        // load native ad
        public void LoadNativeAd(int adCount)
        {
            this.client.LoadAd(adCount);
        }

        //report Impression
        public void ReportImpression(YumiNativeData nativeData)
        {
            this.client.ReportImpression(nativeData);
        }
        //report click
        public void ReportClick(YumiNativeData nativeData)
        {
            this.client.ReportClick(nativeData);
        }
        // Destroys native ad object.
        public void DestroyNativeAd()
        {
            this.client.DestroyNativeAd();
        }

        public void RegisterGameObjectsForInteraction(GameObject gameObject, RectTransform adViewRectTransform,RectTransform mediaViewRectTransform, RectTransform iconViewRectTransform, RectTransform ctaViewRectTransform)
        {
            this.client.RegisterGameObjectsForInteraction(gameObject,adViewRectTransform,mediaViewRectTransform, iconViewRectTransform,ctaViewRectTransform);
        }

        #region IYumiNativeClient event
        // Ad event fired when the native ad has been received.
        public event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;
        // Ad event fired when the native ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the native ad is click.
        public event EventHandler<EventArgs> OnAdClick;
        #endregion
        //回调
        private void ConfigureNativeEvents(){
            this.client.OnNativeAdLoaded += (sender, args) =>
            {
                if (this.OnNativeAdLoaded != null)
                {
                    this.OnNativeAdLoaded(this, args);
                }
            };
            this.client.OnAdFailedToLoad += (sender, args) => { 
                if(this.OnAdFailedToLoad != null){
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
