using System;
using YumiMediationSDK.Common;
using YumiMediationSDK.Api;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace YumiMediationSDK.iOS
{
    public class YumiNativeClient : IYumiNativeClient
    {

        private IntPtr nativeAdPtr;

        private IntPtr nativeClientPtr;

        #region Banner callback types

        internal delegate void YumiNativeAdDidReceiveAdCallback(IntPtr nativeClient, List<YumiNativeData> nativeAds);

        internal delegate void YumiNativeAdDidFailToReceiveAdWithErrorCallback(
                IntPtr nativeClient, string error);

        internal delegate void YumiNativeAdDidClickCallback(IntPtr nativeClient);

        #endregion

        // This property should be used when setting the bannerViewPtr.
        private IntPtr NativeAdPtr
        {
            get
            {
                return this.nativeAdPtr;
            }

            set
            {
                YumiExterns.YumiRelease(this.nativeAdPtr); // clear cache ,if existed
                this.nativeAdPtr = value;
            }
        }


        #region IYumiNativeClient event
        // Ad event fired when the native ad has been received.
        public event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;
        // Ad event fired when the native ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the native ad is click.
        public event EventHandler<EventArgs> OnAdClick;
        #endregion

        #region implement IYumiNativeClient interface 

        // Creates a native ad
        public void CreateNativeAd(string placementId, string channelId, string versionId)
        {
            this.nativeClientPtr = (IntPtr)GCHandle.Alloc(this);
            this.NativeAdPtr = YumiExterns.InitYumiNativeAd(this.nativeClientPtr, placementId, channelId, versionId);

            YumiExterns.SetNativeCallbacks(
                this.NativeAdPtr,
                NativeDidReceiveAdCallback,
                NativeDidFailToReceiveAdWithErrorCallback,
                NativeDidClickCallback);
        }

        // Begins loading the YumiMediationNativeAd with the count you wanted.
        public void LoadAd(int adCount)
        {
            YumiExterns.RequestNativeAd(this.NativeAdPtr, adCount);
        }

        //report Impression
        public void ReportImpression(YumiNativeData nativeData)
        {
            YumiExterns.ReportImpression(this.NativeAdPtr);
        }
        //report click
        public void ReportClick(YumiNativeData nativeData)
        {
            YumiExterns.ReportClick(this.NativeAdPtr);
        }
        // Destroys native ad object.
        public void DestroyNativeAd()
        {
            this.NativeAdPtr = IntPtr.Zero;
        }
        public void Dispose()
        {
            this.DestroyNativeAd();
            ((GCHandle)this.nativeClientPtr).Free();
        }

        ~YumiNativeClient()
        {
            this.Dispose();
        }
        #endregion

        #region  native ad  callback methods

        [MonoPInvokeCallback(typeof(YumiNativeAdDidReceiveAdCallback))]
        private static void NativeDidReceiveAdCallback(IntPtr nativeClient, List<YumiNativeData> nativeAds)
        {
            YumiNativeClient client = IntPtrToNativeClient(nativeClient);
            if (client.OnNativeAdLoaded != null)
            {
                YumiNativeToLoadEventArgs args = new YumiNativeToLoadEventArgs()
                {
                    nativeData = nativeAds
                };

                client.OnNativeAdLoaded(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(YumiNativeAdDidFailToReceiveAdWithErrorCallback))]
        private static void NativeDidFailToReceiveAdWithErrorCallback(IntPtr nativeClient, string error)
        {
            YumiNativeClient client = IntPtrToNativeClient(nativeClient);
            if (client.OnAdFailedToLoad != null)
            {
                YumiAdFailedToLoadEventArgs args = new YumiAdFailedToLoadEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(YumiNativeAdDidClickCallback))]
        private static void NativeDidClickCallback(IntPtr nativeClient)
        {
            YumiNativeClient client = IntPtrToNativeClient(nativeClient);
            if (client.OnAdClick != null)
            {
                client.OnAdClick(client, EventArgs.Empty);
            }
        }


        private static YumiNativeClient IntPtrToNativeClient(IntPtr nativeClient)
        {
            GCHandle handle = (GCHandle)nativeClient;
            return handle.Target as YumiNativeClient;
        }

        #endregion

    }
}
