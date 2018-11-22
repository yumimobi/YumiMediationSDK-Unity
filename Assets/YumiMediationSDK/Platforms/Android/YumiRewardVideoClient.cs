#if UNITY_ANDROID

using System;
using YumiMediationSDK.Common;
using UnityEngine;

namespace YumiMediationSDK.Android
{
    public class YumiRewardVideoClient : AndroidJavaProxy, IYumiRewardVideoClient
    {
        private AndroidJavaObject rewardVideo;
        public YumiRewardVideoClient() : base(YumiUtils.UnityRewardVideoAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(YumiUtils.UnityActivityClassName);
            AndroidJavaObject activity =
                    playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            this.rewardVideo = new AndroidJavaObject(
                YumiUtils.RewardVideoClassName, activity, this);
        }


        // Ad event fired when the reward based video ad is opened.
        public event EventHandler<EventArgs> OnAdOpening;
        // Ad event fired when the reward based video ad has started playing.
        public event EventHandler<EventArgs> OnAdStartPlaying;
        // Ad event fired when the reward based video ad has rewarded the user.
        public event EventHandler<EventArgs> OnAdRewarded;
        // Ad event fired when the reward based video ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;

        #region implement IYumiRewardVideoClient 

        // Creates an RewardVideo.
        public void CreateRewardVideoAd()
        {
            this.rewardVideo.Call("create");
        }
        // load RewardVideo
        public void LoadRewardVideoAd(string placementId, string channelId, string versionId)
        {

            this.rewardVideo.Call(
                   "requestRewardVideoAd",
               new object[3] { placementId, channelId, versionId });
        }

        // Determines whether the interstitial has loaded.
        public bool IsRewardVideoReady()
        {
            return this.rewardVideo.Call<bool>("isReady");
        }

        // Shows the RewardVideo.
        public void PlayRewardVideo()
        {
            this.rewardVideo.Call("playRewardVideo");
        }

        // Destroys an RewardVideo.
        public void DestroyRewardVideo()
        {
            this.rewardVideo.Call("destroyRewardVideo");
        }

        #endregion

        #region UnityRewardVideoAdListener call back 

        public void onAdOpening()
        {
            if (this.OnAdOpening != null)
            {
                this.OnAdOpening(this, EventArgs.Empty);
            }
        }
        public void onAdStartPlaying()
        {
            if (this.OnAdStartPlaying != null)
            {
                this.OnAdStartPlaying(this, EventArgs.Empty);
            }
        }
        public void onAdRewarded()
        {
            if (this.OnAdRewarded != null)
            {
                this.OnAdRewarded(this, EventArgs.Empty);
            }
        }
        public void onAdClosed()
        {
            if (this.OnAdClosed != null)
            {
                this.OnAdClosed(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
#endif