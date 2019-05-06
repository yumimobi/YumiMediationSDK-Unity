using System;
using YumiMediationSDK.Api;

namespace YumiMediationSDK.Common
{
    public interface IYumiRewardVideoClient
    {
        // Ad event fired when the reward based video ad has been received.
        event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when  the reward based video ad has failed to load.
        event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when  the reward based video ad has failed to show.
        event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        // Ad event fired when the reward based video ad is opened.
        event EventHandler<EventArgs> OnAdOpening;
        // Ad event fired when the reward based video ad has started playing.
        event EventHandler<EventArgs> OnAdStartPlaying;
        // Ad event fired when the reward based video ad has rewarded the user.
        event EventHandler<EventArgs> OnAdRewarded;
        // Ad event fired when the reward based video ad is closed.
        event EventHandler<YumiAdCloseEventArgs> OnAdClosed;
        // Ad event fired when the reward based video ad is clicked.
        event EventHandler<EventArgs> OnAdClicked;

        // Creates an RewardVideo.
        void CreateRewardVideoAd();
        // load RewardVideo
        void LoadRewardVideoAd(string placementId, string channelId, string versionId);

        // Determines whether the interstitial has loaded.
        bool IsRewardVideoReady();

        // Shows the RewardVideo.
        void PlayRewardVideo();

        // Destroys an RewardVideo.
        void DestroyRewardVideo();
    }
}
