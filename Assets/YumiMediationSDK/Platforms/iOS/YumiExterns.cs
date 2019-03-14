#if UNITY_IOS
using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace YumiMediationSDK.iOS
{
    internal enum YumiMediationAdViewBannerSize
    {

        /// iPhone and iPod Touch ad size. Typically 320x50.
        kYumiMediationAdViewBanner320x50 = 1 << 0,
        // Leaderboard size for the iPad. Typically 728x90.
        kYumiMediationAdViewBanner728x90 = 1 << 1,
        // Represents the fixed banner ad size - 300pt by 250pt.
        kYumiMediationAdViewBanner300x250 = 1 << 2,
        /// An ad size that spans the full width of the application in portrait orientation. The height is
        /// typically 50 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        kYumiMediationAdViewSmartBannerPortrait = 1 << 3,
        /// An ad size that spans the full width of the application in landscape orientation. The height is
        /// typically 32 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        kYumiMediationAdViewSmartBannerLandscape = 1 << 4

    }
    public class YumiExterns
    {
        #region banner

        [DllImport("__Internal")]
        internal static extern void YumiRelease(IntPtr obj);

        // banner
        [DllImport("__Internal")]
        internal static extern IntPtr InitYumiBannerAd(IntPtr bannerView, string placementID, string channelID, string versionID, int position);
        [DllImport("__Internal")]
        internal static extern void RequestBannerAd(IntPtr bannerView, bool isSmartBanner);
        [DllImport("__Internal")]
        internal static extern void ShowBannerView(IntPtr bannerView);
        [DllImport("__Internal")]
        internal static extern void HideBannerView(IntPtr bannerView);
        [DllImport("__Internal")]
        internal static extern void DestroyBannerView(IntPtr bannerView);
        [DllImport("__Internal")]
        internal static extern void SetBannerAdSize(IntPtr bannerView, YumiMediationAdViewBannerSize bannerSize);
        [DllImport("__Internal")]
        internal static extern void DisableAutoRefresh(IntPtr bannerView);
        [DllImport("__Internal")]
        internal static extern void SetBannerCallbacks(
           IntPtr bannerView,
           YumiBannerClient.YumiBannerDidReceiveAdCallback adReceivedCallback,
           YumiBannerClient.YumiBannerDidFailToReceiveAdWithErrorCallback adFailedCallback,
           YumiBannerClient.YumiBannerDidClickCallback adClickedCallback);

        #endregion

        #region interstitial 
        // interstital
        [DllImport("__Internal")]
        internal static extern IntPtr InitYumiInterstitial(IntPtr interstitial, string placementID, string channelID, string versionID);
        [DllImport("__Internal")]
        internal static extern bool IsInterstitialReady(IntPtr interstitia);
        [DllImport("__Internal")]
        internal static extern void PresentInterstitial(IntPtr interstitia);
        [DllImport("__Internal")]
        internal static extern void SetInterstitiaCallbacks(
           IntPtr interstitia,
            YumiInterstitialClient.YumiInterstitialDidReceiveAdCallback adReceivedCallback,
            YumiInterstitialClient.YumiInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback,
            YumiInterstitialClient.YumiInterstitialDidClickCallback adClickedCallback,
            YumiInterstitialClient.YumiInterstitialDidCloseCallback adClosedCallback);

        #endregion

        #region RewardVideo 
        // create RewardVideo single obj
        [DllImport("__Internal")]
        internal static extern IntPtr CreateYumiRewardVideo(IntPtr rewardVideo);

        [DllImport("__Internal")]
        internal static extern void LoadYumiRewardVideo(IntPtr rewardVideo, string placementID, string channelID, string versionID);
        [DllImport("__Internal")]
        internal static extern bool IsRewardVideoReady(IntPtr rewardVideo);
        [DllImport("__Internal")]
        internal static extern void PlayRewardVideo(IntPtr rewardVideo);
        [DllImport("__Internal")]
        internal static extern void SetRewardVideoCallbacks(
           IntPtr rewardVideo,
            YumiRewardVideoClient.YumiRewardVideoDidOpenAdCallback adOpenedCallback,
            YumiRewardVideoClient.YumiRewardVideoDidStartPlayingCallback adStartPlayingCallback,
            YumiRewardVideoClient.YumiRewardVideoDidRewardCallback adRewardCallback,
            YumiRewardVideoClient.YumiRewardVideoDidCloseCallback adClosedCallback);

        #endregion
        #region Debugcenter 
        // call debugcenter
        [DllImport("__Internal")]
        internal static extern void PresentDebugCenter(string bannerPlacementID, string interstitialPlacementID, string videoPlacementID, string nativePlacementID, string channelID, string versionID);

        [DllImport("__Internal")]
        internal static extern void EnableTestMode();

        #endregion
        #region native ad
        [DllImport("__Internal")]
        internal static extern IntPtr InitYumiNativeAd(IntPtr nativeAd, string placementID, string channelID, string versionID, int preferredAdChoicesPosition, int preferredAdAttributionPosition, string preferredAdAttributionText,uint preferredAdAttributionTextColor,uint preferredAdAttributionTextBackgroundColor, int preferredAdAttributionTextFontSize, bool hideAdAttribution);
       
        [DllImport("__Internal")]
        internal static extern void RequestNativeAd(IntPtr nativeAd, int adCount);
        [DllImport("__Internal")]
        internal static extern void RegisterAssetViewsForInteraction(
                IntPtr nativeAd,string uniqueId,
                int adViewX, int adViewY, int adViewWidth, int adViewHeight,
                int mediaViewX, int mediaViewY, int mediaViewWidth, int mediaViewHeight,
                int iconViewX, int iconViewY, int iconViewWidth, int iconViewHeight,
                int ctaViewX, int ctaViewY, int ctaViewWidth, int ctaViewHeight,
                int titleX, int titleY, int titleWidth, int titleHeight,
                int descX, int descY, int descWidth, int descHeight);

        [DllImport("__Internal")]
        internal static extern void UnregisterView(IntPtr nativeA,string uniqueId);

        [DllImport("__Internal")]
        internal static extern bool IsAdInvalidated(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern void ShowView(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern void HideView(IntPtr nativeA, string uniqueId);

        //get native data value
        [DllImport("__Internal")]
        internal static extern string YumiNativeAdBridgeGetTitle(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern string YumiNativeAdBridgeGetDesc(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern string YumiNativeAdBridgeGetIconUrl(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern string YumiNativeAdBridgeGetCoverImageURL(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern string YumiNativeAdBridgeGetCallToAction(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern string YumiNativeAdBridgeGetPrice(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern string YumiNativeAdBridgeGetStarRating(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern string YumiNativeAdBridgeGetOther(IntPtr nativeA, string uniqueId);
        [DllImport("__Internal")]
        internal static extern bool   YumiNativeAdBridgeHasVideoContent(IntPtr nativeA, string uniqueId);
        //render ad view style
        [DllImport("__Internal")]
        internal static extern void RenderingTitleText(IntPtr nativeA, uint textColor, uint textBgColor, int fontSize);
        [DllImport("__Internal")]
        internal static extern void RenderingDescText(IntPtr nativeA, uint textColor, uint textBgColor, int fontSize);
        [DllImport("__Internal")]
        internal static extern void RenderingCallToActionText(IntPtr nativeA, uint textColor, uint textBgColor, int fontSize);
        [DllImport("__Internal")]
        internal static extern void RenderingIconScaleType(IntPtr nativeA, int scaleType);
        [DllImport("__Internal")]
        internal static extern void RenderingCoverImageScaleType(IntPtr nativeA, int scaleType);
   
        [DllImport("__Internal")]
        internal static extern void SetNativeCallbacks(
          IntPtr nativeAd,
            YumiNativeClient.YumiNativeAdDidReceiveAdCallback adReceivedCallback,
            YumiNativeClient.YumiNativeAdDidFailToReceiveAdWithErrorCallback adFailedCallback,
            YumiNativeClient.YumiNativeAdDidClickCallback adClickedCallback
         );

        #endregion
    }
}

#endif