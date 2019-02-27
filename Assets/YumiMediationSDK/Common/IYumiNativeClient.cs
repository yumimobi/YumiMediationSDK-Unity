using System;
using YumiMediationSDK.Api;
using UnityEngine;

namespace YumiMediationSDK.Common
{
    public interface IYumiNativeClient
    {
        // Ad event fired when the native ad has been received.
        event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;
        // Ad event fired when the native ad has failed to load.
        event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the native ad is click.
        event EventHandler<EventArgs> OnAdClick;

        // Creates a native ad
        void CreateNativeAd(string placementId, string channelId, string versionId);

        // Begins loading the YumiMediationNativeAd with the count you wanted.
        void LoadAd(int adCount);

        //report Impression
        void ReportImpression(YumiNativeData nativeData);
        //report click
        void ReportClick(YumiNativeData nativeData);
        // Destroys native ad object.
        void DestroyNativeAd();
        // Wire up GameObject with the native ad. The game object should be a child of the canvas.
        // Register game objects for interactions.
        // MediaView will be used for impression logging.
        // CallToActionButton will be used for click logging.
        void RegisterGameObjectsForInteraction(GameObject gameObject, RectTransform adViewRectTransform,RectTransform mediaViewRectTransform, RectTransform iconViewRectTransform, RectTransform ctaViewRectTransform);
    }
}
