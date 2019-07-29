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
        /// <summary>
        ///  Creates an nativeAd
        /// </summary>
        /// <param name="placementId">Placement identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="versionId">Version identifier.</param>
        /// <param name="gameObject">game object.</param>
        /// <param name="options">Options.</param>
        public YumiNativeAd(string placementId, string channelId, string versionId, GameObject gameObject ,YumiNativeAdOptions options)
        {

            if (gameObject == null)
            {
                Logger.Log("GameObject cannot be null.");
                return;
            }

            client = YumiAdsClientFactory.BuildNativeClient();

            client.CreateNativeAd(placementId, channelId, versionId, gameObject, options);

            ConfigureNativeEvents();
        }
        /// <summary>
        ///  Loads native ad
        /// </summary>
        /// <param name="adCount">Ad count.</param>
        public void LoadAd(int adCount)
        {
            this.client.LoadAd(adCount);
        }

        /// <summary>
        ///  Destroys native ad object.
        /// </summary>
        public void Destroy()
        {
            this.client.DestroyNativeAd();

        }
        /// <summary>
        /// This is a method to associate a YumiNativeData with the ad assets gameobject you will use to display the native ads.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="elements">Elements.</param>
        public void RegisterGameObjectsForInteraction(YumiNativeData data, Dictionary<NativeElemetType, Transform> elements)
        {
        
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

            client.RegisterGameObjectsForInteraction(data, elements);
        }
        /// <summary>
        ///  Determines whether nativeAd data is invalidated, if invalidated please reload
        /// </summary>
        /// <returns><c>true</c>, if ad invalidated was ised, <c>false</c> otherwise.</returns>
        /// <param name="nativeData">Native data.</param>
        public bool IsAdInvalidated(YumiNativeData nativeData)
        {
            return client.IsAdInvalidated(nativeData);
        }
        /// <summary>
        /// Show nativeAd data associate view 
        /// </summary>
        /// <param name="nativeData">Native data.</param>
        public void ShowView(YumiNativeData nativeData)
        {
            client.ShowView(nativeData);
        }
        /// <summary>
        ///  Hide nativeAd data associate view 
        /// </summary>
        /// <param name="nativeData">Native data.</param>
        public void HideView(YumiNativeData nativeData)
        {
            client.HideView(nativeData);
        }
        /// <summary>
        ///  Unregister nativeAd data associate view,remove it from supview
        /// </summary>
        /// <param name="nativeData">Native data.</param>
        public void UnregisterView(YumiNativeData nativeData)
        {
            this.client.UnregisterView(nativeData);
        }

        #region IYumiNativeClient event
        /// <summary>
        /// Occurs when the native ad has loaded.
        /// </summary>
        public event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;
        /// <summary>
        /// Occurswhen the native ad has failed to load.
        /// </summary>
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        /// <summary>
        /// Occurs when the native ad is click.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClick;

        /// ------only available in ExpressAdView------

        /// <summary>
        /// Ad event fired when the native  express ad has been successed.
        /// </summary>
        public event EventHandler<YumiNativeDataEventArgs> OnExpressAdRenderSuccess;
        /// <summary>
        /// Ad event fired when the native  express ad has been failed.
        /// </summary>
        public event EventHandler<YumiAdFailedToRenderEventArgs> OnExpressAdRenderFail;
        /// <summary>
        /// Ad event fired when the native  express ad has been click close button.
        /// </summary>
        public event EventHandler<YumiNativeDataEventArgs> OnExpressAdClickCloseButton;

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
            this.client.OnExpressAdRenderSuccess += (sender, args) =>
            {
                if (this.OnExpressAdRenderSuccess != null)
                {
                    this.OnExpressAdRenderSuccess(this, args);
                }
            };
            this.client.OnExpressAdRenderFail += (sender, args) =>
            {
                if (this.OnExpressAdRenderFail != null)
                {
                    this.OnExpressAdRenderFail(this, args);
                }
            };
            this.client.OnExpressAdClickCloseButton += (sender, args) =>
            {
                if (this.OnExpressAdClickCloseButton != null)
                {
                    this.OnExpressAdClickCloseButton(this, args);
                }
            };

        }
    }
}
