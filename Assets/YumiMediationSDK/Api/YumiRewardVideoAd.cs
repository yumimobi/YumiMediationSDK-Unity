using System;
using YumiMediationSDK.Common;
using System.Reflection;

namespace YumiMediationSDK.Api
{
    public class YumiRewardVideoAd
    {
        private IYumiRewardVideoClient client;


        private static readonly YumiRewardVideoAd instance = new YumiRewardVideoAd();
        /// <summary>
        /// single YumiRewardVideoAd instance.
        /// </summary>
        /// <value>The instance.</value>
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
        /// <summary>
        /// Initiates the ad request, should only be called once as early as possible.
        /// </summary>
        /// <param name="placementId">Placement identifier.</param>
        /// <param name="channelId">Channel identifier.</param>
        /// <param name="versionId">Version identifier.</param>
        public void LoadAd(string placementId, string channelId, string versionId)
        {
            this.client.LoadRewardVideoAd(placementId,channelId,versionId);
        }

        /// <summary>
        /// Determines whether the RewardVideo has loaded.
        /// </summary>
        /// <returns><c>true</c>, if RewardVideo has loaded, <c>false</c> otherwise.</returns>
        public bool IsReady()
        {
            return this.client.IsRewardVideoReady();
        }

        /// <summary>
        /// play the RewardVideo.
        /// </summary>
        public void Play()
        {
            this.client.PlayRewardVideo();
        }

        /// <summary>
        /// Occurs  when the reward based video ad has been received.
        /// </summary>
        public event EventHandler<EventArgs> OnAdLoaded;
        /// <summary>
        /// Occurs  when the reward based video ad has failed to load.
        /// </summary>
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        /// <summary>
        /// Occurs when the reward based video ad has failed to show.
        /// </summary>
        public event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        /// <summary>
        /// Occurs  when the reward based video ad is opened.
        /// </summary>
        public event EventHandler<EventArgs> OnAdOpening;
        /// <summary>
        /// Occurs when the reward based video ad has started playing.
        /// </summary>
        public event EventHandler<EventArgs> OnAdStartPlaying;
        /// <summary>
        /// Occurs when the reward based video ad has rewarded the user.
        /// </summary>
        public event EventHandler<EventArgs> OnAdRewarded;
        /// <summary>
        /// Occurs when the reward based video ad is closed.
        /// </summary>
        public event EventHandler<YumiAdCloseEventArgs> OnRewardVideoAdClosed;
        /// <summary>
        /// Occurs  when the reward based video ad is clicked.
        /// </summary>
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
            this.client.OnRewardVideoAdClosed += (sender, args) =>
            {
                if (this.OnRewardVideoAdClosed != null)
                {
                    this.OnRewardVideoAdClosed(this, args);
                }
            };


        }

        [Obsolete("Destroy is deprecated.", true)]
        //Destroys an RewardVideo.
        public void Destroy()
        {
            this.client.DestroyRewardVideo();
        }

        [Obsolete("OnAdClosed is deprecated.", true)]
        // Ad event fired when the reward based video ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;
    }
}
