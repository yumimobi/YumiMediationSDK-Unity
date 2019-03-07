using System;
using YumiMediationSDK.Api;
using UnityEngine;
using System.Collections.Generic;

namespace YumiMediationSDK.Common
{
    public interface IYumiNativeClient
    {
        /// <summary>
        /// Ad event fired when the native ad has been received.
        /// </summary>
        event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;

        /// <summary>
        /// Ad event fired when the native ad has failed to load.
        /// </summary>
        event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;

        /// <summary>
        /// Ad event fired when the native ad is click.
        /// </summary>
        event EventHandler<EventArgs> OnAdClick;

        /// <summary>
        /// Creates a native ad
        /// </summary>
        /// <param name="placementId">Placement identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="versionId">Version identifier.</param>
        void CreateNativeAd(string placementId, string channelId, string versionId);

        /// <summary>
        /// Begins loading the YumiMediationNativeAd with the count you wanted.
        /// </summary>
        /// <param name="adCount">Ad count.</param>
        void LoadAd(int adCount);

        /// <summary>
        /// Destroys native ad object.
        /// </summary>
        void DestroyNativeAd();

        /// <summary>
        /// Wire up GameObject with the native ad. The game object should be a child of the canvas.
        /// Register game objects for interactions.
        /// MediaView will be used for impression logging.
        /// CallToActionButton will be used for click logging.
        /// </summary>
        /// <param name="yumiNaitveData">Yumi naitve data.</param>
        /// <param name="gameObject">Game object.</param>
        /// <param name="elements">Elements.</param>
        void RegisterGameObjectsForInteraction(YumiNativeData yumiNaitveData, GameObject gameObject, Dictionary<NativeElemetType, Transform> elements);

        /// <summary>
        /// In case of not showing the ad immediately after the ad has been loaded, 
        /// the developer is responsible for checking whether or not the ad has been 
        /// invalidated. Once the ad is successfully loaded, the ad will be valid for 
        /// 60 mins. You will not get paid if you are showing an invalidated ad. You 
        /// should call isAdInvalidated() to validate the ad.
        /// </summary>
        /// <param name="nativeData">Native data.</param>
        bool IsAdInvalidated(YumiNativeData nativeData);

        /// <summary>
        /// Shows registered native ad view
        /// </summary>
        /// <param name="nativeData">Native data.</param>
        void ShowView(YumiNativeData nativeData);

        /// <summary>
        /// Hides the showing ad view. 
        /// </summary>
        /// <param name="nativeData">Native data.</param>
        void HideView(YumiNativeData nativeData);

        /// <summary>
        /// This is a method to disconnect a YumiNativeData with the view you used to display the native ads.
        /// </summary>
        /// <param name="nativeData">Native data.</param>
        void UnregisterView(YumiNativeData nativeData);
    }
}
