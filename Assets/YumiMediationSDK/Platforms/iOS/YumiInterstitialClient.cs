#if UNITY_IOS
using System;
using System.Collections.Generic;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;
using System.Runtime.InteropServices;


namespace YumiMediationSDK.iOS
{
    public class YumiInterstitialClient : IYumiInterstitialClient
    {

        private IntPtr interstitialPtr;
        private IntPtr interstitialClientPtr;

#region Interstitial callback types

        internal delegate void YumiInterstitialDidReceiveAdCallback(IntPtr interstitialClient);

        internal delegate void YumiInterstitialDidFailToReceiveAdWithErrorCallback(
                IntPtr interstitialClient, string error);

        internal delegate void YumiInterstitialDidCloseCallback(IntPtr interstitialClient);

        internal delegate void YumiInterstitialDidClickCallback(IntPtr interstitialClient);


#endregion
        // Ad event fired when the interstitial ad has been received.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the interstitial ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the interstitial ad has failed to show.
        public event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        // Ad event fired when the interstitial ad is opened.
        public event EventHandler<EventArgs> OnAdOpening;
        // Ad event fired when the interstitial ad has started playing.
        public event EventHandler<EventArgs> OnAdStartPlaying;
        // Ad event fired when the interstitial ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;
        // Ad event fired when the interstitial ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;

        // This property should be used when setting the interstitialPtr.
        private IntPtr InterstitialPtr
        {
            get
            {
                return this.interstitialPtr;
            }

            set
            {
                YumiExterns.YumiRelease(this.interstitialPtr);
                this.interstitialPtr = value;
            }
        }

#region IYumiInterstitialClient implement 
        public void CreateInterstitialAd(string placementId, string channelId, string versionId)
        {
            this.interstitialClientPtr = (IntPtr)GCHandle.Alloc(this);
            this.InterstitialPtr = YumiExterns.InitYumiInterstitial(this.interstitialClientPtr, placementId, channelId, versionId);

            YumiExterns.SetInterstitiaCallbacks(
                this.InterstitialPtr,
                InterstitialDidReceiveAdCallback,
                InterstitialDidFailToReceiveAdWithErrorCallback,
                InterstitialDidClickCallback,
                InterstitialDidCloseCallback);

        }

        // Determines whether the interstitial has loaded.
        public bool IsInterstitialReady()
        {

            return YumiExterns.IsInterstitialReady(this.InterstitialPtr);
        }

        // Shows the InterstitialAd.
        public void ShowInterstitial()
        {
            YumiExterns.PresentInterstitial(this.InterstitialPtr);
        }

        // Destroys an InterstitialAd.
        public void DestroyInterstitial()
        {
            this.InterstitialPtr = IntPtr.Zero;
        }

        public void Dispose()
        {
            this.DestroyInterstitial();
            ((GCHandle)this.interstitialClientPtr).Free();
        }

        ~YumiInterstitialClient()
        {
            this.Dispose();
        }

#endregion

#region Interstitial callback methods

        [MonoPInvokeCallback(typeof(YumiInterstitialDidReceiveAdCallback))]
        private static void InterstitialDidReceiveAdCallback(IntPtr interstitialClient)
        {
            YumiInterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YumiInterstitialDidFailToReceiveAdWithErrorCallback))]
        private static void InterstitialDidFailToReceiveAdWithErrorCallback(
                IntPtr interstitialClient, string error)
        {
            YumiInterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdFailedToLoad != null)
            {
                YumiAdFailedToLoadEventArgs args = new YumiAdFailedToLoadEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YumiInterstitialDidCloseCallback))]
        private static void InterstitialDidCloseCallback(IntPtr interstitialClient)
        {
            YumiInterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(YumiInterstitialDidClickCallback))]
        private static void InterstitialDidClickCallback(IntPtr interstitialClient)
        {
            YumiInterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }
        private static YumiInterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
        {
            GCHandle handle = (GCHandle)interstitialClient;
            return handle.Target as YumiInterstitialClient;
        }
#endregion


    }
}

#endif