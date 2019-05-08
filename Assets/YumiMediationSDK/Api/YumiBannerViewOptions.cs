using System;
namespace YumiMediationSDK.Api
{
    public class YumiBannerViewOptions
    {
        /// <summary>
        ///  banner position in supview
        /// </summary>
        /// <value>The ad position.</value>
        public YumiAdPosition adPosition { get; private set; }
        /// <summary>
        /// Gets the size of the banner.
        /// </summary>
        /// <value>The size of the banner.</value>
        public YumiBannerAdSize bannerSize { get; private set; }
        /// <summary>
        /// If isSmart is set to YES, it will render screen-width banner ads on any screen size across different devices in either orientation.
        /// </summary>
        /// <value><c>true</c> if is smart; otherwise, <c>false</c>.</value>
        public bool isSmart { get; private set; }
        /// <summary>
        /// Disable auto refresh for the YumiMediationBannerView instance.
        /// </summary>
        /// <value><c>true</c> if disable auto refresh; otherwise, <c>false</c>.</value>
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
        /// <summary>
        /// Build default instance.
        /// </summary>
        /// <returns>Builder instance.</returns>
        public YumiBannerViewOptions Build()
        {
            return new YumiBannerViewOptions(this);
        }
        /// <summary>
        /// Sets the banner ad position.
        /// </summary>
        /// <returns>Builder instance.</returns>
        /// <param name="adPosition">Ad position.</param>
        public YumiBannerViewOptionsBuilder setAdPosition(YumiAdPosition adPosition)
        {
            AdPosition = adPosition;
            return this;
        }
        /// <summary>
        /// Sets the size of the banner.
        /// </summary>
        /// <returns>Builder instance.</returns>
        /// <param name="bannerSize">Banner size.</param>
        public YumiBannerViewOptionsBuilder setBannerSize(YumiBannerAdSize bannerSize)
        {
            BannerSize = bannerSize;
            return this;
        }
        /// <summary>
        /// Sets the state of the smart.
        /// </summary>
        /// <returns>Builder instance.</returns>
        /// <param name="isSmart">If set to <c>true</c> is smart.</param>
        public YumiBannerViewOptionsBuilder setSmartState(bool isSmart)
        {
            IsSmart = isSmart;
            return this;
        }
        /// <summary>
        /// Sets the banner state of the disable auto refresh.
        /// </summary>
        /// <returns>Builder instance</returns>
        /// <param name="disableAutoRefreshState">If set to <c>true</c> disable auto refresh state.</param>
        public YumiBannerViewOptionsBuilder setDisableAutoRefreshState(bool disableAutoRefreshState)
        {
            DisableAutoRefresh = disableAutoRefreshState;
            return this;
        }

    }

    public enum YumiBannerAdSize
    {
        /// <summary>
        /// iPhone and iPod Touch ad size. Typically 320x50.
        /// </summary>
        YUMI_BANNER_AD_SIZE_320x50,
        /// <summary>
        /// Leaderboard size for the iPad. Typically 728x90.
        /// </summary>
        YUMI_BANNER_AD_SIZE_728x90,
        /// <summary>
        /// Represents the fixed banner ad size - 300pt by 250pt.
        /// </summary>
        YUMI_BANNER_AD_SIZE_300x250,
        /// <summary>
        /// An ad size that spans the full width of the application in portrait orientation. The height is
        /// typically 50 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        /// </summary>
        YUMI_BANNER_AD_SIZE_SMART_PORTRAIT,
        /// <summary>
        ///  An ad size that spans the full width of the application in landscape orientation. The height is
        /// typically 32 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        /// </summary>
        YUMI_BANNER_AD_SIZE_SMART_LANDSCAPE
    }

    public enum YumiAdPosition
    {
        TOP = 0,
        BOTTOM = 1,
    }
}
