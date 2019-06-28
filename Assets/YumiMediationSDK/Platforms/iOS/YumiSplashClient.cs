#if UNITY_IOS
using System;
using YumiMediationSDK.Common;
using YumiMediationSDK.Api;
using System.Runtime.InteropServices;

namespace YumiMediationSDK.iOS
{
    public class YumiSplashClient : IYumiSplashClient
    {

        private IntPtr splashPtr;
        private IntPtr splashClientPtr;

        private YumiSplashOptions splashOptions;

#region Splash callback types

        internal delegate void YumiSplashDidSuccessToShowCallback(IntPtr splashClient);
        internal delegate void YumiSplashDidFailToShowCallback(IntPtr splashClient, string error);
        internal delegate void YumiSplashDidCloseCallback(IntPtr splashClient);
        internal delegate void YumiSplashDidClickCallback(IntPtr splashClient);

#endregion

        // This property should be used when setting the interstitialPtr.
        private IntPtr SplashPtr
        {
            get
            {
                return this.splashPtr;
            }

            set
            {
                YumiExterns.YumiRelease(this.splashPtr);
                this.splashPtr = value;
            }
        }

#region IYumiSplashClient implement 
        // Ad event fired when the splash ad success to show 
        public event EventHandler<EventArgs> OnAdSuccessToShow;
        // Ad event fired when the splash ad has failed to load.
        public event EventHandler<YumiAdFailedToShowEventArgs> OnAdFailedToShow;
        // Ad event fired when the splash ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;
        // Ad event fired when the splash ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;

        // Creates an Splash.
        public void CreateSplashAd(string placementId, string channelId, string versionId, YumiSplashOptions splashOptions) 
        {
            splashClientPtr = (IntPtr)GCHandle.Alloc(this);
            SplashPtr = YumiExterns.InitYumiSplash(splashClientPtr, placementId, channelId, versionId);
            this.splashOptions = splashOptions;

            YumiExterns.SetSplashFetchTime(SplashPtr, splashOptions.adFetchTime);
            if (splashOptions.adOrientation != YumiSplashOrientation.YUMISPLASHORIENTATION_UNKNOWN || splashOptions.adOrientation != YumiSplashOrientation.YUMISPLASHORIENTATION_PORTRAIT)
            {
                YumiExterns.SetSplashOrientation(SplashPtr, (int)splashOptions.adOrientation);
            }

            YumiExterns.SetSplashCallbacks(
                SplashPtr,
                SplashDidSuccessToShowCallback,
                SplashDidFailToShowCallback,
                SplashDidClickCallback,
                SplashDidCloseCallback               
                );

        }
        //load and show full screen splash
        public void LoadAdAndShow()
        {
            YumiExterns.LoadAdAndShowWithBottomViewHeight(SplashPtr, splashOptions.adBottomViewHeight);
        }
        // Destroies the splash ad.
        public void DestroySplashAd()
        {
            SplashPtr = IntPtr.Zero;
        }
        public void Dispose()
        {
            DestroySplashAd();
            ((GCHandle)splashClientPtr).Free();
        }

        ~YumiSplashClient()
        {
            Dispose();
        }
#endregion

#region Splash callback methods

        [MonoPInvokeCallback(typeof(YumiSplashDidSuccessToShowCallback))]
        private static void SplashDidSuccessToShowCallback(IntPtr splashClient)
        {
            YumiSplashClient client = IntPtrToSplashClient(splashClient);
            if (client.OnAdSuccessToShow != null)
            {
                client.OnAdSuccessToShow(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YumiSplashDidFailToShowCallback))]
        private static void SplashDidFailToShowCallback(IntPtr splashClient,string error)
        {
            YumiSplashClient client = IntPtrToSplashClient(splashClient);
            if (client.OnAdFailedToShow != null)
            {
                YumiAdFailedToShowEventArgs args = new YumiAdFailedToShowEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToShow(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(YumiSplashDidClickCallback))]
        private static void SplashDidClickCallback(IntPtr splashClient)
        {
            YumiSplashClient client = IntPtrToSplashClient(splashClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(YumiSplashDidCloseCallback))]
        private static void SplashDidCloseCallback(IntPtr splashClient)
        {
            YumiSplashClient client = IntPtrToSplashClient(splashClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        private static YumiSplashClient IntPtrToSplashClient(IntPtr splashClient)
        {
            GCHandle handle = (GCHandle)splashClient;
            return handle.Target as YumiSplashClient;
        }
#endregion
    }
}
#endif