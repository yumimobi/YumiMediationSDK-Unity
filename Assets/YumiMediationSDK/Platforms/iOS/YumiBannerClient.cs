#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using YumiMediationSDK.Common;
using YumiMediationSDK.Api;


namespace YumiMediationSDK.iOS
{
    public class YumiBannerClient : IYumiBannerClient
    {

        private IntPtr bannerViewPtr;

        private IntPtr bannerClientPtr;

#region Banner callback types

        internal delegate void YumiBannerDidReceiveAdCallback(IntPtr bannerClient);

        internal delegate void YumiBannerDidFailToReceiveAdWithErrorCallback(
                IntPtr bannerClient, string error);

        internal delegate void YumiBannerDidClickCallback(IntPtr bannerClient);

#endregion

        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the banner ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the banner ad is click.
        public event EventHandler<EventArgs> OnAdClick;


        // This property should be used when setting the bannerViewPtr.
        private IntPtr BannerViewPtr
        {
            get
            {
                return this.bannerViewPtr;
            }

            set
            {
                YumiExterns.YumiRelease(this.bannerViewPtr); // clear cache ,if existed
                this.bannerViewPtr = value;
            }
        }


#region IYumiBannerClient implement 
        public void CreateBannerView(string placementId, string channelId, string versionId, YumiBannerViewOptions bannerOptions)
        {
            // A new GCHandle that protects the object from garbage collection. This GCHandle must be released with Free() when it is no longer needed.
            this.bannerClientPtr = (IntPtr)GCHandle.Alloc(this);

            this.BannerViewPtr = YumiExterns.InitYumiBannerAd(this.bannerClientPtr, placementId, channelId, versionId, (int)bannerOptions.adPosition);

            //config banner 
            if(bannerOptions.disableAutoRefresh)
            {
                YumiExterns.DisableAutoRefresh(BannerViewPtr);
            }

            YumiMediationAdViewBannerSize bannerSize = YumiMediationAdViewBannerSize.kYumiMediationAdViewBanner320x50;
            switch (bannerOptions.bannerSize)
            {
                case YumiBannerAdSize.YUMI_BANNER_AD_SIZE_320x50:
                    bannerSize = YumiMediationAdViewBannerSize.kYumiMediationAdViewBanner320x50;
                    break;
                case YumiBannerAdSize.YUMI_BANNER_AD_SIZE_728x90:
                    bannerSize = YumiMediationAdViewBannerSize.kYumiMediationAdViewBanner728x90;
                    break;
                case YumiBannerAdSize.YUMI_BANNER_AD_SIZE_300x250:
                    bannerSize = YumiMediationAdViewBannerSize.kYumiMediationAdViewBanner300x250;
                    break;
                case YumiBannerAdSize.YUMI_BANNER_AD_SIZE_SMART_PORTRAIT:
                    bannerSize = YumiMediationAdViewBannerSize.kYumiMediationAdViewSmartBannerPortrait;
                    break;
                case YumiBannerAdSize.YUMI_BANNER_AD_SIZE_SMART_LANDSCAPE:
                    bannerSize = YumiMediationAdViewBannerSize.kYumiMediationAdViewSmartBannerLandscape;
                    break;
            }

            YumiExterns.SetBannerAdSize(BannerViewPtr, bannerSize);
            YumiExterns.SetBannerCallbacks(
                this.BannerViewPtr,
                BannerDidReceiveAdCallback,
                BannerDidFailToReceiveAdWithErrorCallback,
                BannerDidClickCallback);

        }

        // Requests a new ad for the banner view.
        public void LoadAd(bool isSmart)
        {
            Logger.LogError("load ad");
            YumiExterns.RequestBannerAd(this.BannerViewPtr, isSmart);
        }

        // Shows the banner view on the screen.
        public void ShowBannerView()
        {
            YumiExterns.ShowBannerView(this.BannerViewPtr);
        }

        // Hides the banner view from the screen.
        public void HideBannerView()
        {
            YumiExterns.HideBannerView(this.BannerViewPtr);
        }

        // Destroys a banner view.
        public void DestroyBannerView()
        {

            YumiExterns.DestroyBannerView(this.BannerViewPtr);
            this.BannerViewPtr = IntPtr.Zero;
        }
        //dealloc
        public void Dispose()
        {
            this.DestroyBannerView();
            ((GCHandle)this.bannerClientPtr).Free();
        }

        ~YumiBannerClient()
        {
            this.Dispose();
        }
#endregion
#region Banner callback methods


        [MonoPInvokeCallback(typeof(YumiBannerDidReceiveAdCallback))]
        private static void BannerDidReceiveAdCallback(IntPtr bannerClient)
        {
            YumiBannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(YumiBannerDidFailToReceiveAdWithErrorCallback))]
        private static void BannerDidFailToReceiveAdWithErrorCallback(IntPtr bannerClient, string error)
        {
            YumiBannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdFailedToLoad != null)
            {
                YumiAdFailedToLoadEventArgs args = new YumiAdFailedToLoadEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(YumiBannerDidClickCallback))]
        private static void BannerDidClickCallback(IntPtr bannerClient)
        {
            YumiBannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdClick != null)
            {
                client.OnAdClick(client, EventArgs.Empty);
            }
        }


        private static YumiBannerClient IntPtrToBannerClient(IntPtr bannerClient)
        {
            GCHandle handle = (GCHandle)bannerClient;
            return handle.Target as YumiBannerClient;
        }
#endregion


    }
}

#endif