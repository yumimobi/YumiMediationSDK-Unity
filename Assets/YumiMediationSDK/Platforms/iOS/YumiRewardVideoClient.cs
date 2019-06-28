#if UNITY_IOS
using System;
using YumiMediationSDK.Common;
using System.Runtime.InteropServices;
using YumiMediationSDK.Api;

namespace YumiMediationSDK.iOS
{
    public class YumiRewardVideoClient : IYumiRewardVideoClient
    {

        IntPtr rewardVideoClientPtr;
        IntPtr rewardVideoPtr;

#region YumiRewardVideoClient callback  types
        internal delegate void YumiRewardVideoDidOpenAdCallback(IntPtr rewardVideo);

        internal delegate void YumiRewardVideoDidStartPlayingCallback(IntPtr rewardVideo);

        internal delegate void YumiRewardVideoDidCloseCallback(IntPtr rewardVideo,bool isRewarded);

        internal delegate void YumiRewardVideoDidRewardCallback(IntPtr rewardVideo);
        // 4.0.0
        internal delegate void YumiRewardVideoDidReceiveAdCallback(IntPtr interstitialClient);
        internal delegate void YumiRewardVideoDidFailToReceiveAdWithErrorCallback(
                IntPtr interstitialClient, string error);
        internal delegate void YumiRewardVideoDidFailToShowAdWithErrorCallback(
              IntPtr interstitialClient, string error);
        internal delegate void YumiRewardVideoDidClickAdCallback(IntPtr interstitialClient);

        #endregion

        // Ad event fired when the reward based video ad has been received.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when  the reward based video ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when  the reward based video ad has failed to show.
        public event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        // Ad event fired when the reward based video ad is opened.
        public event EventHandler<EventArgs> OnAdOpening;
        // Ad event fired when the reward based video ad has started playing.
        public event EventHandler<EventArgs> OnAdStartPlaying;
        // Ad event fired when the reward based video ad has rewarded the user.
        public event EventHandler<EventArgs> OnAdRewarded;
        // Ad event fired when the reward based video ad is closed.
        public event EventHandler<YumiAdCloseEventArgs> OnRewardVideoAdClosed;
        // Ad event fired when the reward based video ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;

        // This property should be used when setting the rewardBasedVideoPtr.
        private IntPtr RewardVideoPtr
        {
            get
            {
                return this.rewardVideoPtr;
            }

            set
            {
                YumiExterns.YumiRelease(this.rewardVideoPtr);
                this.rewardVideoPtr = value;
            }
        }


#region IYumiRewardVideoClient implement 

        // Creates an RewardVideo.
        public void CreateRewardVideoAd()
        {
            this.rewardVideoClientPtr = (IntPtr)GCHandle.Alloc(this);
            this.RewardVideoPtr = YumiExterns.CreateYumiRewardVideo(this.rewardVideoClientPtr);
            // call back 
            YumiExterns.SetRewardVideoCallbacks(
                this.RewardVideoPtr,
                RewardVideoDidOpenAdCallback,
                RewardVideoDidStartPlayingCallback,
                RewardVideoDidRewardCallback,
                RewardVideoDidCloseCallback,
                RewardVideoDidReceiveAdCallback,
                RewardVideoDidFailToReceiveAdWithErrorCallback,
                RewardVideoDidFailToShowAdWithErrorCallback,
                RewardVideoDidClickAdCallback
            );
        }

        public void LoadRewardVideoAd(string placementId, string channelId, string versionId)
        {
            YumiExterns.LoadYumiRewardVideo(this.RewardVideoPtr, placementId, channelId, versionId);
        }


        // Determines whether the interstitial has loaded.
        public bool IsRewardVideoReady()
        {
            return YumiExterns.IsRewardVideoReady(this.RewardVideoPtr);
        }

        // Shows the RewardVideo.
        public void PlayRewardVideo()
        {
            YumiExterns.PlayRewardVideo(this.RewardVideoPtr);
        }

        // Destroys an RewardVideo.
        public void DestroyRewardVideo()
        {
            this.RewardVideoPtr = IntPtr.Zero;
        }
        public void Dispose()
        {
            this.DestroyRewardVideo();
            ((GCHandle)this.rewardVideoClientPtr).Free();
        }

        ~YumiRewardVideoClient()
        {
            this.Dispose();
        }
#endregion

#region RewardVideo callback methods
        [MonoPInvokeCallback(typeof(YumiRewardVideoDidOpenAdCallback))]
        private static void RewardVideoDidOpenAdCallback(IntPtr rewardVideo)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdOpening != null)
            {
                client.OnAdOpening(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(YumiRewardVideoDidStartPlayingCallback))]
        private static void RewardVideoDidStartPlayingCallback(IntPtr rewardVideo)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdStartPlaying != null)
            {
                client.OnAdStartPlaying(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(YumiRewardVideoDidRewardCallback))]
        private static void RewardVideoDidRewardCallback(IntPtr rewardVideo)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdRewarded != null)
            {
                client.OnAdRewarded(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(YumiRewardVideoDidCloseCallback))]
        private static void RewardVideoDidCloseCallback(IntPtr rewardVideo,bool isRewarded)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnRewardVideoAdClosed != null)
            {
                 YumiAdCloseEventArgs args = new YumiAdCloseEventArgs()
                {
                    IsRewarded = isRewarded
                };
                client.OnRewardVideoAdClosed(client, args);
            }
        }
        // v4.0.0
        [MonoPInvokeCallback(typeof(YumiRewardVideoDidReceiveAdCallback))]
        private static void RewardVideoDidReceiveAdCallback(IntPtr rewardVideo)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdLoaded != null)
            {

                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(YumiRewardVideoDidFailToReceiveAdWithErrorCallback))]
        private static void RewardVideoDidFailToReceiveAdWithErrorCallback(IntPtr rewardVideo, string error)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdFailedToLoad != null)
            {
                YumiAdFailedToLoadEventArgs args = new YumiAdFailedToLoadEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(YumiRewardVideoDidFailToShowAdWithErrorCallback))]
        private static void RewardVideoDidFailToShowAdWithErrorCallback(IntPtr rewardVideo, string error)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdFailedToShow != null)
            {
                YumiAdFailedToShowEventArgs args = new YumiAdFailedToShowEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToShow(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(YumiRewardVideoDidClickAdCallback))]
        private static void RewardVideoDidClickAdCallback(IntPtr rewardVideo)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdClicked != null)
            {

                client.OnAdClicked(client, EventArgs.Empty);
            }
        }
        private static YumiRewardVideoClient IntPtrToRewardVideoClient(
           IntPtr rewardVideoClient)
        {
            GCHandle handle = (GCHandle)rewardVideoClient;
            return handle.Target as YumiRewardVideoClient;
        }
#endregion
    }
}
#endif