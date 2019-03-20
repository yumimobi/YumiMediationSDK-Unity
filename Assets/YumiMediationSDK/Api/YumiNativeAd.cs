using System;
using YumiMediationSDK.Common;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace YumiMediationSDK.Api
{
    public class YumiNativeAd
    {
        private IYumiNativeClient client;
        private YumiNativeAdOptions options;
        // Creates an nativeAd
        public YumiNativeAd(string placementId, string channelId, string versionId, YumiNativeAdOptions options)
        {
            client = YumiAdsClientFactory.BuildNativeClient();

            client.CreateNativeAd(placementId, channelId, versionId, options);

            ConfigureNativeEvents();
        }
        // Loads native ad
        public void LoadAd(int adCount)
        {
            this.client.LoadAd(adCount);
        }

        // Destroys native ad object.
        public void Destroy()
        {
            this.client.DestroyNativeAd();

        }
        // This is a method to associate a YumiNativeData with the ad assets gameobject you will use to display the native ads.
        public void RegisterGameObjectsForInteraction(YumiNativeData data, GameObject gameObject, Dictionary<NativeElemetType, Transform> elements)
        {
            if (gameObject == null)
            {
                Logger.Log("GameObject cannot be null.");
                return;
            }

            if (elements == null)
            {
                Logger.Log("Native Elements transform Dictionary cannot be null.");
                return;
            }

            if (elements[NativeElemetType.PANEL] == null ||
               elements[NativeElemetType.TITLE] == null ||
               elements[NativeElemetType.ICON] == null ||
               elements[NativeElemetType.COVER_IMAGE] == null ||
               elements[NativeElemetType.CALL_TO_ACTION] == null)
            {
                Logger.Log("The follow elements are required: panel, title, icon, coverImage, callToAction");
                return;
            }

            client.RegisterGameObjectsForInteraction(data, gameObject, elements);
        }
        // Determines whether nativeAd data is invalidated, if invalidated please reload
        public bool IsAdInvalidated(YumiNativeData nativeData)
        {
            return client.IsAdInvalidated(nativeData);
        }
        // Show nativeAd data associate view 
        public void ShowView(YumiNativeData nativeData)
        {
            client.ShowView(nativeData);
        }
        // Hide nativeAd data associate view 
        public void HideView(YumiNativeData nativeData)
        {
            client.HideView(nativeData);
        }
        // Unregister nativeAd data associate view,remove it from supview
        public void UnregisterView(YumiNativeData nativeData)
        {
            this.client.UnregisterView(nativeData);
        }

        #region IYumiNativeClient event
        // Ad event fired when the native ad has loaded.
        public event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;
        // Ad event fired when the native ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the native ad is click.
        public event EventHandler<EventArgs> OnAdClick;
        #endregion

        private void ConfigureNativeEvents()
        {
            this.client.OnNativeAdLoaded += (sender, args) =>
            {
                if (this.OnNativeAdLoaded != null)
                {
                    this.OnNativeAdLoaded(this, args);
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
