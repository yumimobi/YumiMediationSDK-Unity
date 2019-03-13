using System;
using YumiMediationSDK.Common;
using System.Reflection;

namespace YumiMediationSDK.Api
{
    public class YumiRewardVideoAd
    {
        private IYumiRewardVideoClient client;


        private static readonly YumiRewardVideoAd instance = new YumiRewardVideoAd();

        public static YumiRewardVideoAd Instance
        {
            get
            {
                return instance;
            }
        }

        // 
        public YumiRewardVideoAd(){
            Type yumiAdsClientFactory = Type.GetType(
                "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildRewardVideoClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiRewardVideoClient)method.Invoke(null, null);
            client.CreateRewardVideoAd();

            ConfigureRewardVideoEvents();
        }
        //Creates a Singleton YumiRewardVideoAd.
        public void LoadRewardVideoAd(string placementId, string channelId, string versionId)
        {
            this.client.LoadRewardVideoAd(placementId,channelId,versionId);
        }

        // Determines whether the RewardVideo has loaded.
        public bool IsRewardVideoReady()
        {
            return this.client.IsRewardVideoReady();
        }

        // play the RewardVideo.
        public void PlayRewardVideo()
        {
            this.client.PlayRewardVideo();
        }

        // Destroys an RewardVideo.
        public void DestroyRewardVideo()
        {
            this.client.DestroyRewardVideo();
        }

        // Ad event fired when the reward based video ad is opened.
        public event EventHandler<EventArgs> OnAdOpening;
        // Ad event fired when the reward based video ad has started playing.
        public event EventHandler<EventArgs> OnAdStartPlaying;
        // Ad event fired when the reward based video ad has rewarded the user.
        public event EventHandler<EventArgs> OnAdRewarded;
        // Ad event fired when the reward based video ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;

        private void ConfigureRewardVideoEvents()
        {
            this.client.OnAdOpening += (sender, args) =>
            {
                if (this.OnAdOpening != null)
                {
                    this.OnAdOpening(this, args);
                }
            };

            this.client.OnAdStartPlaying += (sender, args) =>
            {
                if (this.OnAdStartPlaying != null)
                {
                    this.OnAdStartPlaying(this, args);
                }
            };

            this.client.OnAdRewarded += (sender, args) =>
            {
                if (this.OnAdRewarded != null)
                {
                    this.OnAdRewarded(this, args);
                }
            };
            this.client.OnAdClosed += (sender, args) =>
            {
                if (this.OnAdClosed != null)
                {
                    this.OnAdClosed(this, args);
                }
            };


        }

    }
}
