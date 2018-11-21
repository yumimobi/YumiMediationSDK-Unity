using System;
using YumiMediationSDK.Common;
using System.Runtime.InteropServices;
#if UNITY_IOS
namespace YumiMediationSDK.iOS
{
    public class YumiRewardVideoClient : IYumiRewardVideoClient
    {

        IntPtr rewardVideoClientPtr;
        IntPtr rewardVideoPtr;

        #region YumiRewardVideoClient callback  types
        internal delegate void YumiRewardVideoDidOpenAdCallback(IntPtr rewardVideo);

        internal delegate void YumiRewardVideoDidStartPlayingCallback(IntPtr rewardVideo);

        internal delegate void YumiRewardVideoDidCloseCallback(IntPtr rewardVideo);

        internal delegate void YumiRewardVideoDidRewardCallback(IntPtr rewardVideo);

        #endregion

        // Ad event fired when the reward based video ad is opened.
        public event EventHandler<EventArgs> OnAdOpening;
        // Ad event fired when the reward based video ad has started playing.
        public event EventHandler<EventArgs> OnAdStartPlaying;
        // Ad event fired when the reward based video ad has rewarded the user.
        public event EventHandler<EventArgs> OnAdRewarded;
        // Ad event fired when the reward based video ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;

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
                RewardVideoDidCloseCallback
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
        private static void RewardVideoDidCloseCallback(IntPtr rewardVideo)
        {
            YumiRewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
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