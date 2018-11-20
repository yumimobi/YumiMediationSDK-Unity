using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace YumiMediationSDK.iOS
{
    public class YumiExterns
    {
        #region banner 

        public enum YumiMediationAdViewBannerSize
        {
            /// iPhone and iPod Touch ad size. Typically 320x50.
            kYumiMediationAdViewBanner320x50 = 1 << 0,
            // Leaderboard size for the iPad. Typically 728x90.
            kYumiMediationAdViewBanner728x90 = 1 << 1,
            // Represents the fixed banner ad size - 300pt by 250pt.
            kYumiMediationAdViewBanner300x250 = 1 << 2
        }

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
        internal static extern void Present(IntPtr interstitia);
        [DllImport("__Internal")]
        internal static extern void SetInterstitiaCallbacks(
           IntPtr interstitia,
            YumiInterstitialClient.YumiInterstitialDidReceiveAdCallback adReceivedCallback,
            YumiInterstitialClient.YumiInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback,
            YumiInterstitialClient.YumiInterstitialDidClickCallback adClickedCallback,
            YumiInterstitialClient.YumiInterstitialDidCloseCallback adClosedCallback);

        #endregion

    }
}
