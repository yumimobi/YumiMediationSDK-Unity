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

        // Creates a Singleton YumiRewardVideoAd.
        private YumiRewardVideoAd(){
            Type yumiAdsClientFactory = Type.GetType(
                "YumiMediationSDK.YumiAdsClientFactory,Assembly-CSharp");
            MethodInfo method = yumiAdsClientFactory.GetMethod(
                "BuildRewardVideoClient",
                BindingFlags.Static | BindingFlags.Public);
            this.client = (IYumiRewardVideoClient)method.Invoke(null, null);
            client.CreateRewardVideoAd();

            ConfigureRewardVideoEvents();
        }
        // Initiates the ad request, should only be called once as early as possible.
        public void LoadAd(string placementId, string channelId, string versionId)
        {
            this.client.LoadRewardVideoAd(placementId,channelId,versionId);
        }

        // Determines whether the RewardVideo has loaded.
        public bool IsReady()
        {
            return this.client.IsRewardVideoReady();
        }

        // play the RewardVideo.
        public void Play()
        {
            this.client.PlayRewardVideo();
        }

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
        public event EventHandler<YumiAdCloseEventArgs> OnAdClosed;
        // Ad event fired when the reward based video ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;

        private void ConfigureRewardVideoEvents()
        {
            this.client.OnAdLoaded += (sender, args) =>
            {
                if (this.OnAdLoaded != null)
                {
                    this.OnAdLoaded(this, args);
                }
            };
            this.client.OnAdFailedToLoad += (sender, args) =>
            {
                if (this.OnAdFailedToLoad != null)
                {
                    this.OnAdFailedToLoad(this, args);
                }
            };
            this.client.OnAdFailedToShow += (sender, args) =>
            {
                if (this.OnAdFailedToShow != null)
                {
                    this.OnAdFailedToShow(this, args);
                }
            };
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
            this.client.OnAdClicked += (sender, args) =>
            {
                if (this.OnAdClicked != null)
                {
                    this.OnAdClicked(this, args);
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

        [Obsolete("Destroy is deprecated.", true)]
        //Destroys an RewardVideo.
        public void Destroy()
        {
            this.client.DestroyRewardVideo();
        }

    }
}
