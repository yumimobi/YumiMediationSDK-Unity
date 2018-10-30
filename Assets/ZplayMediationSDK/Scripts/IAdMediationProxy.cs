using System;

namespace Assets.ZplayMediationSDK.Scripts
{
    public interface IAdMediationProxy
    {
        event Action<Boolean> RewardedVideoFinished;

        void Initialize(string gameVersion, string channelId, string rewardedVideoSlotId, string interstitialsSlotId, string bannerSlotId);
        Boolean IsRewardedVideoAvailable();
        Boolean IsInterstitialAvailable();
        Boolean IsBannerAvailable();
        void FetchInterstitial();
        void FetchRewardedVideo();
        void ShowInterstitial();
        void ShowRewardedVideo();
        void ShowBanner(Boolean AutomaticAdaption = true);
        void HideBanner();
        void ShowTestSuit();
    }
}