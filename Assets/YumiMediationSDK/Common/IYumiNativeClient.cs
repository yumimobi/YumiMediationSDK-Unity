using System;
using YumiMediationSDK.Api;

namespace YumiMediationSDK.Common
{
    public interface IYumiNativeClient
    {
        // Ad event fired when the native ad has been received.
        event EventHandler<YumiNativeToLoadEventArgs> OnAdLoaded;
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
    }
}
