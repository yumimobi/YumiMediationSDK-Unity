using System;
namespace YumiMediationSDK.Api
{
    public class YumiBannerViewOptions
    {
        // banner position in supview
        public YumiAdPosition adPosition { get; private set; }
        public YumiBannerAdSize bannerSize { get; private set; }
        // If isSmart is set to YES, it will render screen-width banner ads on any screen size across different devices in either orientation.
        public bool isSmart { get; private set; }
        // Disable auto refresh for the YumiMediationBannerView instance.
        public bool disableAutoRefresh { get; private set; }

        internal YumiBannerViewOptions(YumiBannerViewOptionsBuilder builder)
        {
            adPosition = builder.AdPosition;
            bannerSize = builder.BannerSize;
            isSmart = builder.IsSmart;
            disableAutoRefresh = builder.DisableAutoRefresh;
        }
    }

    public class YumiBannerViewOptionsBuilder
    {
        public YumiBannerViewOptionsBuilder()
        {
            AdPosition = YumiAdPosition.BOTTOM;
            BannerSize = YumiBannerAdSize.YUMI_BANNER_AD_SIZE_320x50;
            IsSmart = true;
            DisableAutoRefresh = false;
        }
        internal YumiAdPosition AdPosition { get; private set; }
        internal YumiBannerAdSize BannerSize { get; private set; }
        internal bool IsSmart { get; private set; }
        internal bool DisableAutoRefresh { get; private set; }

        public YumiBannerViewOptions Build()
        {
            return new YumiBannerViewOptions(this);
        }

        public YumiBannerViewOptionsBuilder setAdPosition(YumiAdPosition adPosition)
        {
            AdPosition = adPosition;
            return this;
        }
        public YumiBannerViewOptionsBuilder setBannerSize(YumiBannerAdSize bannerSize)
        {
            BannerSize = bannerSize;
            return this;
        }
        public YumiBannerViewOptionsBuilder setSmartState(bool isSmart)
        {
            IsSmart = isSmart;
            return this;
        }
        public YumiBannerViewOptionsBuilder setDisableAutoRefreshState(bool disableAutoRefreshState)
        {
            DisableAutoRefresh = disableAutoRefreshState;
            return this;
        }

    }

    public enum YumiBannerAdSize
    {
        /// iPhone and iPod Touch ad size. Typically 320x50.
        YUMI_BANNER_AD_SIZE_320x50,
        /// Leaderboard size for the iPad. Typically 728x90.
        YUMI_BANNER_AD_SIZE_728x90,
        /// Represents the fixed banner ad size - 300pt by 250pt.
        YUMI_BANNER_AD_SIZE_300x250,
        /// An ad size that spans the full width of the application in portrait orientation. The height is
        /// typically 50 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        YUMI_BANNER_AD_SIZE_SMART_PORTRAIT,
        /// An ad size that spans the full width of the application in landscape orientation. The height is
        /// typically 32 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        YUMI_BANNER_AD_SIZE_SMART_LANDSCAPE
    }

    public enum YumiAdPosition
    {
        TOP = 0,
        BOTTOM = 1,
    }
}
